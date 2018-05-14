using Genie.Core.Base.Configuration.Concrete;
using Genie.Core.Models.Abstract;
using Genie.Core.Tools;
using Xunit;

namespace Genie.Tests.Core.Tools
{
    public class FormatHelperTests
    {
        private ITemplatePartsContainer GetContainer(string dbms)
        {
            return FormatHelper.GetDbmsSpecificTemplatePartsContainer(
                new GenieConfiguration {DBMS = dbms});
        }

        [Fact]
        public void Tools_FormatHelper_Quoter_MSSQL()
        {
            var quoter = FormatHelper.GetDbmsSpecificQuoter(
                new GenieConfiguration {DBMS = "mssql"});
            Assert.Equal("[a]", quoter("a"));
        }

        [Fact]
        public void Tools_FormatHelper_Quoter_MySQL()
        {
            var quoter = FormatHelper.GetDbmsSpecificQuoter(
                new GenieConfiguration {DBMS = "mysql"});
            Assert.Equal("`a`", quoter("a"));
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_ProcedureCallString_Empty_DBMS()
        {
            var parts = GetContainer("");
            Assert.Empty(parts.StoredProcedureCallString);
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_ProcedureCallString_MSSQL()
        {
            var parts = GetContainer("mssql");
            Assert.Equal("EXEC", parts.StoredProcedureCallString);
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_ProcedureCallString_MySql()
        {
            var parts = GetContainer("mysql");
            Assert.Equal("CALL", parts.StoredProcedureCallString);
        }


        [Fact]
        public void Tools_FormatHelper_TemplateParts_ProcedureCallString_Null_DBMS()
        {
            var parts = GetContainer(null);
            Assert.Empty(parts.StoredProcedureCallString);
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_SlqClientNamespace_Empty_DBMS()
        {
            var parts = GetContainer("");
            Assert.Empty(parts.SqlClientNamespace);
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_SlqClientNamespace_MSSQL()
        {
            var parts = GetContainer("mssql");
            Assert.Equal("System.Data.SqlClient", parts.SqlClientNamespace);
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_SlqClientNamespace_MySql()
        {
            var parts = GetContainer("mysql");
            Assert.Equal("MySql.Data.MySqlClient", parts.SqlClientNamespace);
        }


        [Fact]
        public void Tools_FormatHelper_TemplateParts_SlqClientNamespace_Null_DBMS()
        {
            var parts = GetContainer(null);
            Assert.Empty(parts.SqlClientNamespace);
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_SqlClassName_Empty_DBMS()
        {
            var parts = GetContainer("");
            Assert.Empty(parts.SqlConnectionClassName);
        }


        [Fact]
        public void Tools_FormatHelper_TemplateParts_SqlClassName_MSSQL()
        {
            var parts = GetContainer("mssql");
            Assert.Equal("SqlConnection", parts.SqlConnectionClassName);
        }

        [Fact]
        public void Tools_FormatHelper_TemplateParts_SqlClassName_MySql()
        {
            var parts = GetContainer("mysql");
            Assert.Equal("MySqlConnection", parts.SqlConnectionClassName);
        }


        [Fact]
        public void Tools_FormatHelper_TemplateParts_SqlClassName_Null_DBMS()
        {
            var parts = GetContainer(null);
            Assert.Empty(parts.SqlConnectionClassName);
        }
    }
}