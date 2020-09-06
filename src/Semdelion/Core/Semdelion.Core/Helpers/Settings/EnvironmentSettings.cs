namespace Semdelion.Core.Helpers.Settings
{
    using Semdelion.Core.Helpers.Interfaces;
    using System.Reflection;

    public class EnvironmentSettings : IEnvironmentSettings
    {
        /// <inheritdoc />
        public Assembly CoreAssembly { get; set; }

        /// <inheritdoc />
        public string Filename { get; set; } = "config.yaml";

        /// <inheritdoc />
        public string Folder { get; set; } = "Configs";

        public EnvironmentSettings(Assembly coreAssembly)
        {
            CoreAssembly = coreAssembly;
        }
    }
}
