
#region Usings

using Genie.Core.Base.Generating;

#endregion

namespace Genie.Core.Templates.Dapper
{
    public class DynamicParameters_CachedOutputSettersTemplate : GenieTemplate
    {
        public DynamicParameters_CachedOutputSettersTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
using System.Collections;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
    public partial class DynamicParameters
    {{
        // The type here is used to differentiate the cache by type via generics
        // ReSharper disable once UnusedTypeParameter
        internal static class CachedOutputSetters<T>
        {{
            // Intentional, abusing generics to get our cache splits
            // ReSharper disable once StaticMemberInGenericType
            public static readonly Hashtable Cache = new Hashtable();
        }}
    }}
}}

");

            return E();
        }
    }
}
