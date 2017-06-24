using System.Collections.Generic;
using Genie.Base.Configuration.Abstract;
using Genie.Base.Generating.Absract;
using Genie.Base.Reading.Abstract;
using Genie.Models.Abstract;

namespace Genie.Base.Generating.Concrete
{
    internal class ContentGenerator : IContentGenerator
    {
        public List<IContentFile> Generate(IDatabaseSchema schema, IConfiguration configuration)
        {
            return new List<IContentFile>();
        }
    }
}
