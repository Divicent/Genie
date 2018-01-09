using Genie.Core.Models.Abstract.SchemaCaching;

namespace Genie.Core.Models.Concrete.SchemaCaching
{
    public class SchemaCacheRootObject: ISchemaCacheRootObject
    {
        public string GenieVersion { get; set; }
        public string BaseNamespace { get; set; }
        public bool IsCore { get; set; }
        public bool NoDapper { get; set; }
        public string Schema { get; set; }
    }
}