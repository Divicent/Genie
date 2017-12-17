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
        internal static System.Func<string, string> GetDbmsSpecificQuoter(IConfiguration configuration)
        {
            var ql = "";
            var qr = "";
            if (configuration.DBMS == "mysql")
            {
                ql = "`";
                qr = "`";
            }
            else if (configuration.DBMS == "mssql")
            {
                ql = "[";
                qr = "]";
            }
            return (text) => $"{ql}{text}{qr}";
        }

        /// <summary>
        /// Returns a container which contains string which can be used in templates 
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <returns>ITemplatePartsContainer</returns>
        internal static ITemplatePartsContainer GetDbmsSpecificTemplatePartsContainer(IConfiguration configuration)
        {
            if (_container != null)
            {
                return _container;
            }

            var container = new TemplatePartsContainer();

            if (configuration.DBMS == "mysql")
            {
                container.SqlClientNamespace = "MySql.Data.MySqlClient";
                container.SqlConnectionClassName = "MySqlConnection";
                container.StoredProcedureCallString = "CALL";

            }
            else if (configuration.DBMS == "mssql")
            {
                container.SqlClientNamespace = "System.Data.SqlClient";
                container.SqlConnectionClassName = "SqlConnection";
                container.StoredProcedureCallString = "EXEC";
            }

            return _container = container;
        }
    }
}