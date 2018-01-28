using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Interfaces
{
    public class IConnectionStringProviderTemplate : GenieTemplate
    {
        public IConnectionStringProviderTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Data;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Interfaces
{{
    /// <summary>
    /// This is a provider which should be used to give the
    /// target connection string to the context, this should be
    /// implemented inside the applicaiton.
    /// </summary>
    public interface IConnectionStringProvider
    {{
        /// <summary>
        /// Get the target connection string
        /// </summary>
        /// <returns>The connection string</returns>
        string GetConnectionString();
    }}
}}
");

            return E();
        }
    }
}