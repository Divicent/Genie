using Xunit;GetDbmsSpecificTemplatePartsContainer
using Genie.Core.Tools;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Configuration.Concrete;
using System.Collections.Generic;
using Genie.Core.Models.Abstract;

namespace Genie.Tests.Core.Tools
{
    public class FormatHelperTests
    {
        [Fact]
        public void Tools_FormatHelper_Quoter_MSSQL()
        {
            var quoter = FormatHelper.GetDbmsSpecificQuoter(new GenieConfiguration() { DBMS = "mssql" });
            Assert.Equal(quoter("a"), "[a]");
        }

        [Fact]
        public void Tools_FormatHelper_Quoter_MySQL()
        {
            var quoter = FormatHelper.GetDbmsSpecificQuoter(new GenieConfiguration() { DBMS = "mysql" });
            Assert.Equal(quoter("a"), "`a`");
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_SlqClientNamespace_MSSQL() 
        {
            var parts = GetContainer("mssql");
            Assert.Equal( parts.SqlClientNamespace, "System.Data.SqlClient");
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_SlqClientNamespace_MySql() 
        {
            var parts = GetContainer("mysql");
            Assert.Equal( parts.SqlClientNamespace, "MySql.Data.MySqlClient");
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_SlqClientNamespace_Empty_DBMS() 
        {
            var parts = GetContainer("");
            Assert.Empty(parts.SqlClientNamespace);
        }


        [Fact]
        public void Tools_FormatHelper_TemplateParts_SlqClientNamespace_Null_DBMS() 
        {
            var parts = GetContainer(null);
            Assert.Empty(parts.SqlClientNamespace);
        }


         [Fact]
        public void Tools_FormatHelper_TemplateParts_SqlClassName_MSSQL() 
        {
            var parts = GetContainer("mssql");
            Assert.Equal( parts.SqlConnectionClassName, "SqlConnection");
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_SqlClassName_MySql() 
        {
            var parts = GetContainer("mysql");
            Assert.Equal( parts.SqlConnectionClassName, "MySqlConnection");
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_SqlClassName_Empty_DBMS() 
        {
            var parts = GetContainer("");
            Assert.Empty(parts.SqlConnectionClassName);
        }


        [Fact]
        public void Tools_FormatHelper_TemplateParts_SqlClassName_Null_DBMS() 
        {
            var parts = GetContainer(null);
            Assert.Empty(parts.SqlConnectionClassName);
        }

         [Fact]
        public void Tools_FormatHelper_TemplateParts_ProcedureCallString_MSSQL() 
        {
            var parts = GetContainer("mssql");
            Assert.Equal( parts.StoredProcedureCallString, "EXEC");
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_ProcedureCallString_MySql() 
        {
            var parts = GetContainer("mysql");
            Assert.Equal( parts.StoredProcedureCallString, "CALL");
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_ProcedureCallString_Empty_DBMS() 
        {
            var parts = GetContainer("");
            Assert.Empty(parts.StoredProcedureCallString);
        }


        [Fact]
        public void Tools_FormatHelper_TemplateParts_ProcedureCallString_Null_DBMS() 
        {
            var parts = GetContainer(null);
            Assert.Empty(parts.StoredProcedureCallString);
        }


        private ITemplatePartsContainer GetContainer(string dbms) 
        {
            return FormatHelper.GetDbmsSpecificTemplatePartsContainer(new GenieConfiguration() { DBMS = dbms });
        }
    }
}