using System.Collections.Generic;
using Genie.Base.Abstract;
using Genie.Base.Configuration.Abstract;
using Genie.Base.Reader.Abstract;
using Genie.Models.Abstract;

namespace Genie.Base
{
    internal class ContentGenerator : IContentGenerator
    {
        public List<IContentFile> Generate(IDatabaseSchema schema, IConfiguration configuration)
        {
            return new List<IContentFile>();
        }
    }
}
