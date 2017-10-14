#region Usings

using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    internal class ForeignKeyAttribute : IForeignKeyAttribute
    {
        public IAttribute ReferencingNonForeignKeyAttribute { get; set; }

        public string ReferencingRelationName { get; set; }

        public string ReferencingTableColumnName { get; set; }
    }
}