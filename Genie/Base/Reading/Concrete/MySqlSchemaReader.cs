using Genie.Base.Configuration.Abstract;
using Genie.Base.ProcessOutput.Abstract;
using Genie.Base.Reading.Abstract;

namespace  Genie.Base.Reading.Concrete 
{
    internal class MySqlSchemaReader : IDatabaseSchemaReader
    {
        public IDatabaseSchema Read(IConfiguration configuration, IProcessOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}