using Genie.Models.Abstract;

namespace Genie.Models.Concrete
{
    internal class ForeignKeyAttribute: IForeignKeyAttribute
    {
        public IAttribute ReferencingNonForeignKeyAttribute { get; set; }

        public string ReferencingRelationName { get; set; }

        public string ReferencingTableColumnName { get; set; }
    }
}
