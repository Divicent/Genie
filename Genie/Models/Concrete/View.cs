#region Usings

using System.Collections.Generic;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
  internal class View : IView
  {
    public string Comment { get; set; }
    public string FieldName { get; set; }
    public string Name { get; set; }
    public List<ISimpleAttribute> Attributes { get; set; }

    public IEnumerable<ISimpleAttribute> GetAttributes()
    {
      return Attributes;
    }

    public IEnumerable<IForeignKeyAttribute> GetForeignKeyAttributes()
    {
      return new List<IForeignKeyAttribute>();
    }

    public string GetName()
    {
      return Name;
    }

    public IEnumerable<IReferenceList> GetReferenceLists()
    {
      return new List<IReferenceList>();
    }
  }
}