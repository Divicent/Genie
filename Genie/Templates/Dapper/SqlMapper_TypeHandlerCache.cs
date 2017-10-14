using Genie.Core.Base.Generating.Concrete;

namespace Genie.Core.Templates.Dapper
{
    internal class SqlMapper_TypeHandlerCacheTemplate : GenieTemplate
    {
        public SqlMapper_TypeHandlerCacheTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System;
using System.ComponentModel;
using System.Data;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
    partial class SqlMapper
    {{
        /// <summary>
        /// Not intended for direct usage
        /// </summary>
        [Obsolete(ObsoleteInternalUsageOnly, false)]
#if !COREFX
        [Browsable(false)]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static class TypeHandlerCache<T>
        {{
            /// <summary>
            /// Not intended for direct usage
            /// </summary>
            [Obsolete(ObsoleteInternalUsageOnly, true)]
            public static T Parse(object value)
            {{
                return (T)handler.Parse(typeof(T), value);
            }}
 
            /// <summary>
            /// Not intended for direct usage
            /// </summary>
            [Obsolete(ObsoleteInternalUsageOnly, true)]
            public static void SetValue(IDbDataParameter parameter, object value)
            {{
                handler.SetValue(parameter, value);
            }}

            internal static void SetHandler(ITypeHandler handler)
            {{
#pragma warning disable 618
                TypeHandlerCache<T>.handler = handler;
#pragma warning restore 618
            }}

            private static ITypeHandler handler;
        }}
    }}
}}

");

            return E();
        }
    }
}