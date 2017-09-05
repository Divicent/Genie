using System.Text;
using Genie.Base.Generating.Concrete;
using Genie.Base.Reading.Abstract;
using Genie.Templates;

namespace Genie.Templates.Infrastructure.Interfaces
{
    internal class IProcedureContainerTemplate: GenieTemplate
    {
        private readonly IDatabaseSchema _schema;

        public IProcedureContainerTemplate(string path, IDatabaseSchema schema) : base(path)
        {
            _schema = schema;
        }

public override string Generate()
{
    var sps = new StringBuilder();
    foreach(var sp in _schema.Procedures) 
    {
        sps.AppendLine($@"		IEnumerable<T> {sp.Name}_List<T>({sp.ParamString});");
        sps.AppendLine($@"		T {sp.Name}_Single<T>({sp.ParamString});");
        sps.AppendLine($@"		void {sp.Name}_Void({sp.ParamString});");
    }
L($@"
using System;
using System.Collections.Generic;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Interfaces
{{
	public interface IProcedureContainer
    {{

{sps}

    }}
}}
");

return E();
    
}
    }
}