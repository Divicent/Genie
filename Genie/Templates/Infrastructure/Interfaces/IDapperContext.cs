#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Interfaces
{
    internal class IDapperContextTemplate : GenieTemplate
    {
        public IDapperContextTemplate(string path) : base(path)
        {
        }

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
      
          /// <summary>
          /// Creates a new unit of work for this context
          /// </summary>
          /// <returns>A new unit of work</returns>
          IUnitOfWork Unit();
	}}
}}
");

            return E();
        }
    }
}