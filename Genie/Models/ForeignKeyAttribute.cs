namespace Genie.Models
{
    internal class ForeignKeyAttribute: Attribute
    {
        public Attribute ReferencingAttribute { get; set; }
    }
}
