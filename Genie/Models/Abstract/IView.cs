#region Usings

using System.Collections.Generic;

#endregion

namespace Genie.Core.Models.Abstract
{
  /// <summary>
  ///     Represents a view in a database
  /// </summary>
  public interface IView : IModel
    {
      /// <summary>
      ///     Name of the view
      /// </summary>
      string Name { get; set; }

      /// <summary>
      ///     List of attributes (resulting columns) of the view
      /// </summary>
      List<ISimpleAttribute> Attributes { get; set; }

      /// <summary>
      ///     The Field Name
      /// </summary>
      string FieldName { get; set; }
    }
}