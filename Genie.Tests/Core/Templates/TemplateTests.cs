using System.Collections.Generic;
using System.Linq;
using Genie.Core.Base.Configuration.Abstract;
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
            
            var mysqlCoreMock = new Mock<IConfiguration>();
            mysqlCoreMock.SetupProperty((s) => s.DBMS, "mysql");
            
            TestForConfiguration(mssqlMock.Object);
            TestForConfiguration(mysqlCoreMock.Object);
        }

        private static void TestForConfiguration(IConfiguration configuration)
        {

            
            
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

            var relationAttributes = new List<IAttribute>
            {
                new Attribute
                {
                    Comment = "Test",
                    DataType = "string",
                    IsIdentity = true,
                    IsKey = true,
                    IsLiteralType = true,
                    Name = "SomeName"
                },
                attr,
                new Attribute
                {
                    Comment = "Test2",
                    DataType = "double?",
                    IsIdentity = false,
                    IsKey = false,
                    IsLiteralType = false,
                    Name = "SomeName2"
                },
                new Attribute
                {
                    Comment = "TestDate",
                    DataType = "DateTime",
                    IsIdentity = false,
                    IsKey = false,
                    IsLiteralType = false,
                    Name = "TestData"
                },
                new Attribute
                {
                    Comment = "TestBool",
                    DataType = "bool",
                    IsIdentity = false,
                    IsKey = false,
                    IsLiteralType = false,
                    Name = "TestBool"
                }
            };
            
            
            
            relationMock.SetupProperty((s) => s.Attributes, relationAttributes);
            relationMock.Setup(r => r.GetAttributes())
                .Returns(relationAttributes);

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

            var enm = new Enum
            {
                Name = "Test",
                Type = "int",
                Values = new List<IEnumValue>
                    {new EnumValue {FieldName = "_test1", Name = "Test1", Value = 1}}
            };
            
            var schemaMock = new Mock<IDatabaseSchema>();
            var procedure = new StoredProcedure
            {
                Name = "TestProcedure",
                Parameters = new List<ProcedureParameter> { new ProcedureParameter { DataType = "string", Name = "Procedure", Position = 1}},
                ParamString = "dsad",
                PassString = "dsads",
            };
            schemaMock.SetupProperty((s) => s.Procedures, new List<IStoredProcedure> { procedure });
            schemaMock.SetupProperty((s) => s.Relations, new List<IRelation> { relation });
            schemaMock.SetupProperty((s) => s.Views, new List<IView> { view });
            schemaMock.SetupProperty((s) => s.BaseNamespace, "testnamespace");
            schemaMock.SetupProperty((s) => s.Enums, new List<IEnum> { enm });
            
            var schema = schemaMock.Object;
            var files = new List<ITemplate>
            {
               
                new UnitOfWorkExtensionsTemplate(@"Infrastructure/UnitOfWork", schema),
                new ProcedureContainerExtensionsTemplate(@"Infrastructure/Models/Concrete/Context/BaseQueryContext", schema,configuration),
                new IModelTemplate(@"SomePath", relation, configuration, false),
                new RelationTemplate(@"Test", relation, enm, configuration),
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
                    view.Attributes),
                new IModelColumnSelectorTemplate(@"Infrastructure/Models/IModel", view),
                new ModelColumnSelectorTemplate(@"Infrastructure/Models/IModel", view),
                new IRepositoryTemplate("", view),
                new IRepositoryTemplate("", relation),
                new RepositoryTemplate("", view),
                new RepositoryTemplate("", relation)
            };


            foreach (var file in files)
            {
                var content = file.Generate();
                Assert.NotEmpty(content);
            }
        }
    }
}