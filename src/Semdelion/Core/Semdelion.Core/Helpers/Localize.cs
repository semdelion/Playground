namespace Semdelion.Core.Helpers
{
    using MvvmCross;
    using Semdelion.Core.Providers.Interfaces;

    /// <summary>
    /// 	Класс для работы с локализацией.
    /// </summary>
    public static class Localize
	{
		private static IMvxLocalizationProvider _provider;

		/// <summary>
		/// 	Провайдер локализации.
		/// </summary>
		public static IMvxLocalizationProvider Provider => _provider ??= Mvx.IoCProvider.Resolve<IMvxLocalizationProvider>();


		/// <summary>
		/// 	Получить локализованный текст.
		/// </summary>
		/// <param name="key">Ключ локализации.</param>
		/// <returns>Локализованный текст.</returns>
		public static string GetText(string key)
		{
			return Provider.GetText(key);
		}
	}
}
