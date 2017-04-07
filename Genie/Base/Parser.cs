using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseSchemaReader.DataSchema;
using Genie.Models;
using Attribute = Genie.Models.Attribute;

namespace Genie.Base
{
    internal class Parser
    {
        internal static DatabaseModel Parse(DatabaseSchema schema, GenieConfiguration config)
        {
            try
            {
                var model = new DatabaseModel { Relations = new List<Relation>(), BaseNamespace = config.BaseNamespace, ConnectionName = config.ConnectionName };

                foreach (var databaseTable in schema.Tables)
                {
                    model.Relations.Add(ParseRelation(databaseTable));
                }

                return model;
            }
            catch (Exception e)
            {
                
                throw new Exception("Unable to parse model", e);
            }
        }

        private static Relation ParseRelation(DatabaseTable table)
        {
            var realation = new Relation
            {
                RelationName = table.Name,
                Name = table.Name,
                Attributes = new List<Attribute>()
            };

            foreach (var column in table.Columns)
            {
                if (column.IsForeignKey)
                {
                    var attribute = new Attribute
                    {
                        IsKey = column.IsPrimaryKey,
                        Name = column.Name,
                        DataType = column.DataType.NetDataTypeCSharpName,
                        FieldName = "_" + (column.Name.First() + "").ToLower() + column.Name.Substring(1)
                    };
                    realation.Attributes.Add(attribute);
                }
                else
                {
                    var attribute = new Attribute
                    {
                        IsKey = column.IsPrimaryKey,
                        Name = column.Name,
                        DataType = column.DataType.NetDataTypeCSharpName,
                        FieldName = "_" + (column.Name.First() + "").ToLower() + column.Name.Substring(1)
                    };
                    realation.Attributes.Add(attribute);
                }
            }

            return realation;
        }
    }
}
