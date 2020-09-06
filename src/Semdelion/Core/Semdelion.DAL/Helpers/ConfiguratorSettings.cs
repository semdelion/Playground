namespace Semdelion.DAL.Helpers
{
    using Semdelion.DAL.Helpers.Interfaces;
    using System.Reflection;

    /// <summary>
    ///     Configurator settings.
    /// </summary>
    public class ConfiguratorSettings : IConfiguratorSettings
    {
        /// <inheritdoc />
        public Assembly CoreAssembly { get; set; }

        /// <inheritdoc />
        public string Folder { get; set; }

        /// <inheritdoc />
        public IConfiguratorParser Parser { get; set; }
    }
}
