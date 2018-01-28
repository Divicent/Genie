#region Usings

using System.Text;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Templates.Infrastructure
{
    public class ProcedureContainerTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseSchema _schema;

        public ProcedureContainerTemplate(string path, IDatabaseSchema schema, IConfiguration configuration) :
            base(path)
        {
            _schema = schema;
            _configuration = configuration;
        }

        public override string Generate()
        {
            var spList = new StringBuilder();
            var spSingle = new StringBuilder();
            var spVoid = new StringBuilder();
            var quote = FormatHelper.GetDbmsSpecificQuoter(_configuration);
            var parts = FormatHelper.GetDbmsSpecificTemplatePartsContainer(_configuration);


            foreach (var sp in _schema.Procedures)
            {
                spList.AppendLine(
                    $@"		public IEnumerable<T> {sp.Name}_List<T>({sp.ParamString}) {{ return QueryList<T>(""{_configuration.Schema}.{sp.Name}"", new {sp.PassString}); }}");
                spSingle.AppendLine(
                    $@"		public T {sp.Name}_Single<T>({sp.ParamString}) {{ return QuerySingle<T>(""{_configuration.Schema}.{sp.Name}"", new {sp.PassString}); }}");
                spVoid.AppendLine(
                    $@"		public void {sp.Name}_Void({sp.ParamString}) {{ Execute(""{_configuration.Schema}.{sp.Name}"", new {sp.PassString}); }}");
                spList.AppendLine(
                    $@"		public async Task<IEnumerable<T>> {sp.Name}_ListAsync<T>({sp.ParamString}) {{ return await QueryListAsync<T>(""{_configuration.Schema}.{sp.Name}"",  new {sp.PassString}); }}");
                spSingle.AppendLine(
                    $@"		public async Task<T> {sp.Name}_SingleAsync<T>({sp.ParamString}) {{ return await QuerySingleAsync<T>(""{_configuration.Schema}.{sp.Name}"", new {sp.PassString}); }}");
                spVoid.AppendLine(
                    $@"		public async Task {sp.Name}_VoidAsync({sp.ParamString}) {{ await ExecuteAsync(""{_configuration.Schema}.{sp.Name}"", new {sp.PassString}); }}");
            }

            var usingDapper = _configuration.NoDapper ? "using Dapper;\n" : $"using {GenerationContext.BaseNamespace}.Dapper;\n";

            L($@"

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;
using {parts.SqlClientNamespace};
{usingDapper}
namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
	public class ProcedureContainer: IProcedureContainer
    {{
		private IDBContext Context {{ get; }}

		internal ProcedureContainer(IDBContext context)
		{{
		    Context = context;
		}}

		private {parts.SqlConnectionClassName} GetConnection()
		{{
			return new {parts.SqlConnectionClassName}(Context.Connection.ConnectionString);
		}}

		private void Execute(string  name, object parameters) 
		{{
			using(var connection = GetConnection())
			{{
				connection.Open();
				connection.Execute(name, parameters, commandType: CommandType.StoredProcedure);
			}}
		}}

		private T QuerySingle<T>(string  name, object parameters) 
		{{
			using(var connection = GetConnection())
			{{
				connection.Open();
				return connection.QueryFirstOrDefault<T>(name, parameters, commandType: CommandType.StoredProcedure);
			}}
		}}

		private IEnumerable<T> QueryList<T>(string  name, object parameters) 
		{{
			using(var connection = GetConnection())
			{{
				connection.Open();
				return connection.Query<T>(name, parameters, commandType: CommandType.StoredProcedure);
			}}
		}}

		private async Task ExecuteAsync(string  name, object parameters) 
		{{
			using(var connection = GetConnection())
			{{
				connection.Open();
				await connection.ExecuteAsync(name, parameters, commandType: CommandType.StoredProcedure);
				connection.Close();
			}}
		}}

		private async Task<T> QuerySingleAsync<T>(string  name, object parameters) 
		{{
			using(var connection = GetConnection())
			{{
				connection.Open();
				return await connection.QueryFirstOrDefaultAsync<T>(name, parameters, commandType: CommandType.StoredProcedure);
			}}
		}}

		private async Task<IEnumerable<T>> QueryListAsync<T>(string  name, object parameters) 
		{{
			using(var connection = GetConnection())
			{{
				connection.Open();
				return await connection.QueryAsync<T>(name, parameters, commandType: CommandType.StoredProcedure);
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