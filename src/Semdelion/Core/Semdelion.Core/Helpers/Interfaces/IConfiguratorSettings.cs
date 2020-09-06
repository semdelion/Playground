namespace Semdelion.Core.Helpers.Interfaces
{
    using System.Reflection;

    /// <summary>
    ///    Configurator settings.
    /// </summary>
    public interface IConfiguratorSettings
    {
        /// <summary>
        ///     Building the kernel.
        /// </summary>
        Assembly CoreAssembly { get; set; }

        /// <summary>
        ///     Configs folder.
        /// </summary>
        string Folder { get; set; }

        /// <summary>
        ///     Data parser.
        /// </summary>
        IConfiguratorParser Parser { get; set; }
    }
}
