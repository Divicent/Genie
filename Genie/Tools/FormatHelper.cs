#region Usings

using System;
using System.Collections.Generic;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Models.Abstract;
using Genie.Core.Models.Concrete;

#endregion

namespace Genie.Core.Tools
{
    /// <summary>
    ///     Helps to format templates
    /// </summary>
    public static class FormatHelper
    {
        private static readonly Dictionary<string, ITemplatePartsContainer> Containers =
            new Dictionary<string, ITemplatePartsContainer>();

        /// <summary>
        ///     Get a function which can be used to quote an attribute name
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <returns>A function</returns>
        public static Func<string, string> GetDbmsSpecificQuoter(IConfiguration configuration)
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

            return text => $"{ql}{text}{qr}";
        }

        /// <summary>
        ///     Returns a container which contains string which can be used in templates
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <returns>ITemplatePartsContainer</returns>
        public static ITemplatePartsContainer GetDbmsSpecificTemplatePartsContainer(IConfiguration configuration)

        {
            if (string.IsNullOrWhiteSpace(configuration.DBMS))
                return new TemplatePartsContainer
                {
                    SqlConnectionClassName = "",
                    SqlClientNamespace = "",
                    StoredProcedureCallString = ""
                };

            if (Containers.TryGetValue(configuration.DBMS, out _)) return Containers[configuration.DBMS];

            var container = new TemplatePartsContainer();

            if (configuration.DBMS == "mysql")
            {
                container.SqlClientNamespace = "MySql.Data.MySqlClient";
                container.SqlConnectionClassName = "MySqlConnection";
                container.StoredProcedureCallString = "CALL";
            }
            else
            {
                container.SqlClientNamespace = "System.Data.SqlClient";
                container.SqlConnectionClassName = "SqlConnection";
                container.StoredProcedureCallString = "EXEC";
            }

            return Containers[configuration.DBMS] = container;
        }
    }
}