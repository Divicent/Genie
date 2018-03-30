using Genie.Core.Base.Generating;
using Genie.Core.Models.Abstract;

namespace Genie.Core.Templates.Infrastructure.Models.Concrete.Context
{
    public class ModelColumnSelectorTemplate: GenieTemplate
    {
        private readonly IModel _model;
        
        public ModelColumnSelectorTemplate(string path, IModel model) : base(path)
        {
            _model = model;
        }

        public override string Generate()
        {
            L($@"
using System;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete.Context
{{
    public class {_model.GetName()}ColumnSelector: I{_model.GetName()}ColumnSelector
    {{
        {Lines(_model.GetAttributes(), FormatColumn, "        ")}
    }}
}}");

            return E();
        }
        
        
        private static string FormatColumn(ISimpleAttribute attribute)
        {
            return $@"
                public IColumn<{attribute.DataType}> {attribute.Name} {{ get {{ return new Column<{attribute.DataType}>(""{attribute.Name}""); }} }}
            ";
        }
    }
}