using Genie.Base.Generating.Concrete;
using Genie.Base.Reading.Abstract;
using Genie.Templates;
using System.Collections.Generic;
using Genie.Models.Abstract;
using System.Text;

namespace Genie.Templates.Infrastructure.Models.Abstract.Context
{
    internal class IModelFilterContextTemplate: GenieTemplate
    {
		private readonly List<ISimpleAttribute> _attributes;
		private readonly string _name;

        public IModelFilterContextTemplate(string path, string name, List<ISimpleAttribute> attributes) : base(path) 
		{
			_name = name;
			_attributes = attributes;
		}

public override string Generate()
{
	var props = new StringBuilder();
	foreach(var atd in _attributes) 
	{
		props.AppendLine();
		if(!string.IsNullOrWhiteSpace(atd.Comment)) 
		{
			props.AppendLine($@"		/// <summary>
		/// {atd.Comment}
		/// </summary>");
		}

		if(atd.DataType == "string") 
		{
			props.AppendLine($@"	    IStringFilter<I{_name}FilterContext,I{_name}QueryContext> {atd.Name} {{ get; }}");
		}
		else if(atd.DataType == "int" || atd.DataType == "int?" || atd.DataType == "double" || atd.DataType == "double?" || atd.DataType == "decimal" || atd.DataType == "decimal?" || atd.DataType == "long" || atd.DataType == "long?" ) 
		{
			props.AppendLine($@"		INumberFilter<I{_name}FilterContext,I{_name}QueryContext> {atd.Name} {{ get; }}");
		}
		else if(atd.DataType == "DateTime" || atd.DataType == "DateTime?") 
		{
			props.AppendLine($@"	    IDateFilter<I{_name}FilterContext,I{_name}QueryContext> {atd.Name} {{ get; }}");
		}
		else if(atd.DataType == "bool" || atd.DataType == "bool?") 
		{
			props.AppendLine($@"	    IBoolFilter<I{_name}FilterContext,I{_name}QueryContext> {atd.Name} {{ get; }}");
		}

	}
L($@"
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context
{{
	public interface I{_name}FilterContext : IFilterContext
	{{

{props}

    }}
}}");

return E();
    
}
    }
}