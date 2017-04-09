namespace Genie.Base.Abstract
{
    /// <summary>
    /// Helps to clear the target folder before generating 
    /// </summary>
    internal interface IObstacleManager
    {
        /// <summary>
        /// Clears the provided folder
        /// </summary>
        /// <param name="basePath">folder path</param>
        /// <param name="output">a process output to use</param>
        void Clear(string basePath, IProcessOutput output);
    }
}
