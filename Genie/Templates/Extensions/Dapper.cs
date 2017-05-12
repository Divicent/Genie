
using Genie.Models;
using Genie.Models.Abstract;
using Genie.Templates.Extensions;

namespace Genie.Templates.Dapper
{
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

