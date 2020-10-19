namespace Semdelion.DAL.Helpers.Interfaces
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
