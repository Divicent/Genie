using System.Collections.Generic;
using Genie.Models;
using Genie.Models.Abstract;
using Genie.Templates.Extensions;

namespace Genie.Templates.Extensions
{
    /// <summary>
    /// represents a generate able template 
    /// </summary>
    public interface ITemplateFile
    {
        /// <summary>
        /// Generate the template
        /// </summary>
        /// <returns></returns>
        IContentFile Generate();
    }
}

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
}

namespace Genie.Templates.General
{
    public partial class EnumBase : ITemplateFile
    {
        private readonly string _path;
        public EnumBase(string path)
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

    namespace Interfaces
    {

        public partial class IEnumBase : ITemplateFile
        {

            private readonly string _path;
            public IEnumBase(string path)
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
}

namespace Genie.Templates.Infrastructure
{
    public partial class DapperContext : ITemplateFile
    {

        private readonly string _path;
        public DapperContext(string path)
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

    public partial class FactoryRepository : ITemplateFile
    {
        private readonly string _path;
        public FactoryRepository(string path)
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

    public partial class Repository : ITemplateFile
    {
        private readonly string _path;
        public Repository(string path)
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

    public partial class UnitOfWork : ITemplateFile
    {
        private readonly string _path;
        public UnitOfWork(string path)
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

    public partial class ViewRepository : ITemplateFile
    {
        private readonly string _path;
        public ViewRepository(string path)
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

    namespace Enum
    {
        public partial class ConditionExtension : ITemplateFile
        {
            private readonly string _path;
            public ConditionExtension(string path)
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

    namespace EnumQueriesStoredProcedures
    {
        public partial class QueriesAndEnum : ITemplateFile
        {
            private readonly List<IRelation> _relations;
            private readonly string _path; 
            internal QueriesAndEnum(List<IRelation> relations, string path)
            {
                _relations = relations;
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

    namespace Interfaces
    {
        public partial class IDapperContext : ITemplateFile
        {
            private readonly string _path;
            public IDapperContext(string path)
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

        public partial class IFactoryRepository : ITemplateFile
        {
            private readonly string _path;
            public IFactoryRepository(string path)
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

        public partial class IRepository : ITemplateFile
        {
            private readonly string _path;
            public IRepository(string path)
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

        public partial class IUnitOfWork : ITemplateFile
        {
            private readonly string _path;
            public IUnitOfWork(string path)
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

        public partial class IViewRepository : ITemplateFile
        {
            private readonly string _path;
            public IViewRepository(string path)
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

    namespace Models
    {
        public partial class Relation : ITemplateFile
        {
            private readonly IRelation _relation;
            private readonly string _path;

            internal Relation(IRelation relation, string path)
            {
                _relation = relation;
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

        public partial class View : ITemplateFile
        {
            private readonly IView _view;
            private readonly string _path;

            internal View(IView view, string path)
            {
                _view = view;
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
}