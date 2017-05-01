using Genie.Models;
using Genie.Models.Abstract;
using Genie.Templates.Extensions;

namespace Genie.Templates.Infrastructure.Filters.Concrete
{
    public partial class BaseFilterContext : ITemplateFile
    {
        private readonly string _path;
        public BaseFilterContext(string path)
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

    public partial class BaseOrderContext : ITemplateFile
    {
        private readonly string _path;
        public BaseOrderContext(string path)
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

    public partial class BoolFilter : ITemplateFile
    {
        private readonly string _path;
        public BoolFilter(string path)
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

    public partial class DateFilter : ITemplateFile
    {
        private readonly string _path;
        public DateFilter(string path)
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

    public partial class ExpressionJoin : ITemplateFile
    {
        private readonly string _path;
        public ExpressionJoin(string path)
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

    public partial class NumberFilter : ITemplateFile
    {
        private readonly string _path;
        public NumberFilter(string path)
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

    public partial class OrderElement : ITemplateFile
    {
        private readonly string _path;
        public OrderElement(string path)
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

    public partial class OrderJoin : ITemplateFile
    {
        private readonly string _path;
        public OrderJoin(string path)
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

    public partial class PropertyFilter : ITemplateFile
    {
        private readonly string _path;
        public PropertyFilter(string path)
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

    public partial class QueryMaker : ITemplateFile
    {
        private readonly string _path;
        public QueryMaker(string path)
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

    public partial class RepoQuery : ITemplateFile
    {
        private readonly string _path;
        public RepoQuery(string path)
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

    public partial class StringFilter : ITemplateFile
    {
        private readonly string _path;
        public StringFilter(string path)
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