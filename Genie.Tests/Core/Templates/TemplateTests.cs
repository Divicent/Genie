using System.Collections.Generic;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Templates.Abstract;
using Genie.Core.Templates.Infrastructure.Enum;
using Genie.Core.Templates.Infrastructure.Filters.Abstract;
using Genie.Core.Templates.Infrastructure.Filters.Concrete;
using Moq;
using Xunit;

namespace Genie.Tests.Core.Templates
{
    public class TemplateTests
    {
        [Fact]
        public void TestTemplates()
        {
            var configuration = new Mock<IConfiguration>().Object;
            
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
//
//                new RepositoryImplementationTemplate(@"Infrastructure/Repositories/Repositories", schema),
//                new IDapperContextTemplate(@"Infrastructure/Interfaces/IDapperContext"),
//                new IRepositoryTemplate(@"Infrastructure/Interfaces/IRepository"),
//                new IUnitOfWorkTemplate(@"Infrastructure/Interfaces/IUnitOfWork", schema),
//                new IReadOnlyRepositoryTemplate(@"Infrastructure/Interfaces/IReadOnlyRepository"),
//                new IProcedureContainerTemplate(@"Infrastructure/Interfaces/IProcedureContainer", schema),
//                new IOperationTemplate(@"Infrastructure/Interfaces/IOperation"),
//
//                new DapperContextTemplate(@"Infrastructure/DapperContext", configuration),
//                new RepositoryTemplate(@"Infrastructure/Repository"),
//                new UnitOfWorkTemplate(@"Infrastructure/UnitOfWork", schema),
//                new ReadOnlyRepositoryTemplate(@"Infrastructure/ReadOnlyRepository"),
//                new ProcedureContainerTemplate(@"Infrastructure/ProcedureContainer", schema, configuration),
//                new OperationTemplate(@"Infrastructure/Operation"),
//
//                new IReferencedEntityCollectionTemplate(
//                    @"Infrastructure/Collections/Abstract/IReferencedEntityCollection"),
//                new ReferencedEntityCollectionTemplate(
//                    @"Infrastructure/Collections/Concrete/ReferencedEntityCollection"),
//
//                new IAddActionTemplate(@"Infrastructure/Actions/Abstract/IAddAction"),
//                new AddActionTemplate(@"Infrastructure/Actions/Concrete/AddAction"),
//
//                new BaseModelTemplate(@"Infrastructure/Models/Concrete/BaseModel"),
//                new BaseQueryContextTemplate(@"Infrastructure/Models/Concrete/Context/BaseQueryContext",
//                    configuration)
            };


            foreach (var file in files)
            {
                var content = file.Generate();
                Assert.NotEmpty(content);
            }
        }
    }
}