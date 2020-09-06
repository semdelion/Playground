namespace Semdelion.Core.Helpers.Interfaces
{  
    /// <summary>
    ///     Parser for the project configurator.
    /// </summary>
    public interface IConfiguratorParser
    {
        /// <summary>
        ///     Get model from content.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <typeparam name="T">Type model.</typeparam>
        /// <returns>Converted model.</returns>
        T Get<T>(string content);
    }
}
