using Genie.Base.Generating.Concrete;
using Genie.Templates;

namespace Genie.Templates.Infrastructure.Interfaces
{
    internal class IDapperContextTemplate: GenieTemplate
    {
        public IDapperContextTemplate(string path) : base(path){}

public override string Generate()
{
L($@"

using System.Data;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Interfaces
{{
	/// <summary>
    /// A system wide context that holds the connection to the database and manages the connection
    /// </summary>
	public interface IDapperContext
	{{
	    /// <summary>
        /// Connection to the database
        /// </summary>
		IDbConnection Connection {{ get; }}
        IUnitOfWork Unit();
	}}
}}
");

return E();
    
}
    }
}