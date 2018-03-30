using Genie.Core.Base.Generating;
using Genie.Core.Models.Abstract;

namespace Genie.Core.Templates.Infrastructure.Models.Abstract.Context
{
    public class IModelColumnSelectorTemplate : GenieTemplate
    {
        private readonly IModel _model;

        public IModelColumnSelectorTemplate(string path, IModel model) : base(path)
        {
            _model = model;
        }

       

        public override string Generate()
        {
            L($@"
using System;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context
{{
    public interface I{_model.GetName()}ColumnSelector
    {{
        {Lines(_model.GetAttributes(), FormatColumn, "        ")}
    }}
}}");

            return E();
        }

        private static string FormatColumn(ISimpleAttribute attribute)
        {
            return $@"
                /// <summary>
                /// Select {attribute.Name} Column
                /// </summary>
                IColumn<{attribute.DataType}> {attribute.Name} {{ get; }}
            ";
        }
    }
}