#region Usings

using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Templates.Infrastructure
{
    internal class ProcedureContainerTemplate : GenieTemplate
    {
        private readonly IDatabaseSchema _schema;
        private readonly IConfiguration _configuration;

        public ProcedureContainerTemplate(string path, IDatabaseSchema schema, IConfiguration configuration) : base(path)
        {
            _schema = schema;
            _configuration = configuration;
        }

        public override string Generate()
        {
            var spList = new StringBuilder();
            var spSingle = new StringBuilder();
            var spVoid = new StringBuilder();
            var quote = FormatHelper.GetDBMSSpecificQuoter(_configuration);
            var parts = FormatHelper.GetDBMSSpecificTemplatePartsContainer(_configuration);


            foreach (var sp in _schema.Procedures)
            {
                spList.AppendLine(
                    $@"		public IEnumerable<T> {sp.Name}_List<T>({sp.ParamString}) {{ return QueryList<T>(""{parts.StoredProcedureCallString} {quote(_configuration.Schema)}.{quote(sp.Name)} {sp.PassString};""); }}");
                spSingle.AppendLine(
                    $@"		public T {sp.Name}_Single<T>({sp.ParamString}) {{ return QuerySingle<T>(""{parts.StoredProcedureCallString} {quote(_configuration.Schema)}.{quote(sp.Name)} {sp.PassString};""); }}");
                spVoid.AppendLine(
                    $@"		public void {sp.Name}_Void({sp.ParamString}) {{ Execute(""{parts.StoredProcedureCallString} {quote(_configuration.Schema)}.{quote(sp.Name)} { sp.PassString };""); }}");
            }

            var usingDapper = (_configuration.Core ? "using Dapper;\n" : "");

            L($@"

using System;
using System.Collections.Generic;
using System.Linq;
using {GenerationContext.BaseNamespace}.Dapper;
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;
using {parts.SqlClientNamespace};
{usingDapper}
namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
	public class ProcedureContainer: IProcedureContainer
    {{
		private IDapperContext Context {{ get; }}

		internal ProcedureContainer(IDapperContext context)
		{{
		    Context = context;
		}}


		private void Execute(string query) 
		{{
			using(var connection = new {parts.SqlConnectionClassName}(Context.Connection.ConnectionString))
			{{
				connection.Open();
				connection.Execute(query);
				connection.Close();
			}}
		}}

		private T QuerySingle<T>(string query) 
		{{
			using(var connection = new {parts.SqlConnectionClassName}(Context.Connection.ConnectionString))
			{{
				connection.Open();
				return connection.QueryFirstOrDefault<T>(query);
			}}
		}}

		private IEnumerable<T> QueryList<T>(string query) 
		{{
			using(var connection = new {parts.SqlConnectionClassName}(Context.Connection.ConnectionString))
			{{
				connection.Open();
				return connection.Query<T>(query);
			}}
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