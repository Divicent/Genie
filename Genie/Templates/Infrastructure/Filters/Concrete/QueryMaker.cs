#region Usings


using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Templates.Infrastructure.Filters.Concrete
{
    internal class QueryMakerTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;
        public QueryMakerTemplate(string path, IConfiguration configuration) : base(path)
        {
            _configuration = configuration;
        }

        public override string Generate()
        {
           var quote = FormatHelper.GetDBMSSpecificQuoter(_configuration);

            L($@"

using System.Linq;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete
{{
    internal static class QueryMaker
    {{
        internal static string EqualsTo(string propertyName, object value, bool quoted)
        {{
            return string.Format(""{quote("{0}")} = {{2}}{{1}}{{2}}"", propertyName, value, quoted ? ""'"":"""");
        }}

        internal static string NotEquals(string propertyName, object value, bool quoted)
        {{
            return string.Format(""{quote("{0}")} != {{2}}{{1}}{{2}}"", propertyName, value, quoted ? ""'"" : """");
        }}

        internal static string Contains(string propertyName, object value)
        {{
            return string.Format(""{quote("{0}")} LIKE '%{{1}}%'"", propertyName, value);
        }}

        internal static string NotContains(string propertyName, object value)
        {{
            return string.Format(""{quote("{0}")} NOT LIKE '%{{1}}%'"", propertyName, value);
        }}

        internal static string StartsWith(string propertyName, object value)
        {{
            return string.Format(""{quote("{0}")} LIKE '{{1}}%'"", propertyName, value);
        }}

        internal static string NotStartsWith(string propertyName, object value)
        {{
            return string.Format(""{quote("{0}")} NOT LIKE '{{1}}%'"", propertyName, value);
        }}

        internal static string EndsWith(string propertyName, object value)
        {{
            return string.Format(""{quote("{0}")} LIKE '%{{1}}'"", propertyName, value);
        }} 

        internal static string NotEndsWith(string propertyName, object value)
        {{
            return string.Format(""{quote("{0}")} NOT LIKE '%{{1}}'"", propertyName, value);
        }}

        internal static string IsEmpty(string propertyName)
        {{
            return string.Format(""{quote("{0}")} = ''"", propertyName);
        }}

        internal static string IsNotEmpty(string propertyName)
        {{
            return string.Format(""{quote("{0}")} != ''"", propertyName);
        }}

        internal static string IsNull(string propertyName)
        {{
            return string.Format(""{quote("{0}")} IS NULL"", propertyName);
        }}

        internal static string IsNotNull(string propertyName)
        {{
            return string.Format(""{quote("{0}")} IS NOT NULL"", propertyName);
        }}

        internal static string GreaterThan(string propertyName, object value, bool quoted)
        {{
            return string.Format(""{quote("{0}")} > {{2}}{{1}}{{2}}"", propertyName, value, quoted ? ""'"" : """");
        }}

        internal static string LessThan(string propertyName, object value, bool quoted)
        {{
            return string.Format(""{quote("{0}")} < {{2}}{{1}}{{2}}"", propertyName, value, quoted ? ""'"" : """");
        }}

        internal static string GreaterThanOrEquals(string propertyName, object value, bool quoted)
        {{
            return string.Format(""{quote("{0}")} >= {{2}}{{1}}{{2}}"", propertyName, value, quoted ? ""'"" : """");
        }}

        internal static string LessThanOrEquals(string propertyName, object value, bool quoted)
        {{
            return string.Format(""{quote("{0}")} <= {{2}}{{1}}{{2}}"", propertyName, value, quoted ? ""'"" : """");
        }}

        internal static string Between(string propertyName, object from, object to, bool quoted)
        {{
            return string.Format(""({quote("{0}")} >= {{2}}{{1}}{{2}} AND {quote("{0}")} <= {{2}}{{3}}{{2}})"", propertyName, from, quoted ? ""'"" : """", to);
        }}

        internal static string IsTrue(string propertyName)
        {{
            return string.Format(""{quote("{0}")} = 1"", propertyName);
        }}

        internal static string IsFalse(string propertyName)
        {{
            return string.Format(""{quote("{0}")} = 0"", propertyName);
        }}

        internal static string In(string propertyName, object[] values, bool quoted)
        {{
            return string.Format(""{quote("{0}")} IN({{1}})"", propertyName, values.Aggregate("""", (c, n) => c + string.Format("",{{1}}{{0}}{{1}}"", n, quoted ? ""'"" : """")).TrimStart(','));
        }}

        internal static string NotIn(string propertyName, object[] values, bool quoted)
        {{
            return string.Format(""{quote("{0}")} NOT IN({{1}})"", propertyName, values.Aggregate("""", (c, n) => c + string.Format("",{{1}}{{0}}{{1}}"", n, quoted ? ""'"" : """")).TrimStart(','));
        }}
    }}
}}
");

            return E();
        }
    }
}