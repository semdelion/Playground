using Playground.Core.ViewModels.Pager;
using Playground.Core.ViewModels.Tabs;
﻿using MvvmCross;
using MvvmCross.IoC;
using Playground.Core.Providers;
using Playground.Core.Services;
using Playground.Core.ViewModels;
using Semdelion.Core;
using Semdelion.DAL.Services.Decorators;

namespace Playground.Core
{
    public class PlaygroundApp: App 
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterAppStart<PagesRootViewModel>();
            Mvx.IoCProvider.RegisterType<IContactService, ContactService>();

            var serviceDecorator = new ServiceDecorator();
            ServiceDecorator.AddDefaultFilters(serviceDecorator);

            Mvx.IoCProvider.RegisterSingleton<IServiceDecorator>(serviceDecorator);

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IContactProvider, ContactProvider>();

            RegisterAppStart<MainViewModel>();
        }
    }
}
