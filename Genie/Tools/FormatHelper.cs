#region Usings

using System.Threading.Tasks;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Models.Abstract;
using Genie.Core.Models.Concrete;

#endregion

namespace Genie.Core.Tools
{
    /// <summary>
    /// Helps to format templates
    /// </summary>
    internal static class FormatHelper 
    {
        private static ITemplatePartsContainer _container;
        
        /// <summary>
        /// Get a function which can be used to quote an attribute name
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <returns>A function</returns>
        internal static System.Func<string, string> GetDBMSSpecificQuoter(IConfiguration configuration)
        {
            var Ql = "";
            var Qr = "";
            switch(configuration.DBMS)
            {
                case "mysql":
                    Ql = Qr = "`";
                    break;
                case "mssql":
                    Ql = "[";
                    Qr = "]";
                    break;
            }
            return (text) => $"{Ql}{text}{Qr}";
        }

        /// <summary>
        /// Returns a container which contains string which can be used in templates 
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <returns>ITemplatePartsContainer</returns>
        internal static ITemplatePartsContainer GetDBMSSpecificTemplatePartsContainer(IConfiguration configuration)
        {
            if(_container != null) return _container;

            var container = new TemplatePartsContainer();
            switch(configuration.DBMS)
            {
                case "mysql":
                    container.SqlClientNamespace = "MySql.Data.MySqlClient";
                    container.SqlConnectionClassName = "MySqlConnection";
                    container.StoredProcedureCallString = "CALL";
                    break;
                case "mssql":
                    container.SqlClientNamespace = "System.Data.SqlClient";
                    container.SqlConnectionClassName = "SqlConnection";
                    container.StoredProcedureCallString = "EXEC";
                    break;
                default:
                    return container;
            }

            return _container = container;
        }
    }
}