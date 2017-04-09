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
                Path = @"\Dapper\SqlMapper.cs"
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
                Path = @"\General\EnumBase.cs"
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
                    Path = @"\General\Interfaces\IEnumBase.cs"
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
                Path = @"\Infrastructure\DapperContext.cs"
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
                Path = @"\Infrastructure\FactoryRepository.cs"
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
                Path = @"\Infrastructure\Repository.cs"
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
                Path = @"\Infrastructure\UnitOfWork.cs"
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
                Path = @"\Infrastructure\ViewRepository.cs"
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
                    Path = @"\Infrastructure\Enum\ConditionExtension.cs"
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
                    Path = @"\Infrastructure\EnumQueriesStoredProcedures\QueriesAndEnum.cs"
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
                    Path = @"\Infrastructure\Interfaces\IDapperContext.cs"
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
                    Path = @"\Infrastructure\Interfaces\IFactoryRepository.cs"
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
                    Path = @"\Infrastructure\Interfaces\IRepository.cs"
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
                    Path = @"\Infrastructure\Interfaces\IUnitOfWork.cs"
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
                    Path = @"\Infrastructure\Interfaces\IViewRepository.cs"
                };
            }
        }
    }
}