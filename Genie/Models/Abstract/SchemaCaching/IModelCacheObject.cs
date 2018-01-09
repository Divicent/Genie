namespace Genie.Core.Models.Abstract.SchemaCaching
{
    public interface IModelCacheObject
    {
        string Name { get; }
        string Hash { get; }
    }
}