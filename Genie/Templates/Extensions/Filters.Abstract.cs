using Genie.Models;
using Genie.Models.Abstract;
using Genie.Templates.Extensions;

namespace Genie.Templates.Infrastructure.Filters.Abstract
{
    public partial class IBoolFilter : ITemplateFile
    {
        private readonly string _path;

        public IBoolFilter(string path)
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

    public partial class IDateFilter : ITemplateFile
    {
        private readonly string _path;

        public IDateFilter(string path)
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

    public partial class IExpressionJoin : ITemplateFile
    {
        private readonly string _path;

        public IExpressionJoin(string path)
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

    public partial class IFilterContext : ITemplateFile
    {
        private readonly string _path;

        public IFilterContext(string path)
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

    public partial class INumberFilter : ITemplateFile
    {
        private readonly string _path;

        public INumberFilter(string path)
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

    public partial class IOrderContext : ITemplateFile
    {
        private readonly string _path;

        public IOrderContext(string path)
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

    public partial class IOrderElement : ITemplateFile
    {
        private readonly string _path;

        public IOrderElement(string path)
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

    public partial class IOrderJoin : ITemplateFile
    {
        private readonly string _path;

        public IOrderJoin(string path)
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

    public partial class IPropertyFilter : ITemplateFile
    {
        private readonly string _path;

        public IPropertyFilter(string path)
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

    public partial class IRepoQuery : ITemplateFile
    {
        private readonly string _path;

        public IRepoQuery(string path)
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

    public partial class IStringFilter : ITemplateFile
    {
        private readonly string _path;

        public IStringFilter(string path)
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