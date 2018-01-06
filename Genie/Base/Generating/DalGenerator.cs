#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Exceptions;
using Genie.Core.Base.ProcessOutput.Abstract;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Models.Abstract;
using Genie.Core.Models.Concrete;
using Genie.Core.Templates.Abstract;
using Genie.Core.Templates.Dapper;
using Genie.Core.Templates.Infrastructure;
using Genie.Core.Templates.Infrastructure.Actions.Abstract;
using Genie.Core.Templates.Infrastructure.Actions.Concrete;
using Genie.Core.Templates.Infrastructure.Collections.Abstract;
using Genie.Core.Templates.Infrastructure.Collections.Concrete;
using Genie.Core.Templates.Infrastructure.Enum;
using Genie.Core.Templates.Infrastructure.Filters.Abstract;
using Genie.Core.Templates.Infrastructure.Filters.Concrete;
using Genie.Core.Templates.Infrastructure.Interfaces;
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
        IProgressReporter output)
    {
       
      List<ITemplate> files;
      try
      {
        files = new List<ITemplate>
                {
                    new ConditionExtensionTemplate(@"Infrastructure/Enum/ConditionExtension"),
                    new IBoolFilterTemplate(@"Infrastructure/Filters/Abstract/IBoolFilter"),
                    new IDateFilterTemplate(@"Infrastructure/Filters/Abstract/IDateFilter"),
                    new IExpressionJoinTemplate(@"Infrastructure/Filters/Abstract/IExpressionJoin"),
                    new IFilterContextTemplate(@"Infrastructure/Filters/Abstract/IFilterContext"),
                    new INumberFilterTemplate(@"Infrastructure/Filters/Abstract/INumberFilter"),
                    new IOrderContextTemplate(@"Infrastructure/Filters/Abstract/IOrderContext"),
                    new IOrderElementTemplate(@"Infrastructure/Filters/Abstract/IOrderElement"),
                    new IOrderJoinTemplate(@"Infrastructure/Filters/Abstract/IOrderJoin"),
                    new IPropertyFilterTemplate(@"Infrastructure/Filters/Abstract/IPropertyFilter"),
                    new IRepoQueryTemplate(@"Infrastructure/Filters/Abstract/IRepoQuery"),
                    new IStringFilterTemplate(@"Infrastructure/Filters/Abstract/IStringFilter"),

                    new BaseFilterContextTemplate(@"Infrastructure/Filters/Concrete/BaseFilterContext"),
                    new BaseOrderContextTemplate(@"Infrastructure/Filters/Concrete/BaseOrderContext"),
                    new BoolFilterTemplate(@"Infrastructure/Filters/Concrete/BoolFilter"),
                    new DateFilterTemplate(@"Infrastructure/Filters/Concrete/DateFilter"),
                    new ExpressionJoinTemplate(@"Infrastructure/Filters/Concrete/ExpressionJoin"),
                    new NumberFilterTemplate(@"Infrastructure/Filters/Concrete/NumberFilter"),
                    new OrderElementTemplate(@"Infrastructure/Filters/Concrete/OrderElement", configuration),
                    new OrderJoinTemplate(@"Infrastructure/Filters/Concrete/OrderJoin"),
                    new PropertyFilterTemplate(@"Infrastructure/Filters/Concrete/PropertyFilter"),
                    new QueryMakerTemplate(@"Infrastructure/Filters/Concrete/QueryMaker", configuration),
                    new RepoQueryTemplate(@"Infrastructure/Filters/Concrete/RepoQuery"),
                    new StringFilterTemplate(@"Infrastructure/Filters/Concrete/StringFilter"),

                    new RepositoryImplementationTemplate(@"Infrastructure/Repositories/Repositories", schema),
                    new IDapperContextTemplate(@"Infrastructure/Interfaces/IDapperContext"),
                    new IRepositoryTemplate(@"Infrastructure/Interfaces/IRepository"),
                    new IUnitOfWorkTemplate(@"Infrastructure/Interfaces/IUnitOfWork", schema),
                    new IReadOnlyRepositoryTemplate(@"Infrastructure/Interfaces/IReadOnlyRepository"),
                    new IProcedureContainerTemplate(@"Infrastructure/Interfaces/IProcedureContainer", schema),
                    new IOperationTemplate(@"Infrastructure/Interfaces/IOperation"),

                    new DapperContextTemplate(@"Infrastructure/DapperContext", configuration),
                    new RepositoryTemplate(@"Infrastructure/Repository"),
                    new UnitOfWorkTemplate(@"Infrastructure/UnitOfWork", schema),
                    new ReadOnlyRepositoryTemplate(@"Infrastructure/ReadOnlyRepository"),
                    new ProcedureContainerTemplate(@"Infrastructure/ProcedureContainer", schema, configuration),
                    new OperationTemplate(@"Infrastructure/Operation"),

                    new IReferencedEntityCollectionTemplate(
                        @"Infrastructure/Collections/Abstract/IReferencedEntityCollection"),
                    new ReferencedEntityCollectionTemplate(
                        @"Infrastructure/Collections/Concrete/ReferencedEntityCollection"),

                    new IAddActionTemplate(@"Infrastructure/Actions/Abstract/IAddAction"),
                    new AddActionTemplate(@"Infrastructure/Actions/Concrete/AddAction"),

                    new BaseModelTemplate(@"Infrastructure/Models/Concrete/BaseModel"),
                    new BaseQueryContextTemplate(@"Infrastructure/Models/Concrete/Context/BaseQueryContext", configuration)
                };

        files.AddRange(

            configuration.NoDapper ?
                new List<ITemplate>
                {
                            new KeyAttributeTemplate(@"Dapper/KeyAttribute"),
                            new IdentityAttributeTemplate(@"Dapper/IdentityAttribute"),
                            new SqlMapperExtensionsTemplate(@"Dapper/SqlMapperExtensions", configuration),
                            new TableAttributeTemplate(@"Dapper/TableAttribute"),
                            new WriteAttributeTemplate(@"Dapper/WriteAttribute")
                } :

                new List<ITemplate>
                {
                            new SqlMapper_AsyncTemplate(@"Dapper/SqlMapper.Async"),
                            new XmlHandlersTemplate(@"Dapper/XmlHandlers"),
                            new WrappedReaderTemplate(@"Dapper/WrappedReader"),
                            new WrappedDataReaderTemplate(@"Dapper/WrappedDataReader"),
                            new UdtTypeHandlerTemplate(@"Dapper/UdtTypeHandler"),
                            new TypeExtensionsTemplate(@"Dapper/TypeExtensions"),
                            new TableValuedParameterTemplate(@"Dapper/TableValuedParameter"),
                            new SqlMapper_TypeHandlerCacheTemplate(@"Dapper/SqlMapper.TypeHandlerCache"),
                            new SqlMapper_TypeHandlerTemplate(@"Dapper/SqlMapper.TypeHandler"),
                            new SqlMapper_TypeDeserializerCacheTemplate(@"Dapper/SqlMapper.TypeDeserializerCache"),
                            new SqlMapper_SettingsTemplate(@"Dapper/SqlMapper.Settings"),
                            new SqlMapper_LiteralTokenTemplate(@"Dapper/SqlMapper.LiteralToken"),
                            new SqlMapper_LinkTemplate(@"Dapper/SqlMapper.Link"),
                            new SqlMapper_ITypeMapTemplate(@"Dapper/SqlMapper.ITypeMap"),
                            new SqlMapper_ITypeHandlerTemplate(@"Dapper/SqlMapper.ITypeHandler"),
                            new SqlMapper_IParameterLookupTemplate(@"Dapper/SqlMapper.IParameterLookup"),
                            new SqlMapper_IParameterCallbacksTemplate(@"Dapper/SqlMapper.IParameterCallbacks"),
                            new SqlMapper_IMemberMapTemplate(@"Dapper/SqlMapper.IMemberMap"),
                            new SqlMapper_IDynamicParametersTemplate(@"Dapper/SqlMapper.IDynamicParameters"),
                            new SqlMapper_IdentityTemplate(@"Dapper/SqlMapper.Identity"),
                            new SqlMapper_IDataReaderTemplate(@"Dapper/SqlMapper.IDataReader"),
                            new SqlMapper_ICustomQueryParameterTemplate(@"Dapper/SqlMapper.ICustomQueryParameter"),
                            new SqlMapper_GridReader_AsyncTemplate(@"Dapper/SqlMapper.GridReader.Async"),
                            new SqlMapper_GridReaderTemplate(@"Dapper/SqlMapper.GridReader"),
                            new SqlMapper_DontMapTemplate(@"Dapper/SqlMapper.DontMap"),
                            new SqlMapper_DeserializerStateTemplate(@"Dapper/SqlMapper.DeserializerState"),
                            new SqlMapper_DapperTableTemplate(@"Dapper/SqlMapper.DapperTable"),
                            new SqlMapper_DapperRowMetaObjectTemplate(@"Dapper/SqlMapper.DapperRowMetaObject"),
                            new SqlMapper_CacheInfoTemplate(@"Dapper/SqlMapper.CacheInfo"),
                            new SqlDataRecordListTVPParameterTemplate(@"Dapper/SqlDataRecordListTVPParameter"),
                            new SqlDataRecordHandlerTemplate(@"Dapper/SqlDataRecordHandler"),
                            new ExplicitConstructorAttributeTemplate(@"Dapper/ExplicitConstructorAttribute"),
                            new DynamicParameters_CachedOutputSettersTemplate(
                                @"Dapper/DynamicParameters.CachedOutputSetters"),
                            new DynamicParameters_ParamInfoTemplate(@"Dapper/DynamicParameters.ParamInfo"),
                            new CommandDefinitionTemplate(@"Dapper/CommandDefinition"),
                            new CommandFlagsTemplate(@"Dapper/CommandFlags"),
                            new DapperRowTemplate(@"Dapper/DapperRow"),
                            new DataTableHandlerTemplate(@"Dapper/DataTableHandler"),
                            new SqlMapperTemplate(@"Dapper/SqlMapper"),
                            new CustomPropertyTypeMapTemplate(@"Dapper/CustomPropertyTypeMap"),
                            new DbStringTemplate(@"Dapper/DbString"),
                            new DefaultTypeMapTemplate(@"Dapper/DefaultTypeMap"),
                            new DynamicParametersTemplate(@"Dapper/DynamicParameters"),
                            new FeatureSupportTemplate(@"Dapper/FeatureSupport"),
                            new KeyAttributeTemplate(@"Dapper/KeyAttribute"),
                            new IdentityAttributeTemplate(@"Dapper/IdentityAttribute"),
                            new SimpleMemberMapTemplate(@"Dapper/SimpleMemberMap"),
                            new SqlMapperExtensionsTemplate(@"Dapper/SqlMapperExtensions", configuration),
                            new TableAttributeTemplate(@"Dapper/TableAttribute"),
                            new WriteAttributeTemplate(@"Dapper/WriteAttribute")
                }
            );

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

          files.Add(new ModelQueryContextTemplate(
              @"Infrastructure/Models/Concrete/Context/" + relation.Name + "QueryContext", relation.Name,
              relation.Attributes.Cast<ISimpleAttribute>().ToList(), configuration));
          files.Add(new ModelFilterContextTemplate(
              @"Infrastructure/Models/Concrete/Context/" + relation.Name + "FilterContext", relation.Name,
              relation.Attributes.Cast<ISimpleAttribute>().ToList()));
          files.Add(new ModelOrderContextTemplate(
              @"Infrastructure/Models/Concrete/Context/" + relation.Name + "OrderContext", relation.Name,
              relation.Attributes.Cast<ISimpleAttribute>().ToList()));
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

          files.Add(new ModelQueryContextTemplate(
              @"Infrastructure/Models/Concrete/Context/" + view.Name + "QueryContext", view.Name,
              view.Attributes, configuration));
          files.Add(new ModelFilterContextTemplate(
              @"Infrastructure/Models/Concrete/Context/" + view.Name + "FilterContext", view.Name,
              view.Attributes));
          files.Add(new ModelOrderContextTemplate(
              @"Infrastructure/Models/Concrete/Context/" + view.Name + "OrderContext", view.Name,
              view.Attributes));
        }

        var canWriteAbstractModels = false;

        if (configuration.AbstractModelsEnabled)
        {
          if (!System.IO.Directory.Exists(configuration.AbstractModelsLocation))
          {
            try
            {
              System.IO.Directory.CreateDirectory(configuration.AbstractModelsLocation);
              canWriteAbstractModels = true;
            }
            catch
            {
              canWriteAbstractModels = false;
            }
          }
          else
          {
            canWriteAbstractModels = true;
          }
        }

        if (canWriteAbstractModels)
        {
          foreach (var relation in schema.Relations)
          {
            files.Add(new IModelTemplate(System.IO.Path.Combine(configuration.AbstractModelsLocation, $"I{relation.Name}"), relation, configuration));
          }

          foreach (var view in schema.Views)
          {
            files.Add(new IModelTemplate(System.IO.Path.Combine(configuration.AbstractModelsLocation, $"I{view.Name}"), view, configuration));
          }
        }
      }
      catch (Exception e)
      {
        throw new GenieException("Unable to create list of template files.", e);
      }

      try
      {
        GenerationContext.BaseNamespace = configuration.BaseNamespace;
        GenerationContext.Core = configuration.Core;
        GenerationContext.NoDapper = configuration.NoDapper;
          
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
          
          using (var progress = output.Child( files.Count, "Generating Content"))
          {
              var contentFiles =
                  files.Select(templateFile =>
                  {
                      progress.Tick(templateFile.Path);
                      return new ContentFile
                      {
                          Path = templateFile.Path + "." + "cs",
                          Content = comment + templateFile.Generate()
                      };
                  }).ToList();

              return contentFiles;
          }

      }
      catch (Exception e)
      {
        throw new GenieException("Unable to generate file content", e);
      }
    }
  }
}