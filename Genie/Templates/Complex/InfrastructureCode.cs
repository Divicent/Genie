
using Genie.Models;

namespace Genie.Templates.Complex
{
    public partial class Infrastructure
    {
        private DatabaseModel Model { get; set; }

        internal string TransformText(DatabaseModel model)
        {
            Model = model;
            return TransformText();
        }
    }
}
