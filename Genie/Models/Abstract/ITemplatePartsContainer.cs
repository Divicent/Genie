namespace Genie.Core.Models.Abstract
{

    /// <summary>
    /// Contains some strings which are DBMS specific
    /// </summary>
    internal interface ITemplatePartsContainer
    {
        string SqlClientNamespace { get; }
        string SqlConnectionClassName { get; }
        string StoredProcedureCallString { get; }
    }
}