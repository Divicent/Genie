namespace Genie.Templates.Abstract 
{
    internal interface ITemplate 
    {
        string Generate();
        string Path { get; }
    }
}