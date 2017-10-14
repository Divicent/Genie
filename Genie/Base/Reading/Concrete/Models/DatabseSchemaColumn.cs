namespace Genie.Core.Base.Reading.Concrete.Models
{
    internal class DatabaseSchemaColumn
    {
        public string TableComment { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string TableFullName { get; set; }
        public string TableName { get; set; }
        public string Type { get; set; }
        public bool Nullable { get; set; }
        public string DataType { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string ReferencedTableName { get; set; }
        public string ReferencedColumnName { get; set; }
        public bool IsIdentity { get; set; }
    }
}