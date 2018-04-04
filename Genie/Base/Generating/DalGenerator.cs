#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Exceptions;
using Genie.Core.Base.ProcessOutput.Abstract;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Models.Abstract;
using Genie.Core.Models.Concrete;
using Genie.Core.Templates.Abstract;
using Genie.Core.Templates.Infrastructure;
using Genie.Core.Templates.Infrastructure.Models.Abstract;
using Genie.Core.Templates.Infrastructure.Models.Abstract.Context;
using Genie.Core.Templates.Infrastructure.Models.Concrete;
using Genie.Core.Templates.Infrastructure.Models.Concrete.Context;
using Genie.Core.Templates.Infrastructure.Repositories;

#endregion

namespace Genie.Core.Base.Generating
{
    internal static class DalGenerator
    {
        /// <summary>
        ///     Generate the DAL using schema and configuration
        /// </summary>
        /// <param name="schema">Schema to use</param>
        /// <param name="configuration">Configuration to use</param>
        /// <param name="output">Output to report progress</param>
        /// <returns>Collection of file contents</returns>
        public static IEnumerable<IContentFile> Generate(IDatabaseSchema schema, IConfiguration configuration,
            IProcessOutput output)
        {
            List<ITemplate> files;
            try
            {
                files = new List<ITemplate>
                {
                    new RepositoryImplementationTemplate(@"Infrastructure/Repositories/Repositories", schema),
                    new UnitOfWorkExtensionsTemplate(@"Infrastructure/UnitOfWorkExtensions", schema)
                };

                foreach (var relation in schema.Relations)
                {
                    files.Add(new RelationTemplate(@"Infrastructure/Models/Concrete/" + relation.Name, relation,
                        schema.Enums.FirstOrDefault(e => e.Name == $"{relation.Name}Enum"), configuration));

                    files.Add(new IModelQueryContextTemplate(
                        @"Infrastructure/Models/Abstract/Context/I" + relation.Name + "QueryContext", relation.Name));
                    files.Add(new IModelFilterContextTemplate(
                        @"Infrastructure/Models/Abstract/Context/I" + relation.Name + "FilterContext", relation.Name,
                        relation.Attributes.Cast<ISimpleAttribute>().ToList()));
                    files.Add(new IModelOrderContextTemplate(
                        @"Infrastructure/Models/Abstract/Context/I" + relation.Name + "OrderContext", relation.Name,
                        relation.Attributes.Cast<ISimpleAttribute>().ToList()));
                    
                    files.Add(new IModelColumnSelectorTemplate(
                        @"Infrastructure/Models/Abstract/Context/I" + relation.Name + "ColumnSelector", relation));
                    
                    files.Add(new ModelQueryContextTemplate(
                        @"Infrastructure/Models/Concrete/Context/" + relation.Name + "QueryContext", relation.Name,
                        relation.Attributes.Cast<ISimpleAttribute>().ToList(), configuration));
                    files.Add(new ModelFilterContextTemplate(
                        @"Infrastructure/Models/Concrete/Context/" + relation.Name + "FilterContext", relation.Name,
                        relation.Attributes.Cast<ISimpleAttribute>().ToList()));
                    files.Add(new ModelOrderContextTemplate(
                        @"Infrastructure/Models/Concrete/Context/" + relation.Name + "OrderContext", relation.Name,
                        relation.Attributes.Cast<ISimpleAttribute>().ToList()));
                    files.Add(new ModelColumnSelectorTemplate(
                        @"Infrastructure/Models/Concrete/Context/" + relation.Name + "ColumnSelector", relation));
                }

                foreach (var view in schema.Views)
                {
                    files.Add(new ViewTemplate(@"Infrastructure/Models/Concrete/" + view.Name, view, configuration));

                    files.Add(new IModelQueryContextTemplate(
                        @"Infrastructure/Models/Abstract/Context/I" + view.Name + "QueryContext", view.Name));
                    files.Add(new IModelFilterContextTemplate(
                        @"Infrastructure/Models/Abstract/Context/I" + view.Name + "FilterContext", view.Name,
                        view.Attributes));
                    files.Add(new IModelOrderContextTemplate(
                        @"Infrastructure/Models/Abstract/Context/I" + view.Name + "OrderContext", view.Name,
                        view.Attributes));
                    files.Add(new IModelColumnSelectorTemplate(
                        @"Infrastructure/Models/Abstract/Context/I" + view.Name + "ColumnSelector", view));

                    files.Add(new ModelQueryContextTemplate(
                        @"Infrastructure/Models/Concrete/Context/" + view.Name + "QueryContext", view.Name,
                        view.Attributes, configuration));
                    files.Add(new ModelFilterContextTemplate(
                        @"Infrastructure/Models/Concrete/Context/" + view.Name + "FilterContext", view.Name,
                        view.Attributes));
                    files.Add(new ModelOrderContextTemplate(
                        @"Infrastructure/Models/Concrete/Context/" + view.Name + "OrderContext", view.Name,
                        view.Attributes));
                    files.Add(new ModelColumnSelectorTemplate(
                        @"Infrastructure/Models/Concrete/Context/" + view.Name + "ColumnSelector", view));
                }

                var canWriteAbstractModels = false;

                if (configuration.AbstractModelsEnabled)
                    if (!Directory.Exists(configuration.AbstractModelsLocation))
                        try
                        {
                            Directory.CreateDirectory(configuration.AbstractModelsLocation);
                            canWriteAbstractModels = true;
                        }
                        catch
                        {
                            canWriteAbstractModels = false;
                        }
                    else
                        canWriteAbstractModels = true;
                
                if (canWriteAbstractModels)
                {
                    var internl =configuration.AbstractModelsNamespace.Contains(configuration.BaseNamespace);
                    
                    foreach (var relation in schema.Relations)
                        files.Add(new IModelTemplate(
                            Path.Combine(configuration.AbstractModelsLocation, $"I{relation.Name}"), relation,
                            configuration, !internl) );

                    foreach (var view in schema.Views)
                        files.Add(new IModelTemplate(
                            Path.Combine(configuration.AbstractModelsLocation, $"I{view.Name}"), view, configuration,
                            !internl));
                }
            }
            catch (Exception e)
            {
                throw new GenieException("Unable to create list of template files.", e);
            }

            try
            {
                GenerationContext.BaseNamespace = configuration.BaseNamespace;

                const string comment =
                    @"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Genie (http://www.github.com/rusith/genie).
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
";

                var progress = output.Progress(files.Count, "Generating Content",
                    $"Done generating content for {files.Count} files");
                var contentFiles = new List<ContentFile>();
                foreach (var templateFile in files)
                {
                    contentFiles.Add(new ContentFile
                    {
                        Path = templateFile.Path + "." + "cs",
                        Content = comment + templateFile.Generate(),
                        External = templateFile.External
                    });
                    progress.Tick(templateFile.Path);
                }
                return contentFiles;
            }
            catch (Exception e)
            {
                throw new GenieException("Unable to generate file content", e);
            }
        }
    }
}