#region Usings

using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    public class Relation : IRelation, ILiquidizable
    {
        public string Name { get; set; }
        public List<IAttribute> Attributes { get; set; }
        public List<IForeignKeyAttribute> ForeignKeyAttributes { get; set; }
        public List<IReferenceList> ReferenceLists { get; set; }
        public string FieldName { get; set; }
        public string Comment { get; set; }

        public IEnumerable<ISimpleAttribute> GetAttributes()
        {
            return Attributes;
        }

        public string GetName()
        {
            return Name;
        }

        public IEnumerable<IForeignKeyAttribute> GetForeignKeyAttributes()
        {
            return ForeignKeyAttributes;
        }

        public IEnumerable<IReferenceList> GetReferenceLists()
        {
            return ReferenceLists;
        }

        public object ToLiquid()
        {
            var keys  = Attributes.Where(a => a.IsKey).ToList();
            var hasKeys = keys.Count > 0;
            var keyString = "";
            var keyCommentString = "";
            var keyGetter = "";
            var removeKeys = "";
            if (hasKeys)
            {
                keyString = keys.Aggregate("", (c, n) => c + ", " + n.DataType + " " + n.Name.ToLower())
                    .TrimStart(',').TrimStart(' ');
                keyCommentString = keys.Aggregate("", (c, n) => c +
                                                                $@"
            /// <param name=""{n.Name.ToLower()}"">Value for primary key {n.Name}</param>");

                keyGetter = keys.Aggregate("", (c, n) => c + ".And." + n.Name + ".EqualsTo(" + n.Name.ToLower() + ")")
                    .TrimStart('.').TrimStart('A').TrimStart('n').TrimStart('d');
                removeKeys = keys.Aggregate("", (c, n) => $"{c}                        {n.Name} = {n.Name.ToLower()},\n").TrimEnd('\n');
            }

            return new
            {
                Name,
                Attributes,
                ForeignKeyAttributes,
                ReferenceLists,
                FieldName,
                Comment,
                keyString,
                keyCommentString,
                keyGetter,
                removeKeys
            };
        }
    }
}