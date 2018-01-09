namespace Genie.Core.Models.Abstract.SchemaCaching
{
    public interface ISchemaCacheRootObject
    {
        string GenieVersion { get; }
        string BaseNamespace { get; }
        bool IsCore { get; }
        bool NoDapper { get; }
        string Schema { get; }
    }
}