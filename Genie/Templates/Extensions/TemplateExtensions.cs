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
        IContentFile Generate(string path);
    }
}

namespace Genie.Templates.Dapper
{
    public partial class SqlMapper : ITemplateFile
    {
        public IContentFile Generate(string path)
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = path
            };
        }
    }
}

namespace Genie.Templates.General
{
    public partial class EnumBase : ITemplateFile
    {
        public IContentFile Generate(string path)
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = path
            };
        }
    }

    namespace Interfaces
    {
        public partial class IEnumBase : ITemplateFile
        {
            public IContentFile Generate(string path)
            {
                return new ContentFile
                {
                    Content = TransformText(),
                    Path = path
                };
            }
        }
    }
}

namespace Genie.Templates.Infrastructure
{
    public partial class DapperContext : ITemplateFile
    {
        public IContentFile Generate(string path)
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = path
            };
        }
    }

    public partial class FactoryRepository : ITemplateFile
    {
        public IContentFile Generate(string path)
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = path
            };
        }
    }

    public partial class Repository : ITemplateFile
    {
        public IContentFile Generate(string path)
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = path
            };
        }
    }

    public partial class UnitOfWork : ITemplateFile
    {
        public IContentFile Generate(string path)
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = path
            };
        }
    }

    public partial class ViewRepository : ITemplateFile
    {
        public IContentFile Generate(string path)
        {
            return new ContentFile
            {
                Content = TransformText(),
                Path = path
            };
        }
    }

    namespace Enum
    {
        public partial class ConditionExtension : ITemplateFile
        {
            public IContentFile Generate(string path)
            {
                return new ContentFile
                {
                    Content = TransformText(),
                    Path = path
                };
            }
        }
    }

    namespace EnumQueriesStoredProcedures
    {
        public partial class QueriesAndEnum : ITemplateFile
        {
            private readonly List<IRelation> _relations;
             
            internal QueriesAndEnum(List<IRelation> relations)
            {
                _relations = relations;
            }

            public IContentFile Generate(string path)
            {
                return new ContentFile
                {
                    Content = TransformText(),
                    Path = path
                };
            }
        }
    }

    namespace Interfaces
    {
        public partial class IDapperContext : ITemplateFile
        {
            public IContentFile Generate(string path)
            {
                return new ContentFile
                {
                    Content = TransformText(),
                    Path = path
                };
            }
        }

        public partial class IFactoryRepository : ITemplateFile
        {
            public IContentFile Generate(string path)
            {
                return new ContentFile
                {
                    Content = TransformText(),
                    Path = path
                };
            }
        }

        public partial class IRepository : ITemplateFile
        {
            public IContentFile Generate(string path)
            {
                return new ContentFile
                {
                    Content = TransformText(),
                    Path = path
                };
            }
        }

        public partial class IUnitOfWork : ITemplateFile
        {
            public IContentFile Generate(string path)
            {
                return new ContentFile
                {
                    Content = TransformText(),
                    Path = path
                };
            }
        }

        public partial class IViewRepository : ITemplateFile
        {
            public IContentFile Generate(string path)
            {
                return new ContentFile
                {
                    Content = TransformText(),
                    Path = path
                };
            }
        }
    }

    namespace Models
    {
        public partial class Relation : ITemplateFile
        {
            private readonly IRelation _relation;

            internal Relation(IRelation relation)
            {
                _relation = relation;
            }

            public IContentFile Generate(string path)
            {

                return new ContentFile
                {
                    Content = TransformText(),
                    Path = path
                };
            }
        }

        public partial class View : ITemplateFile
        {
            private readonly IView _view;

            internal View(IView view)
            {
                _view = view;
            }

            public IContentFile Generate(string path)
            {
                return new ContentFile
                {
                    Content = TransformText(),
                    Path = path
                };
            }
        }
    }
}