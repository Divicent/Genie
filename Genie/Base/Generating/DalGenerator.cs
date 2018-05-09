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
using Genie.Core.Templates.Infrastructure.Models;
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
                    new UnitOfWorkExtensionsTemplate(@"Infrastructure/UnitOfWorkExtensions", schema),
                    new ProcedureContainerExtensionsTemplate(@"Infrastructure/ProcedureContainerExtensions", schema, configuration)
                };

                foreach (var relation in schema.Relations)
                {

                    var attributes = relation.Attributes.Cast<ISimpleAttribute>().ToList();

                    files.Add(new ObjectTemplate($"Infrastructure/Models/{relation.Name}", configuration,
                        new IModelColumnSelectorTemplate("", relation),
                        new IModelFilterContextTemplate("", relation.Name, attributes),
                        new IModelOrderContextTemplate("", relation.Name, attributes),
                        new IModelQueryContextTemplate("", relation.Name),

                        new ModelColumnSelectorTemplate(@"", relation),
                        new ModelFilterContextTemplate("", relation.Name, attributes),
                        new ModelOrderContextTemplate("", relation.Name, attributes),
                        new ModelQueryContextTemplate("", relation.Name, attributes, configuration),
                        new RelationTemplate("", relation, schema.Enums.FirstOrDefault(e => e.Name == $"{relation.Name}Enum"), configuration),
                        new IRepositoryTemplate("", relation),
                        new RepositoryTemplate("", relation)

                    ));
                }

                foreach (var view in schema.Views)
                {
                    var attributes = view.Attributes.ToList();

                    files.Add(new ObjectTemplate($"Infrastructure/Models/{view.Name}", configuration,
                        new IModelColumnSelectorTemplate("", view),
                        new IModelFilterContextTemplate("", view.Name, attributes),
                        new IModelOrderContextTemplate("", view.Name, attributes),
                        new IModelQueryContextTemplate("", view.Name),

                        new ModelColumnSelectorTemplate(@"", view),
                        new ModelFilterContextTemplate("", view.Name, attributes),
                        new ModelOrderContextTemplate("", view.Name, attributes),
                        new ModelQueryContextTemplate("", view.Name, attributes, configuration),
                        new ViewTemplate("", view, configuration), 
                        new IRepositoryTemplate("", view),
                        new RepositoryTemplate("", view)

                    ));
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
//
//     This code was generated by Genie (http://www.github.com/divicent/genie).
//     Changes to this file may cause incorrect behavior and will be lost when the code is regenerated.
//
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