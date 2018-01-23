namespace Genie.Core.Templates.Abstract
{
    public interface ITemplate
    {
        string Path { get; }
        string Generate();
    }
}