
using Genie.Models;
using Genie.Models.Abstract;
using Genie.Models.Concrete;
using Genie.Templates.Extensions;

namespace Genie.Templates.Dapper
{
    public partial class CommandFlags : ITemplateFile
    {
        private readonly string _path;

        public CommandFlags(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }
    public partial class CommandDefinition : ITemplateFile
    {
        private readonly string _path;

        public CommandDefinition(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class DapperRow : ITemplateFile
    {
        private readonly string _path;

        public DapperRow(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class DataTableHandler : ITemplateFile
    {
        private readonly string _path;

        public DataTableHandler(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class DynamicParameters_CachedOutputSetters : ITemplateFile
    {
        private readonly string _path;

        public DynamicParameters_CachedOutputSetters(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class DynamicParameters_ParamInfo : ITemplateFile
    {
        private readonly string _path;

        public DynamicParameters_ParamInfo(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class ExplicitConstructorAttribute : ITemplateFile
    {
        private readonly string _path;

        public ExplicitConstructorAttribute(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlDataRecordHandler : ITemplateFile
    {
        private readonly string _path;

        public SqlDataRecordHandler(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlDataRecordListTVPParameter : ITemplateFile
    {
        private readonly string _path;

        public SqlDataRecordListTVPParameter(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_CacheInfo : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_CacheInfo(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_DapperRowMetaObject : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_DapperRowMetaObject(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_DapperTable : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_DapperTable(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_DeserializerState : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_DeserializerState(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_DontMap : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_DontMap(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_GridReader : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_GridReader(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_ICustomQueryParameter : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_ICustomQueryParameter(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_IDataReader : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_IDataReader(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_Identity : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_Identity(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_IDynamicParameters : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_IDynamicParameters(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_IMemberMap : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_IMemberMap(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_IParameterCallbacks : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_IParameterCallbacks(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_IParameterLookup : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_IParameterLookup(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_ITypeHandler : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_ITypeHandler(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_ITypeMap : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_ITypeMap(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_Link : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_Link(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_LiteralToken : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_LiteralToken(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_Settings : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_Settings(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_TypeDeserializerCache : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_TypeDeserializerCache(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapper_TypeHandler : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_TypeHandler(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }
    public partial class SqlMapper_TypeHandlerCache : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper_TypeHandlerCache(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }
    public partial class TableValuedParameter : ITemplateFile
    {
        private readonly string _path;

        public TableValuedParameter(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }
    public partial class TypeExtensions : ITemplateFile
    {
        private readonly string _path;

        public TypeExtensions(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }
    public partial class UdtTypeHandler : ITemplateFile
    {
        private readonly string _path;

        public UdtTypeHandler(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }
    public partial class WrappedDataReader : ITemplateFile
    {
        private readonly string _path;

        public WrappedDataReader(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }
    public partial class WrappedReader : ITemplateFile
    {
        private readonly string _path;

        public WrappedReader(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }
    public partial class XmlHandlers : ITemplateFile
    {
        private readonly string _path;

        public XmlHandlers(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }
   
    public partial class SqlMapper : ITemplateFile
    {
        private readonly string _path;

        public SqlMapper(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class CustomPropertyTypeMap : ITemplateFile
    {
        private readonly string _path;

        public CustomPropertyTypeMap(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class DbString : ITemplateFile
    {
        private readonly string _path;

        public DbString(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class DefaultTypeMap : ITemplateFile
    {
        private readonly string _path;

        public DefaultTypeMap(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class DynamicParameters : ITemplateFile
    {
        private readonly string _path;

        public DynamicParameters(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class FeatureSupport : ITemplateFile
    {
        private readonly string _path;

        public FeatureSupport(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class ISqlAdapter : ITemplateFile
    {
        private readonly string _path;

        public ISqlAdapter(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class KeyAttribute : ITemplateFile
    {
        private readonly string _path;

        public KeyAttribute(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class PostgresAdapter : ITemplateFile
    {
        private readonly string _path;

        public PostgresAdapter(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SimpleMemberMap : ITemplateFile
    {
        private readonly string _path;

        public SimpleMemberMap(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlMapperExtensions : ITemplateFile
    {
        private readonly string _path;

        public SqlMapperExtensions(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class SqlServerAdapter : ITemplateFile
    {
        private readonly string _path;

        public SqlServerAdapter(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class TableAttribute : ITemplateFile
    {
        private readonly string _path;

        public TableAttribute(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }

    public partial class WriteAttribute : ITemplateFile
    {
        private readonly string _path;

        public WriteAttribute(string path)
        {
            _path = path;
        }

        public IContentFile Generate()
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = _path
            };
        }
    }
}

