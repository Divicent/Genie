namespace Genie.Core.Base.Reading.Abstract
{
    internal interface IDatabaseSchemaReaderFactory
    {
        IDatabaseSchemaReader GetReader(string databaseManagementSystemName);
    }
}