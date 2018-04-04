#region Usings

using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Base.Reading.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure
{
    public class UnitOfWorkExtensionsTemplate : GenieTemplate
    {
        private readonly IDatabaseSchema _schema;

        public UnitOfWorkExtensionsTemplate(string path, IDatabaseSchema schema) : base(path)
        {
            _schema = schema;
        }

        public override string Generate()
        {
            var funcs = new StringBuilder();
            foreach (var relation in _schema.Relations)
            {
                funcs.AppendLine($@"
        public static I{relation.Name}Repository {relation.Name}Repository(this IUnitOfWork unit)
        {{
            return Get(unit, ""{relation.Name}"", () => new {relation.Name}Repository(unit.Context, unit));
        }}
");
            }

            foreach (var view in _schema.Views)
            {
                funcs.AppendLine($@"
        public static I{view.Name}Repository {view.Name}Repository(this IUnitOfWork unit)
        {{
            return Get(unit, ""{view.Name}"", () => new {view.Name}Repository(unit.Context));
        }}
");
            }

            L($@"
using System;
using Genie.Core.Infrastructure.Interfaces;
using {GenerationContext.BaseNamespace}.Infrastructure.Repositories.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Repositories.Concrete;

namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
    public static class UnitOfWorkExtensions
    {{

{funcs}


        private static T Get<T>(IUnitOfWork unit, string key, Func<T> constructor) where T:class
        {{
            if (unit.Repos.TryGetValue(key, out var current))
                return current as T;
            var @new = constructor();
            unit.Repos[key] = @new;
            return @new;
        }}
  
    }}
}}
");

            return E();
        }
    }
}