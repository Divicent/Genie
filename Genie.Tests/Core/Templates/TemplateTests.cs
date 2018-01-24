using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using Genie.Core.Base.Configuration.Abstract;
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
using Moq;
using Xunit;

namespace Genie.Tests.Core.Templates
{
    public class TemplateTests
    {
        [Fact]
        public void TestTemplates()
        {
            var mssqlMock = new Mock<IConfiguration>();
            mssqlMock.SetupProperty((s) => s.DBMS, "mssql");
            mssqlMock.SetupProperty((s) => s.Core, false);
            
            var mysqlCoreMock = new Mock<IConfiguration>();
            mysqlCoreMock.SetupProperty((s) => s.DBMS, "mysql");
            mysqlCoreMock.SetupProperty((s) => s.Core, true);
            
            TestForConfiguration(mssqlMock.Object);
            TestForConfiguration(mysqlCoreMock.Object);
        }

        private static void TestForConfiguration(IConfiguration configuration)
        {
            var schemaMock = new Mock<IDatabaseSchema>();
            schemaMock.SetupProperty((s) => s.Procedures, new List<IStoredProcedure>());
            schemaMock.SetupProperty((s) => s.Relations, new List<IRelation>());
            schemaMock.SetupProperty((s) => s.Views, new List<IView>());
            schemaMock.SetupProperty((s) => s.BaseNamespace, "testnamespace");
            schemaMock.SetupProperty((s) => s.Enums, new List<IEnum>());
            
            
            var relationMock = new Mock<IRelation>();
            relationMock.SetupProperty((s) => s.Name, "Test");
            relationMock.SetupProperty((s) => s.Comment, "Test");
            relationMock.SetupProperty((s) => s.FieldName, "Test");

            var attr = new Attribute
            {
                Comment = "Test1",
                DataType = "int",
                IsIdentity = false,
                IsKey = false,
                IsLiteralType = false,
                Name = "SomeName2"
            };
            relationMock.SetupProperty((s) => s.Attributes, new List<IAttribute> { new Attribute
            {
                Comment = "Test", 
                DataType = "string", 
                IsIdentity = true, 
                IsKey = true, 
                IsLiteralType = true, 
                Name = "SomeName"
            },
               attr,
                new Attribute {
                    Comment = "Test2", 
                    DataType = "double?", 
                    IsIdentity = false, 
                    IsKey = false, 
                    IsLiteralType = false, 
                    Name = "SomeName2"
                },
                new Attribute {
                    Comment = "TestDate", 
                    DataType = "DateTime", 
                    IsIdentity = false, 
                    IsKey = false, 
                    IsLiteralType = false, 
                    Name = "TestData"
                },
                new Attribute {
                    Comment = "TestBool", 
                    DataType = "bool", 
                    IsIdentity = false, 
                    IsKey = false, 
                    IsLiteralType = false, 
                    Name = "TestBool"
                }
            });
            relationMock.SetupProperty((s) => s.ForeignKeyAttributes,  new List<IForeignKeyAttribute> 
                { new ForeignKeyAttribute 
                    { ReferencingRelationName = "test", 
                        ReferencingNonForeignKeyAttribute = attr, ReferencingTableColumnName = "ID"} });
            relationMock.SetupProperty((s) => s.ReferenceLists,  new List<IReferenceList>
            {
                new ReferenceList { ReferencedPropertyName = "ID", ReferencedPropertyOnThisRelation =  "Test1", ReferencedRelationName =  "Test"}
            });

            var relation = relationMock.Object;
            
            
            var viewMock = new Mock<IView>();
            viewMock.SetupProperty((s) => s.Name, "Test");
            viewMock.SetupProperty((s) => s.FieldName, "Test");
            viewMock.SetupProperty((s) => s.Attributes, new List<ISimpleAttribute> { new Attribute
            {
                Comment = "Test", 
                DataType = "string", 
                IsIdentity = true, 
                IsKey = true, 
                IsLiteralType = true, 
                Name = "SomeName"
            } });

            var view = viewMock.Object;
            var schema = schemaMock.Object;
            var files = new List<ITemplate>
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
                new BaseQueryContextTemplate(@"Infrastructure/Models/Concrete/Context/BaseQueryContext",
                    configuration),
                
                new CommandDefinitionTemplate(@"Dapper/CommandDefinition"),
                new CommandFlagsTemplate(@"Dapper/CommandFlags"),
                new CustomPropertyTypeMapTemplate(@"Dapper/CustomPropertyTypeMap"),
                new DataTableHandlerTemplate(@"Dapper/DataTableHandler"),
                new DbStringTemplate(@"Dapper/DbString"),
                new DefaultTypeMapTemplate(@"Dapper/DefaultTypeMap"),
                new DynamicParameters_CachedOutputSettersTemplate(@"Dapper/DynamicParameters.CachedOutputSetters"),
                new DynamicParametersTemplate(@"Dapper/DynamicParameters"),
                new DynamicParameters_ParamInfoTemplate(@"Dapper/DynamicParameters.ParamInfo"),
                new ExplicitConstructorAttributeTemplate(@"Dapper/ExplicitConstructorAttribute"),
                new FeatureSupportTemplate(@"Dapper/FeatureSupport"),
                new SimpleMemberMapTemplate(@"Dapper/SimpleMemberMap"),
                new SqlDataRecordHandlerTemplate(@"Dapper/SqlDataRecordHandler"),
                new SqlDataRecordListTVPParameterTemplate(@"Dapper/SqlDataRecordListTVPParameter"),
                new SqlMapper_AsyncTemplate(@"Dapper/SqlMapper.Async"),
                new SqlMapper_CacheInfoTemplate(@"Dapper/SqlMapper.CacheInfo"),
                new SqlMapperTemplate(@"Dapper/SqlMapper"),
                new SqlMapper_DapperRowTemplate(@"Dapper/SqlMapper.DapperRow"),
                new SqlMapper_DapperRowMetaObjectTemplate(@"Dapper/SqlMapper.DapperRowMetaObject"),
                new SqlMapper_DapperTableTemplate(@"Dapper/SqlMapper.DapperTable"),
                new SqlMapper_DeserializerStateTemplate(@"Dapper/SqlMapper.DeserializerState"),
                new SqlMapper_DontMapTemplate(@"Dapper/SqlMapper.DontMap"),
                new SqlMapper_GridReader_AsyncTemplate(@"Dapper/SqlMapper.GridReader.Async"),
                new SqlMapper_GridReaderTemplate(@"Dapper/SqlMapper.GridReader"),
                new SqlMapper_ICustomQueryParameterTemplate(@"Dapper/SqlMapper.ICustomQueryParameter"),
                new SqlMapper_IDataReaderTemplate(@"Dapper/SqlMapper.IDataReader"),
                new SqlMapper_IdentityTemplate(@"Dapper/SqlMapper.Identity"),
                new SqlMapper_IDynamicParametersTemplate(@"Dapper/SqlMapper.IDynamicParameters"),
                new SqlMapper_IMemberMapTemplate(@"Dapper/SqlMapper.IMemberMap"),
                new SqlMapper_IParameterCallbacksTemplate(@"Dapper/SqlMapper.IParameterCallbacks"),
                new SqlMapper_IParameterLookupTemplate(@"Dapper/SqlMapper.IParameterLookup"),
                new SqlMapper_ITypeHandlerTemplate(@"Dapper/SqlMapper.ITypeHandler"),
                new SqlMapper_ITypeMapTemplate(@"Dapper/SqlMapper.ITypeMap"),
                new SqlMapper_LinkTemplate(@"Dapper/SqlMapper.Link"),
                new SqlMapper_LiteralTokenTemplate(@"Dapper/SqlMapper.LiteralToken"),
                new SqlMapper_SettingsTemplate(@"Dapper/SqlMapper.Settings"),
                new SqlMapper_TypeDeserializerCacheTemplate(@"Dapper/SqlMapper.TypeDeserializerCache"),
                new SqlMapper_TypeHandlerTemplate(@"Dapper/SqlMapper.TypeHandler"),
                new SqlMapper_TypeHandlerCacheTemplate(@"Dapper/SqlMapper.TypeHandlerCache"),
                new TableValuedParameterTemplate(@"Dapper/TableValuedParameter"),
                new TypeExtensionsTemplate(@"Dapper/TypeExtensions"),
                new UdtTypeHandlerTemplate(@"Dapper/UdtTypeHandler"),
                new WrappedDataReaderTemplate(@"Dapper/WrappedDataReader"),
                new WrappedReaderTemplate(@"Dapper/WrappedReader"),
                new XmlHandlersTemplate(@"Dapper/XmlHandlers"),
                new KeyAttributeTemplate(@"Dapper/KeyAttribute"),
                new IdentityAttributeTemplate(@"Dapper/IdentityAttribute"),
                new SqlMapperExtensionsTemplate(@"Dapper/SqlMapperExtensions", configuration),
                new TableAttributeTemplate(@"Dapper/TableAttribute"),
                new WriteAttributeTemplate(@"Dapper/WriteAttribute"),
                new IModelTemplate(@"SomePath", relation, configuration),
                new RelationTemplate(@"Test", relation, new Enum { Name = "Test", Type = "int", Values = new List<IEnumValue> 
                    { new EnumValue { FieldName = "_test1", Name = "Test1", Value = 1 } }}, configuration),
                new IModelQueryContextTemplate("C://", "Test"),
                new IModelFilterContextTemplate("C://", "name", relation.Attributes.Cast<ISimpleAttribute>().ToList()),
                new IModelOrderContextTemplate("C://", "name", relation.Attributes.Cast<ISimpleAttribute>().ToList()),
                new IModelOrderContextTemplate("C://", "name", relation.Attributes.Cast<ISimpleAttribute>().ToList()),
                new ModelQueryContextTemplate("C://", "name", relation.Attributes.Cast<ISimpleAttribute>().ToList(), configuration),
                new ModelFilterContextTemplate("C://", "name", relation.Attributes.Cast<ISimpleAttribute>().ToList()),
                new ModelOrderContextTemplate("C://", "name", relation.Attributes.Cast<ISimpleAttribute>().ToList()),
                new RelationTemplate(@"Infrastructure/Models/Concrete/" + relation.Name, relation,
                    schema.Enums.FirstOrDefault(e => e.Name == $"{relation.Name}Enum"), configuration),
                new IModelQueryContextTemplate(
                    @"Infrastructure/Models/Abstract/Context/I" + relation.Name + "QueryContext", relation.Name),
                new IModelOrderContextTemplate(
                    @"Infrastructure/Models/Abstract/Context/I" + relation.Name + "OrderContext", relation.Name,
                    relation.Attributes.Cast<ISimpleAttribute>().ToList()),
                new ModelQueryContextTemplate(
                    @"Infrastructure/Models/Concrete/Context/" + relation.Name + "QueryContext", relation.Name,
                    relation.Attributes.Cast<ISimpleAttribute>().ToList(), configuration),
                new ModelFilterContextTemplate(
                    @"Infrastructure/Models/Concrete/Context/" + relation.Name + "FilterContext", relation.Name,
                    relation.Attributes.Cast<ISimpleAttribute>().ToList()),
                new ModelOrderContextTemplate(
                    @"Infrastructure/Models/Concrete/Context/" + relation.Name + "OrderContext", relation.Name,
                    relation.Attributes.Cast<ISimpleAttribute>().ToList()),
                new ViewTemplate(@"Infrastructure/Models/Concrete/" + view.Name, view, configuration),
                new IModelQueryContextTemplate(
                    @"Infrastructure/Models/Abstract/Context/I" + view.Name + "QueryContext", view.Name),
                new IModelFilterContextTemplate(
                    @"Infrastructure/Models/Abstract/Context/I" + view.Name + "FilterContext", view.Name,
                    view.Attributes),
                new IModelOrderContextTemplate(
                    @"Infrastructure/Models/Abstract/Context/I" + view.Name + "OrderContext", view.Name,
                    view.Attributes),
                new ModelQueryContextTemplate(
                    @"Infrastructure/Models/Concrete/Context/" + view.Name + "QueryContext", view.Name,
                    view.Attributes, configuration),
                new ModelFilterContextTemplate(
                    @"Infrastructure/Models/Concrete/Context/" + view.Name + "FilterContext", view.Name,
                    view.Attributes),
                new ModelOrderContextTemplate(
                    @"Infrastructure/Models/Concrete/Context/" + view.Name + "OrderContext", view.Name,
                    view.Attributes)
            };


            foreach (var file in files)
            {
                var content = file.Generate();
                Assert.NotEmpty(content);
            }
        }
    }
}