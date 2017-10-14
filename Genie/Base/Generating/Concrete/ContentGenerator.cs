using System.Collections.Generic;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating.Absract;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Models.Abstract;

namespace Genie.Core.Base.Generating.Concrete
{
    internal class ContentGenerator : IContentGenerator
    {
        public List<IContentFile> Generate(IDatabaseSchema schema, IConfiguration configuration)
        {
            return new List<IContentFile>();
        }
    }
}