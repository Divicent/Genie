#region Usings

using System.Text;
using Genie.Core.Base.Generating.Concrete;
using Genie.Core.Base.Reading.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure
{
    internal class ProcedureContainerTemplate : GenieTemplate
    {
        private readonly IDatabaseSchema _schema;

        public ProcedureContainerTemplate(string path, IDatabaseSchema schema) : base(path)
        {
            _schema = schema;
        }

        public override string Generate()
        {
            var spList = new StringBuilder();
            var spSingle = new StringBuilder();
            var spVoid = new StringBuilder();

            foreach (var sp in _schema.Procedures)
            {
                spList.AppendLine(
                    $@"		public IEnumerable<T> {sp.Name}_List<T>({
                            sp.ParamString
                        }) {{ return Context.Connection.Query<T>(""EXEC {sp.Name} {sp.PassString}""); }}");
                spSingle.AppendLine(
                    $@"		public T {sp.Name}_Single<T>({
                            sp.ParamString
                        }) {{ return Context.Connection.QueryFirstOrDefault<T>(""EXEC {sp.Name} {
                            sp.PassString
                        }""); }}");
                spVoid.AppendLine(
                    $@"		public void {sp.Name}_Void({sp.ParamString}) {{ Context.Connection.Execute(""EXEC {sp.Name} {
                            sp.PassString
                        }""); }}");
            }

            L($@"

using System;
using System.Collections.Generic;
using System.Linq;
using {GenerationContext.BaseNamespace}.Dapper;
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;

namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
	public class ProcedureContainer: IProcedureContainer
    {{
		private IDapperContext Context {{ get; }}

		internal ProcedureContainer(IDapperContext context)
		{{
		    Context = context;
		}}


{spList}

{spSingle}

{spVoid}

    }}
}}
");

            return E();
        }
    }
}