using System;
using System.Collections.Generic;
using System.Linq;
using Genie.Base.Abstract;
using Genie.Models.Abstract;
using Genie.Templates.Dapper;
using Genie.Templates.Extensions;
using Genie.Templates.General;
using Genie.Templates.General.Interfaces;
using Genie.Templates.Infrastructure;
using Genie.Templates.Infrastructure.Enum;
using Genie.Templates.Infrastructure.EnumQueriesStoredProcedures;
using Genie.Templates.Infrastructure.Interfaces;
using Genie.Templates.Infrastructure.Models;
using Genie.Templates.SqlMaker;

namespace Genie.Base
{
    internal class DalGenerator : IDalGenerator
    {
        public List<IContentFile> Generate(IDatabaseSchema schema, IBasicConfiguration configuration, IProcessOutput output)
        {

            output.WriteInformation("Generating files.");
            List<ITemplateFile> files;
            try
            {
                files = new List<ITemplateFile>
                {
                    new SqlMapper(@"Dapper\SqlMapper"),
                    new CustomPropertyTypeMap(@"Dapper\CustomPropertyTypeMap"),
                    new DbString(@"Dapper\DbString"),
                    new DefaultTypeMap(@"Dapper\DefaultTypeMap"),
                    new DynamicParameters(@"Dapper\DynamicParameters"),
                    new FeatureSupport(@"Dapper\FeatureSupport"),
                    new ISqlAdapter(@"Dapper\ISqlAdapter"),
                    new KeyAttribute(@"Dapper\KeyAttribute"),
                    new PostgresAdapter(@"Dapper\PostgresAdapter"),
                    new SimpleMemberMap(@"Dapper\SimpleMemberMap"),
                    new SqlMapperExtensions(@"Dapper\SqlMapperExtensions"),
                    new SqlServerAdapter(@"Dapper\SqlServerAdapter"),
                    new TableAttribute(@"Dapper\TableAttribute"),
                    new WriteAttribute(@"Dapper\WriteAttribute"),

                    new EnumBase(@"General\EnumBase"),
                    new IEnumBase(@"General\Interfaces\IEnumBase"),

                    new ConditionExtension(@"Infrastructure\Enum\ConditionExtension"),
                    new QueriesAndEnum(schema.Relations, @"Infrastructure\EnumQueriesStoredProcedures\QueriesAndEnum"),
                    new IDapperContext(@"Infrastructure\Interfaces\IDapperContext"),
                    new IFactoryRepository(@"Infrastructure\Interfaces\IFactoryRepository"),
                    new IRepository(@"Infrastructure\Interfaces\IRepository"),
                    new IUnitOfWork(@"Infrastructure\Interfaces\IUnitOfWork"),
                    new IViewRepository(@"Infrastructure\Interfaces\IViewRepository"),

                    new DapperContext(@"Infrastructure\DapperContext"),
                    new FactoryRepository(@"Infrastructure\FactoryRepository"),
                    new Repository(@"Infrastructure\Repository"),
                    new UnitOfWork(schema, @"Infrastructure\UnitOfWork"),
                    new ViewRepository(@"Infrastructure\ViewRepository"),

                    new QueryMaker(@"SqlMaker/SqlMaker"),
                    new QueryMaker(@"SqlMaker/Interfaces/ISqlFirst"),
                    new QueryMaker(@"SqlMaker/Interfaces/ISqlMaker"),
                    new QueryMaker(@"SqlMaker/Interfaces/ISqlMakerDelete"),
                    new QueryMaker(@"SqlMaker/Interfaces/ISqlMakerInsert"),
                    new QueryMaker(@"SqlMaker/Interfaces/ISqlMakerSelect"),
                    new QueryMaker(@"SqlMaker/Interfaces/ISqlMakerUpdate"),
                    new QueryMaker(@"SqlMaker/Interfaces/ISqlMkrBase")
                };

                files.AddRange(schema.Relations
                    .Select(relation => new Relation(relation, @"Infrastructure\Models\" + relation.Name)));

                files.AddRange(schema.Views
                    .Select(view => new View(view, @"Infrastructure\Models\" + view.Name)));

                output.WriteInformation(string.Format("{0} file found.", files.Count));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to create list of template files.", e);
            }

            try
            {
                GenerationContext.BaseNamespace = configuration.BaseNamespace;
                output.WriteInformation("Generating File content.");
                var contentFiles =
                    files.Select(templateFile => templateFile.Generate()).ToList();

                output.WriteSuccess(string.Format("Successfully generated {0} files.", contentFiles.Count));

                foreach (var contentFile in contentFiles)
                    contentFile.Path = contentFile.Path + "." + "cs";

                return contentFiles;

            }
            catch (Exception e)
            {
                throw new Exception("Unable to generate file content", e);
            }
           
        }
    }
}
