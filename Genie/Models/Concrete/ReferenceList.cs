#region Usings

using DotLiquid;
using Genie.Core.Extensions;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    public class ReferenceList : IReferenceList, ILiquidizable
    {
        public string ReferencedRelationName { get; set; }
        public string ReferencedPropertyName { get; set; }
        public string ReferencedPropertyOnThisRelation { get; set; }
        public object ToLiquid()
        {
            return new
            {
                ReferencedRelationName,
                ReferencedPropertyName,
                ReferencedPropertyOnThisRelation,
                ReferencedRelationNamePlural = ReferencedRelationName.ToPlural()
            };
        }
    }
}