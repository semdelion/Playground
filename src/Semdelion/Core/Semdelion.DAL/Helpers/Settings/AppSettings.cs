namespace Semdelion.DAL.Helpers.Settings
{
    using MvvmCross;
    using Semdelion.DAL.Helpers.Interfaces;

    /// <summary>
    ///    Aplication settings.
    /// </summary>
    public class AppSettings : IAppSettings
    {
        private static IAppSettings _appSettings;
        public static IAppSettings Current => _appSettings ??= Mvx.IoCProvider.Resolve<IAppSettings>();

        public IEnvironment Environment { get; set; }

        public AppSettings(IEnvironment environment)
        {
            Environment = environment;
        }
    }
}
