namespace Semdelion.Core.Helpers.Interfaces
{
    using System.Reflection;

    public interface IEnvironment
    {
        /// <summary>
        ///     BaseUrl.
        /// </summary>
        string BaseUrl { get; }
    }

    /// <summary>
    ///     Настройка парсинга основных конфигов.
    /// </summary>
    public interface IEnvironmentSettings
    {
        /// <summary>
        ///     В каком <see cref="Assembly"/> искать конфиг.
        /// </summary>
        Assembly CoreAssembly { get; set; }

        /// <summary>
        ///     File name.
        /// </summary>
        string Filename { get; set; }

        /// <summary>
        ///     Folder with configs.
        /// </summary>
        string Folder { get; set; }
    }
}
