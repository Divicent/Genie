using Genie.Models.Abstract;

namespace Genie.Models
{
    internal class ForeignKeyAttribute: IForeignKeyAttribute
    {
        public IAttribute ReferencingNonForeignKeyAttribute { get; set; }

        public string ReferencingRelationName { get; set; }

        public string ReferencingTableColumnName { get; set; }
    }
}
