using System;
using System.Collections.Generic;
using System.Linq;
using Genie.Base.Abstract;
using Genie.Models.Abstract;
using Genie.Templates.Dapper;
using Genie.Templates.Extensions;
using Genie.Templates.Infrastructure;
using Genie.Templates.Infrastructure.Actions.Abstract;
using Genie.Templates.Infrastructure.Actions.Concrete;
using Genie.Templates.Infrastructure.Collections.Abstract;
using Genie.Templates.Infrastructure.Collections.Concrete;
using Genie.Templates.Infrastructure.Enum;
using Genie.Templates.Infrastructure.Filters.Abstract;
using Genie.Templates.Infrastructure.Interfaces;
using Genie.Templates.Infrastructure.Repositories;
using Genie.Templates.Infrastructure.Filters.Concrete;
using Genie.Templates.Infrastructure.Models.Abstract.Context;
using Genie.Templates.Infrastructure.Models.Concrete;
using Genie.Templates.Infrastructure.Models.Concrete.Context;

namespace Genie.Base
{
    internal class DalGenerator : IDalGenerator
    {
        public List<IContentFile> Generate(IDatabaseSchema schema, IConfiguration configuration, IProcessOutput output)
        {

            output.WriteInformation("Generating files.");
            List<ITemplateFile> files;
            try
            {
                files = new List<ITemplateFile>
                {
                    new ConditionExtension(@"Infrastructure\Enum\ConditionExtension"),

                    new IBoolFilter(@"Infrastructure\Filters\Abstract\IBoolFilter"),
                    new IDateFilter(@"Infrastructure\Filters\Abstract\IDateFilter"),
                    new IExpressionJoin(@"Infrastructure\Filters\Abstract\IExpressionJoin"),
                    new IFilterContext(@"Infrastructure\Filters\Abstract\IFilterContext"),
                    new INumberFilter(@"Infrastructure\Filters\Abstract\INumberFilter"),
                    new IOrderContext(@"Infrastructure\Filters\Abstract\IOrderContext"),
                    new IOrderElement(@"Infrastructure\Filters\Abstract\IOrderElement"),
                    new IOrderJoin(@"Infrastructure\Filters\Abstract\IOrderJoin"),
                    new IPropertyFilter(@"Infrastructure\Filters\Abstract\IPropertyFilter"),
                    new IRepoQuery(@"Infrastructure\Filters\Abstract\IRepoQuery"),
                    new IStringFilter(@"Infrastructure\Filters\Abstract\IStringFilter"),

                    new BaseFilterContext(@"Infrastructure\Filters\Concrete\BaseFilterContext"),
                    new BaseOrderContext(@"Infrastructure\Filters\Concrete\BaseOrderContext"),
                    new BoolFilter(@"Infrastructure\Filters\Concrete\BoolFilter"),
                    new DateFilter(@"Infrastructure\Filters\Concrete\DateFilter"),
                    new ExpressionJoin(@"Infrastructure\Filters\Concrete\ExpressionJoin"),
                    new NumberFilter(@"Infrastructure\Filters\Concrete\NumberFilter"),
                    new OrderElement(@"Infrastructure\Filters\Concrete\OrderElement"),
                    new OrderJoin(@"Infrastructure\Filters\Concrete\OrderJoin"),
                    new PropertyFilter(@"Infrastructure\Filters\Concrete\PropertyFilter"),
                    new QueryMaker(@"Infrastructure\Filters\Concrete\QueryMaker"),
                    new RepoQuery(@"Infrastructure\Filters\Concrete\RepoQuery"),
                    new StringFilter(@"Infrastructure\Filters\Concrete\StringFilter"),

                    new RepositoryImplementation(@"Infrastructure\Repositories\Repositories", schema.Relations, schema.Views),
                    new IDapperContext(@"Infrastructure\Interfaces\IDapperContext"),
                    new IRepository(@"Infrastructure\Interfaces\IRepository"),
                    new IUnitOfWork(schema, @"Infrastructure\Interfaces\IUnitOfWork"),
                    new IReadOnlyRepository(@"Infrastructure\Interfaces\IReadOnlyRepository"),
                    new IProcedureContainer(@"Infrastructure\Interfaces\IProcedureContainer", schema),
                    new IOperation(@"Infrastructure\Interfaces\IOperation"),

                    new DapperContext(@"Infrastructure\DapperContext"),
                    new Repository(@"Infrastructure\Repository"),
                    new UnitOfWork(schema, @"Infrastructure\UnitOfWork"),
                    new ReadOnlyRepository(@"Infrastructure\ReadOnlyRepository"),
                    new ProcedureContainer(@"Infrastructure\ProcedureContainer", schema),
                    new Operation(@"Infrastructure\Operation"),

                    new IReferencedEntityCollection(@"Infrastructure\Collections\Abstract\IReferencedEntityCollection"),
                    new ReferencedEntityCollection(@"Infrastructure\Collections\Concrete\ReferencedEntityCollection"),

                    new IAddAction(@"Infrastructure\Actions\Abstract\IAddAction"),
                    new AddAction(@"Infrastructure\Actions\Concrete\AddAction"),

                    new BaseModel(@"Infrastructure\Models\Concrete\BaseModel"),
                    new BaseQueryContext(@"Infrastructure\Models\Concrete\Context\BaseQueryContext")
                };

                if (configuration.NoDapper)
                {
                    files.AddRange(new List<ITemplateFile>
                    {
                        new ISqlAdapter(@"Dapper\ISqlAdapter"),
                        new KeyAttribute(@"Dapper\KeyAttribute"),
                        new PostgresAdapter(@"Dapper\PostgresAdapter"),
                        new SqlMapperExtensions(@"Dapper\SqlMapperExtensions"),
                        new SqlServerAdapter(@"Dapper\SqlServerAdapter"),
                        new TableAttribute(@"Dapper\TableAttribute"),
                        new WriteAttribute(@"Dapper\WriteAttribute"),
                    });
                }
                else
                {
                    files.AddRange(new List<ITemplateFile>
                    {
                        new XmlHandlers(@"Dapper\XmlHandlers"),
                        new WrappedReader(@"Dapper\WrappedReader"),
                        new WrappedDataReader(@"Dapper\WrappedDataReader"),
                        new UdtTypeHandler(@"Dapper\UdtTypeHandler"),
                        new TypeExtensions(@"Dapper\TypeExtensions"),
                        new TableValuedParameter(@"Dapper\TableValuedParameter"),
                        new SqlMapper_TypeHandlerCache(@"Dapper\SqlMapper.TypeHandlerCache"),
                        new SqlMapper_TypeHandler(@"Dapper\SqlMapper.TypeHandler"),
                        new SqlMapper_TypeDeserializerCache(@"Dapper\SqlMapper.TypeDeserializerCache"),
                        new SqlMapper_Settings(@"Dapper\SqlMapper.Settings"),
                        new SqlMapper_LiteralToken(@"Dapper\SqlMapper.LiteralToken"),
                        new SqlMapper_Link(@"Dapper\SqlMapper.Link"),
                        new SqlMapper_ITypeMap(@"Dapper\SqlMapper.ITypeMap"),
                        new SqlMapper_ITypeHandler(@"Dapper\SqlMapper.ITypeHandler"),
                        new SqlMapper_IParameterLookup(@"Dapper\SqlMapper.IParameterLookup"),
                        new SqlMapper_IParameterCallbacks(@"Dapper\SqlMapper.IParameterCallbacks"),
                        new SqlMapper_IMemberMap(@"Dapper\SqlMapper.IMemberMap"),
                        new SqlMapper_IDynamicParameters(@"Dapper\SqlMapper.IDynamicParameters"),
                        new SqlMapper_Identity(@"Dapper\SqlMapper.Identity"),
                        new SqlMapper_IDataReader(@"Dapper\SqlMapper.IDataReader"),
                        new SqlMapper_ICustomQueryParameter(@"Dapper\SqlMapper.ICustomQueryParameter"),
                        new SqlMapper_GridReader(@"Dapper\SqlMapper.GridReader"),
                        new SqlMapper_DontMap(@"Dapper\SqlMapper.DontMap"),
                        new SqlMapper_DeserializerState(@"Dapper\SqlMapper.DeserializerState"),
                        new SqlMapper_DapperTable(@"Dapper\SqlMapper.DapperTable"),
                        new SqlMapper_DapperRowMetaObject(@"Dapper\SqlMapper.DapperRowMetaObject"),
                        new SqlMapper_CacheInfo(@"Dapper\SqlMapper.CacheInfo"),
                        new SqlDataRecordListTVPParameter(@"Dapper\SqlDataRecordListTVPParameter"),
                        new SqlDataRecordHandler(@"Dapper\SqlDataRecordHandler"),
                        new ExplicitConstructorAttribute(@"Dapper\ExplicitConstructorAttribute"),
                        new DynamicParameters_CachedOutputSetters(@"Dapper\DynamicParameters.CachedOutputSetters"),
                        new DynamicParameters_ParamInfo(@"Dapper\DynamicParameters.ParamInfo"),
                        new CommandDefinition(@"Dapper\CommandDefinition"),
                        new CommandFlags(@"Dapper\CommandFlags"),
                        new DapperRow(@"Dapper\DapperRow"),
                        new DataTableHandler(@"Dapper\DataTableHandler"),
                        new SqlMapper(@"Dapper\SqlMapper"),
                        new CustomPropertyTypeMap(@"Dapper\CustomPropertyTypeMap"),
                        new DbString(@"Dapper\DbString"),
                        new DefaultTypeMap(@"Dapper\DefaultTypeMap"),
                        new DynamicParameters(@"Dapper\DynamicParameters"),
                        new FeatureSupport(@"Dapper\FeatureSupport"),
                        new ISqlAdapter(@"Dapper\ISqlAdapter"),
                        new KeyAttribute(@"Dapper\KeyAttribute"),
                        new PostgresAdapter(@"Dapper\PostgresAdapter"),
                        new SimpleMemberMap(@"Dapper\SimpleMemberMap"),
                        new SqlMapperExtensions(@"Dapper\SqlMapperExtensions"),
                        new SqlServerAdapter(@"Dapper\SqlServerAdapter"),
                        new TableAttribute(@"Dapper\TableAttribute"),
                        new WriteAttribute(@"Dapper\WriteAttribute"),
                    });
                }


                foreach (var relation in schema.Relations)
                {
                    files.Add(new Relation(relation, @"Infrastructure\Models\Concrete\" + relation.Name));

                    files.Add(new IModelQueryContext(relation.Name, @"Infrastructure\Models\Abstract\Context\I" + relation.Name + "QueryContext"));
                    files.Add(new IModelFilterContext(relation.Name, relation.Attributes.Cast<ISimpleAttribute>().ToList(), @"Infrastructure\Models\Abstract\Context\I" + relation.Name + "FilterContext"));
                    files.Add(new IModelOrderContext(relation.Name, relation.Attributes.Cast<ISimpleAttribute>().ToList(), @"Infrastructure\Models\Abstract\Context\I" + relation.Name + "OrderContext"));

                    files.Add(new ModelQueryContext(relation.Name, relation.Attributes.Cast<ISimpleAttribute>().ToList(), @"Infrastructure\Models\Concrete\Context\" + relation.Name + "QueryContext"));
                    files.Add(new ModelFilterContext(relation.Name, relation.Attributes.Cast<ISimpleAttribute>().ToList(), @"Infrastructure\Models\Concrete\Context\" + relation.Name + "FilterContext"));
                    files.Add(new ModelOrderContext(relation.Name, relation.Attributes.Cast<ISimpleAttribute>().ToList(), @"Infrastructure\Models\Concrete\Context\" + relation.Name + "OrderContext"));
                }

                foreach (var view in schema.Views)
                {
                    files.Add(new View(view, @"Infrastructure\Models\Concrete\" + view.Name));

                    files.Add(new IModelQueryContext(view.Name, @"Infrastructure\Models\Abstract\Context\I" + view.Name + "QueryContext"));
                    files.Add(new IModelFilterContext(view.Name, view.Attributes, @"Infrastructure\Models\Abstract\Context\I" + view.Name + "FilterContext"));
                    files.Add(new IModelOrderContext(view.Name, view.Attributes, @"Infrastructure\Models\Abstract\Context\I" + view.Name + "OrderContext"));

                    files.Add(new ModelQueryContext(view.Name, view.Attributes, @"Infrastructure\Models\Concrete\Context\" + view.Name + "QueryContext"));
                    files.Add(new ModelFilterContext(view.Name, view.Attributes, @"Infrastructure\Models\Concrete\Context\" + view.Name + "FilterContext"));
                    files.Add(new ModelOrderContext(view.Name, view.Attributes, @"Infrastructure\Models\Concrete\Context\" + view.Name + "OrderContext"));
                }

                output.WriteInformation($"{files.Count} file found.");
            }
            catch (Exception e)
            {
                throw new Exception("Unable to create list of template files.", e);
            }

            try
            {
                GenerationContext.BaseNamespace = configuration.BaseNamespace;
                GenerationContext.Core = configuration.Core;
                GenerationContext.NoDapper = configuration.NoDapper;
                output.WriteInformation("Generating File content.");
                var contentFiles =
                    files.Select(templateFile => templateFile.Generate()).ToList();

                output.WriteSuccess($"Successfully generated {contentFiles.Count} files.");

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

                foreach (var contentFile in contentFiles)
                {
                    contentFile.Path = contentFile.Path + "." + "cs";
                    contentFile.Content = comment + contentFile.Content;
                }

                return contentFiles;

            }
            catch (Exception e)
            {
                throw new Exception("Unable to generate file content", e);
            }
           
        }
    }
}
