using Genie.Models.Abstract;

namespace Genie.Templates.Extensions
{
    /// <summary>
    /// represents a generate able template 
    /// </summary>
    public interface ITemplateFile
    {
        /// <summary>
        /// Generate the template
        /// </summary>
        /// <returns></returns>
        IContentFile Generate();
    }
}
