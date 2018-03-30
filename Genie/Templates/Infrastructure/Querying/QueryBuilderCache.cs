using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Querying
{
    public class QueryBuilderCacheTemplate: GenieTemplate
    {
        public QueryBuilderCacheTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Querying
{{
    public static class QueryBuilderCache
    {{
        internal static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties 
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        
        internal static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> IdentityProperties 
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        
        internal static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties 
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        
        internal static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName 
            = new ConcurrentDictionary<RuntimeTypeHandle, string>();
        
        internal static readonly ConcurrentDictionary<string, string> SelectParts 
            = new ConcurrentDictionary<string, string>();
    }}
}}");
            return E();
        }
    }
}