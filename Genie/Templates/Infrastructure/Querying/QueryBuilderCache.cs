using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Tools;

namespace Genie.Core.Templates.Infrastructure.Querying
{
    public class QueryBuilderCacheTemplate: GenieTemplate
    {
        private readonly IConfiguration _configuration;
        
        public QueryBuilderCacheTemplate(string path, IConfiguration configuration) : base(path)
        {
            _configuration = configuration;
        }

        public override string Generate()
        {
            var quote = FormatHelper.GetDbmsSpecificQuoter(_configuration);
            L($@"

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using {GenerationContext.BaseNamespace}.Dapper;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Querying
{{
    public static class QueryBuilderCache
    {{
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties 
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> IdentityProperties 
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties 
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName 
            = new ConcurrentDictionary<RuntimeTypeHandle, string>();
        
        internal static readonly ConcurrentDictionary<string, string> SelectParts 
            = new ConcurrentDictionary<string, string>();
        
        
        internal static IEnumerable<PropertyInfo> KeyPropertiesCache(Type type)
        {{

            IEnumerable<PropertyInfo> pi;
            if (KeyProperties.TryGetValue(type.TypeHandle, out pi))
            {{
                return pi;
            }}

            var allProperties = TypePropertiesCache(type).ToList();
            var keyProperties = allProperties.Where(p => p.GetCustomAttributes(true).Any(a => a is KeyAttribute)).ToList();

            KeyProperties[type.TypeHandle] = keyProperties;
            return keyProperties;
        }}

        private static IEnumerable<PropertyInfo> IdentityPropertiesCache(Type type)
        {{

            IEnumerable<PropertyInfo> pi;
            if (IdentityProperties.TryGetValue(type.TypeHandle, out pi))
            {{
                return pi;
            }}

            var allProperties = TypePropertiesCache(type).ToList();
            var identityProperties = allProperties.Where(p => p.GetCustomAttributes(true).Any(a => a is IdentityAttribute)).ToList();

            IdentityProperties[type.TypeHandle] = identityProperties;
            return identityProperties;
        }}

        internal static IEnumerable<PropertyInfo> TypePropertiesCache(Type type)
        {{
            IEnumerable<PropertyInfo> pis;
            if (TypeProperties.TryGetValue(type.TypeHandle, out pis))
            {{
                return pis;
            }}

            var properties = type.GetProperties().Where(IsWriteable).ToList();
            TypeProperties[type.TypeHandle] = properties;
            return properties;
        }}
        
        internal static Tuple<string, string, string> GetInsertParameters(BaseModel entityToInsert)
        {{
            var type = entityToInsert.GetType();

            var name = GetTableName(type);

            var sbColumnList = new StringBuilder(null);

            var allProperties = TypePropertiesCache(type).ToList();
            var keyProperties = KeyPropertiesCache(type).ToList();
            var identityProperties = IdentityPropertiesCache(type).ToList();
            var allPropertiesExceptIndentity = allProperties.Except(identityProperties).ToList();

            var index = 0;
            var lst = allProperties.Count == keyProperties.Count ? keyProperties : allPropertiesExceptIndentity;
            foreach (var property in lst)
            {{
                sbColumnList.AppendFormat(""{quote("{0}")}"", property.Name);
                if (index < lst.Count - 1)
                    sbColumnList.Append("", "");
                index++;
            }}

            index = 0;
            var sbParameterList = new StringBuilder(null);

            foreach (var property in lst)
            {{
                sbParameterList.AppendFormat(""@{{0}}"", property.Name);
                if (index < lst.Count - 1)
                    sbParameterList.Append("", "");
                index++;
            }}

            return new Tuple<string, string, string>(name, sbColumnList.ToString(), sbParameterList.ToString());
        }}
        
        internal static string GetTableName(Type type)
        {{
            string name;
            if (TypeTableName.TryGetValue(type.TypeHandle, out name)) return name;
            name = type.Name + ""s"";
            if (type.GetTypeInfo().IsInterface && name.StartsWith(""I""))
                name = name.Substring(1);

            var tableattr = type.GetTypeInfo().GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == ""TableAttribute"") as
                dynamic;
            if (tableattr != null)
                name = tableattr.Name;
            TypeTableName[type.TypeHandle] = name;
            return name;
        }}
        
        
        private static bool IsWriteable(PropertyInfo pi)
        {{
            var attributes = pi.GetCustomAttributes(typeof(WriteAttribute), false).ToList();
            if (attributes.Count != 1)
                return true;
            var write = (WriteAttribute)attributes.First();
            return write.Write;
        }}
    }}
}}");
            return E();
        }
    }
}