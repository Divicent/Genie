namespace Genie.Base.Exceptions 
{
    public class GenieException: System.Exception
    {
        public GenieException(string message): base(message) {}
        public GenieException(string message, System.Exception innerException): base(message, innerException) {}
    }
}