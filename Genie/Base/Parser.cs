using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var model = new DatabaseModel { Relations = new List<Relation>(), BaseNamespace = config.BaseNamespace, Views = new List<View>(), Procedures = new List<StoredProcedure>()};

                foreach (var databaseTable in schema.Tables)
                {
                    model.Relations.Add(ParseRelation(databaseTable));
                }

                foreach (var view in schema.Views)
                {
                    model.Views.Add(ParseView(view));
                }

                foreach (var sp in schema.StoredProcedures)
                {
                    model.Procedures.Add(ParseProcedure(sp));
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
                Attributes = new List<Attribute>(),
                ForeignKeyAttributes = new List<ForeignKeyAttribute>()
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

                    var fkAttribute = new ForeignKeyAttribute
                    {
                        DataType = column.ForeignKeyTableName,
                        FieldName = attribute.FieldName + "obj",
                        Name = "Get"+ attribute.Name,
                        ReferencingAttribute = attribute
                    };

                    realation.ForeignKeyAttributes.Add(fkAttribute);
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

        private static View ParseView(DatabaseTable databaseView)
        {
            var view = new View
            {
                RelationName = databaseView.Name,
                Name = databaseView.Name,
                Attributes = new List<Attribute>()
            };

            foreach (var column in databaseView.Columns)
            {
                var attribute = new Attribute
                {
                    Name = column.Name,
                    DataType = column.DataType.NetDataTypeCSharpName,
                };
                view.Attributes.Add(attribute);
            }

            return view;
        }
        private static StoredProcedure ParseProcedure(DatabaseStoredProcedure databaseSp)
        {

            var parameters = databaseSp.Arguments;
            var parameterString = parameters.Aggregate("", (current, param) => current + string.Format("{0} {1}", param.DataType.NetDataTypeCSharpName, param.Name.Replace("@","")) + ",");
            var parameterPassString = parameters.Aggregate("", (current, param) => current + string.Format("{1} = '\"+{0}+\"'", param.Name.Replace("@", ""), param.Name) + ",");

            return new StoredProcedure
            {
                Name = databaseSp.Name,
                FullName = databaseSp.FullName,
                ParamString = parameterString.TrimEnd(','),
                PassString = parameterPassString.TrimEnd(',')
            };
        }
    }
}
