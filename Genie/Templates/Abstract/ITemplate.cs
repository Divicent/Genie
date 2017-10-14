namespace Genie.Core.Templates.Abstract
{
    internal interface ITemplate
    {
        string Path { get; }
        string Generate();
    }
}