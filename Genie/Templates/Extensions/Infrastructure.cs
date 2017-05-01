using System.Collections.Generic;
using Genie.Base.Abstract;
using Genie.Models;
using Genie.Models.Abstract;
using Genie.Templates.Extensions;

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

    public partial class RepositoryFactory : ITemplateFile
    {
        private readonly string _path;
        public RepositoryFactory(string path)
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
        private readonly IDatabaseSchema _schema;

        internal UnitOfWork(IDatabaseSchema schema, string path)
        {
            _path = path;
            _schema = schema;
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

    public partial class ReadOnlyRepository : ITemplateFile
    {
        private readonly string _path;
        public ReadOnlyRepository(string path)
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

    public partial class ProcedureContainer : ITemplateFile
    {
        private readonly string _path;
        private readonly IDatabaseSchema _schema;

        internal ProcedureContainer(string path, IDatabaseSchema schema)
        {
            _path = path;
            _schema = schema;
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

    public partial class Operation : ITemplateFile
    {
        private readonly string _path;
        public Operation(string path)
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

    namespace Repositories
    {
        public partial class RepositoryImplementation : ITemplateFile
        {
            private readonly List<IRelation> _relations;
            private readonly List<IView> _views;
            private readonly string _path;

            internal RepositoryImplementation(string path, List<IRelation> relations, List<IView> views)
            {
                _relations = relations;
                _path = path;
                _views = views;
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

        public partial class IRepositoryFactory : ITemplateFile
        {
            private readonly string _path;
            public IRepositoryFactory(string path)
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
            private readonly IDatabaseSchema _schema;
            internal IUnitOfWork(IDatabaseSchema schema, string path)
            {
                _path = path;
                _schema = schema;
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

        public partial class IReadOnlyRepository : ITemplateFile
        {
            private readonly string _path;
            public IReadOnlyRepository(string path)
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

        public partial class IProcedureContainer : ITemplateFile
        {
            private readonly string _path;
            private readonly IDatabaseSchema _schema;

            internal IProcedureContainer(string path, IDatabaseSchema schema)
            {
                _path = path;
                _schema = schema;
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

        public partial class IOperation : ITemplateFile
        {
            private readonly string _path;

            internal IOperation(string path)
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
        public partial class BaseModel : ITemplateFile
        {
            private readonly string _path;
            public BaseModel(string path)
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
