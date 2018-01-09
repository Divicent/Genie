using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Files.Abstract;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Models.Abstract;
using Genie.Core.Models.Abstract.SchemaCaching;

namespace Genie.Core.Base.SchemaCaching.Abstract
{
    public interface ISchemaCachingManager
    {
        /// <summary>
        /// This will create a schema file if it does n not exist
        /// and will make sure the schema file is correct
        /// </summary>
        /// <param name="schema">Schema to write</param>
        void SetupCache(IDatabaseSchema schema);

        /// <summary>
        /// This will convert the given schema to a cache object
        /// </summary>
        /// <param name="schema">Schema to use</param>
        /// <returns>a cache object</returns>
        object ConvertSchemaToSchemaCache(IDatabaseSchema schema);

        /// <summary>
        /// This will create the root part of the catch object
        /// </summary>
        /// <returns></returns>
        ISchemaCacheRootObject CreateRootObject();

        /// <summary>
        /// Create cache object for a single model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IModelCacheObject CreateModelCacheObject(IModel model);

    }
}
