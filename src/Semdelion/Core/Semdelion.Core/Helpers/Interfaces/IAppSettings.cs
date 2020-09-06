namespace Semdelion.Core.Helpers.Interfaces
{
    /// <summary>
    ///     Application settings.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        ///     Settings.
        /// </summary>
        IEnvironment Environment { get; set; }
    }
}
