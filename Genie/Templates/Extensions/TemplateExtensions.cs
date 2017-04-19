using System.Collections.Generic;
using Genie.Base.Abstract;
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

namespace Genie.Templates.General
{
    //public partial class EnumBase : ITemplateFile
    //{
    //    private readonly string _path;
    //    public EnumBase(string path)
    //    {
    //        _path = path;
    //    }

    //    public IContentFile Generate()
    //    {
    //        return new ContentFile
    //        {
    //            Content = TransformText(),
    //            Path = _path
    //        };
    //    }
    //}

    namespace Interfaces
    {

        //public partial class IEnumBase : ITemplateFile
        //{

        //    private readonly string _path;
        //    public IEnumBase(string path)
        //    {
        //        _path = path;
        //    }

        //    public IContentFile Generate()
        //    {
        //        return new ContentFile
        //        {
        //            Content = TransformText(),
        //            Path = _path
        //        };
        //    }
        //}
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

        internal UnitOfWork(IDatabaseSchema schema,string path)
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
        public partial class RepositoryImplementation
        {
            private readonly List<Relation> _relations;
            private readonly List<View> _views;
            private readonly string _path;

            internal RepositoryImplementation(string path, List<Relation> relations, List<View> views)
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

namespace Genie.Templates.SqlMaker
{
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

    namespace Interfaces
    {
        public partial class ISqlFirst : ITemplateFile
        {
            private readonly string _path;
            public ISqlFirst(string path)
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

        public partial class ISqlMaker : ITemplateFile
        {
            private readonly string _path;
            public ISqlMaker(string path)
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

        public partial class ISqlMakerDelete : ITemplateFile
        {
            private readonly string _path;
            public ISqlMakerDelete(string path)
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

        public partial class ISqlMakerInsert : ITemplateFile
        {
            private readonly string _path;
            public ISqlMakerInsert(string path)
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

        public partial class ISqlMakerSelect : ITemplateFile
        {
            private readonly string _path;
            public ISqlMakerSelect(string path)
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

        public partial class ISqlMakerUpdate : ITemplateFile
        {
            private readonly string _path;
            public ISqlMakerUpdate(string path)
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

        public partial class ISqlMkrBase : ITemplateFile
        {
            private readonly string _path;
            public ISqlMkrBase(string path)
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