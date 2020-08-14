namespace Semdelion.Core.Extensions
{
    using MvvmCross;
    using MvvmCross.Binding.Binders;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Localization;

    public static class MvxFluentBindingDescriptionExtensions
    {
        public static MvxFluentBindingDescription<TTarget, TSource> ToFlyLocalizationId<TTarget, TSource>(
            this MvxFluentBindingDescription<TTarget, TSource> bindingDescription,
            string localizationId)
            where TSource : IMvxLocalizedTextSourceOwner
            where TTarget : class
        {
            var valueConverter = Mvx.IoCProvider.Resolve<IMvxValueConverterLookup>().Find("Language");
            return bindingDescription.To(vm => vm.LocalizedTextSource)
                .OneWay()
                .WithConversion(valueConverter, localizationId);
        }
    }
}
