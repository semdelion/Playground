using Xamarin.Essentials;

namespace Semdelion.Core.User
{
    public static class Settings
    {
        public static bool ModeNight
        {
            get => Preferences.Get("ModeNight", false);
            set => Preferences.Set("ModeNight", value);
        } 
    }
}
