namespace Genie.Core.Models.Abstract
{

    /// <summary>
    /// Contains some strings which are DBMS specific
    /// </summary>
    public interface ITemplatePartsContainer
    {
        string SqlClientNamespace { get; }
        string SqlConnectionClassName { get; }
        string StoredProcedureCallString { get; }
    }
}