
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Templates.Infrastructure.Models.Abstract.Context;
using Genie.Core.Templates.Infrastructure.Models.Concrete.Context;
using Genie.Core.Templates.Infrastructure.Repositories;

namespace Genie.Core.Templates.Infrastructure.Models
{
    public class ObjectTemplate: GenieTemplate
    {
        private readonly IModelColumnSelectorTemplate _iColumnSelector;
        private readonly IModelFilterContextTemplate _iFilterContext;
        private readonly IModelOrderContextTemplate _iOrderContext;
        private readonly IModelQueryContextTemplate _iQueryContext;

        private readonly ModelColumnSelectorTemplate _modelColumnSelector;
        private readonly ModelFilterContextTemplate _filterContext;
        private readonly ModelOrderContextTemplate _orderContext;
        private readonly ModelQueryContextTemplate _queryContext;
        private readonly GenieTemplate _model;

        private readonly IConfiguration _configuration;

        private readonly IRepositoryTemplate _iRepository;
        private readonly RepositoryTemplate _repository;

        public ObjectTemplate(string path, IConfiguration configuration,
            IModelColumnSelectorTemplate iColumnSelector,
            IModelFilterContextTemplate iFilterContext,
            IModelOrderContextTemplate iOrderContext,
            IModelQueryContextTemplate iQueryContext, 

            ModelColumnSelectorTemplate modelColumnSelector,
            ModelFilterContextTemplate filterContext,
            ModelOrderContextTemplate orderContext,
            ModelQueryContextTemplate queryContext,
            GenieTemplate model,
            IRepositoryTemplate iRepository,
            RepositoryTemplate repository
            ) : base(path)
        {
            
            _iColumnSelector = iColumnSelector;
            _iFilterContext = iFilterContext;
            _iOrderContext = iOrderContext;
            _iQueryContext = iQueryContext;

            _modelColumnSelector = modelColumnSelector;
            _filterContext = filterContext;
            _orderContext = orderContext;
            _queryContext = queryContext;
            _model = model;
            _configuration = configuration;

            _iRepository = iRepository;
            _repository = repository;
        }

        public override string Generate()
        {
            const string template =
@"
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Genie.Core.Infrastructure;
using Genie.Core.Infrastructure.Actions.Abstract;
using Genie.Core.Infrastructure.Actions.Concrete;
using Genie.Core.Infrastructure.Collections.Abstract;
using Genie.Core.Infrastructure.Collections.Concrete;
using Genie.Core.Infrastructure.Filters.Abstract;
using Genie.Core.Infrastructure.Filters.Concrete;
using Genie.Core.Infrastructure.Interfaces;
using Genie.Core.Infrastructure.Models;
using Genie.Core.Mapper;
using {{baseNamespace}}.Infrastructure.Models.Abstract.Context;
using {{baseNamespace}}.Infrastructure.Models.Concrete;
using {{baseNamespace}}.Infrastructure.Models.Concrete.Context;
using {{baseNamespace}}.Infrastructure.Repositories.Abstract;
{{abstractModelsNamespace}}

namespace {{baseNamespace}}.Infrastructure
{

    namespace Models 
    {
        namespace Abstract
        {
            namespace Context
            {
            
{{icolumnSelector}}

{{ifilterContext}}

{{iOrderContext}}

{{iQueryContext}}

            }
        }

        namespace Concrete
        {
        
{{model}}
        
            namespace Context
            {

{{modelColumnSelector}}

{{filterContext}}

{{orderContext}}

{{queryContext}}

            }
        }
    }

    namespace Repositories
    {
        namespace Abstract 
        {
{{iRepository}}
        }

        namespace Concrete 
        {
{{repository}}
        }
    }
}

";
            var abstractModelsNamespace = _configuration.AbstractModelsEnabled
                ? $"using {_configuration.AbstractModelsNamespace};\n"
                : "";

            return Process(nameof(ObjectTemplate), template, new
            {
                baseNamespace = GenerationContext.BaseNamespace,
                abstractModelsNamespace,
                icolumnSelector = _iColumnSelector.Generate(),
                ifilterContext = _iFilterContext.Generate(),
                iOrderContext = _iOrderContext.Generate(),
                iQueryContext = _iQueryContext.Generate(),
                model = _model.Generate(),
                modelColumnSelector = _modelColumnSelector.Generate(),
                filterContext = _filterContext.Generate(),
                orderContext = _orderContext.Generate(),
                queryContext = _queryContext.Generate(),
                iRepository = _iRepository.Generate(),
                repository = _repository.Generate()
            });
        }
    }
}
