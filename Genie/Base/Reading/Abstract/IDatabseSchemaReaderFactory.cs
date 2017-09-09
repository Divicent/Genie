namespace Genie.Base.Reading.Abstract 
{
    internal interface IDatabaseSchemaReaderFactory 
    {
        IDatabaseSchemaReader GetReader(string  databaseManagementSystemName);
    }
}