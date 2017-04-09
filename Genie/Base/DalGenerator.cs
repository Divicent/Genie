using System;
using System.Collections.Generic;
using Genie.Base.Abstract;
using Genie.Models.Abstract;
using Genie.Templates.Dapper;
using Genie.Templates.Extensions;
using Genie.Templates.SqlMaker.Interfaces;

namespace Genie.Base
{
    internal class DalGenerator : IDalGenerator
    {
        public List<IContentFile> Generate(IDatabaseSchema schema, IBasicConfiguration configuration)
        {
            var files = new List<ITemplateFile>();
            files.Add(new SqlMapper());
        }
    }
}
