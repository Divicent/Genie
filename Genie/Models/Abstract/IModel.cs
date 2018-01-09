using System.Collections.Generic;

namespace Genie.Core.Models.Abstract
{
    public interface IModel
    {
      /// <summary>
      ///     Get all attributes
      /// </summary>
      /// <returns></returns>
      IEnumerable<ISimpleAttribute> GetAttributes();

      /// <summary>
      ///     Get name of the model
      /// </summary>
      /// <returns></returns>
      string GetName();

      /// <summary>
      ///     Get all foreign key attributes
      /// </summary>
      /// <returns></returns>
      IEnumerable<IForeignKeyAttribute> GetForeignKeyAttributes();

      /// <summary>
      ///     Get reference lists if available
      /// </summary>
      /// <returns></returns>
      IEnumerable<IReferenceList> GetReferenceLists();
    }
}