#region Usings

using System.Text;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Templates.Infrastructure
{
    public class ProcedureContainerExtensionsTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseSchema _schema;

        public ProcedureContainerExtensionsTemplate(string path, IDatabaseSchema schema, IConfiguration configuration) :
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
            var parts = FormatHelper.GetDbmsSpecificTemplatePartsContainer(_configuration);


            foreach (var sp in _schema.Procedures)
            {
                spList.AppendLine(
                    $@"		public static IEnumerable<T> {sp.Name}_List<T>(this IProcedureContainer pc, {sp.ParamString}) {{ return pc.QueryList<T>(""{_configuration.Schema}.{sp.Name}"", new {sp.PassString}); }}");
                spSingle.AppendLine(
                    $@"		public static T {sp.Name}_Single<T>(this IProcedureContainer pc, {sp.ParamString}) {{ return pc.QuerySingle<T>(""{_configuration.Schema}.{sp.Name}"", new {sp.PassString}); }}");
                spVoid.AppendLine(
                    $@"		public static void {sp.Name}_Void(this IProcedureContainer pc, {sp.ParamString}) {{ pc.Execute(""{_configuration.Schema}.{sp.Name}"", new {sp.PassString}); }}");
                spList.AppendLine(
                    $@"		public async static Task<IEnumerable<T>> {sp.Name}_ListAsync<T>(this IProcedureContainer pc, {sp.ParamString}) {{ return await pc.QueryListAsync<T>(""{_configuration.Schema}.{sp.Name}"",  new {sp.PassString}); }}");
                spSingle.AppendLine(
                    $@"		public async static Task<T> {sp.Name}_SingleAsync<T>(this IProcedureContainer pc, {sp.ParamString}) {{ return await pc.QuerySingleAsync<T>(""{_configuration.Schema}.{sp.Name}"", new {sp.PassString}); }}");
                spVoid.AppendLine(
                    $@"		public async static Task {sp.Name}_VoidAsync(this IProcedureContainer pc, {sp.ParamString}) {{ await pc.ExecuteAsync(""{_configuration.Schema}.{sp.Name}"", new {sp.PassString}); }}");
            }

            L($@"

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Genie.Core.Infrastructure.Interfaces;

namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
	public static class ProcedureContainerExtensions
    {{
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