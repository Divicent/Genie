#region Usings

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



            const string template =
@"
using System;
using Genie.Core.Infrastructure.Interfaces;
using {{baseNamespace}}.Infrastructure.Repositories.Abstract;
using {{baseNamespace}}.Infrastructure.Repositories.Concrete;

namespace {{baseNamespace}}.Infrastructure
{
    public static class UnitOfWorkExtensions
    {
{% for relation in relations %}
        public static I{{relation.Name}}Repository {{relation.Name}}Repository(this IUnitOfWork unit)
        {
            return Get(unit, ""{{relation.Name}}"", () => new {{relation.Name}}Repository(unit.Context, unit));
        }
{% endfor %}

{% for view in views %}
        public static I{{view.Name}}Repository {{view.Name}}Repository(this IUnitOfWork unit)
        {
            return Get(unit, ""{{view.Name}}"", () => new {{view.Name}}Repository(unit.Context));
        }
{% endfor %}

        private static T Get<T>(IUnitOfWork unit, string key, Func<T> constructor) where T:class
        {
            if (unit.Repos.TryGetValue(key, out var current))
                return current as T;
            var @new = constructor();
            unit.Repos[key] = @new;
            return @new;
        }
    }
}
";

            var processed = Process(template, new
            {
                baseNamespace = GenerationContext.BaseNamespace,
                relations = _schema.Relations,
                views = _schema.Views,
            });

            return processed;
        }
    }
}