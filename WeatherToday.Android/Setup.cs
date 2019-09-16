﻿using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Content;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Reflection;
using WeatherToday.Android.MvxBindings;
using WeatherToday.Android.Services;
using WeatherToday.Android.Utilities;
using WeatherToday.API.Services.Platform;
using WeatherToday.Core;
using WeatherToday.Core.Services.Platform;
using System;
using System.Threading.Tasks;
using WeatherToday.Android.Presenter;
using MvvmCross.Converters;
using WeatherToday.Android.Converters.Date;

namespace WeatherToday.Android
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(NavigationView).Assembly,
            typeof(CoordinatorLayout).Assembly,
            typeof(FloatingActionButton).Assembly,
            typeof(Toolbar).Assembly,
            typeof(DrawerLayout).Assembly,
            typeof(ViewPager).Assembly,
            typeof(MvxRecyclerView).Assembly,
            typeof(MvxSwipeRefreshLayout).Assembly,
        };

        /// <summary>
        /// Fill the Binding Factory Registry with bindings from the support library.
        /// </summary>
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);

            registry.RegisterFactory(new MvxCustomBindingFactory<SwipeRefreshLayout>("IsRefreshing", (swipeRefreshLayout) => new SwipeRefreshLayoutIsRefreshingTargetBinding(swipeRefreshLayout)));
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new WeatherAndroidPresenter(AndroidViewAssemblies);
            Mvx.IoCProvider.RegisterSingleton<ICustomPresenter>(mvxFragmentsPresenter);

            return mvxFragmentsPresenter;
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            registry.AddOrOverwrite("WeekDayFormat", new WeekDayValueConverter());
            registry.AddOrOverwrite("ShortDateFormat", new ShortDateValueConverter());
        }

        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.RegisterSingleton<IAnalitycsService>(new AnalitycsService());
            Mvx.IoCProvider.RegisterSingleton<IUserInteraction>(new UserInteraction());
            Mvx.IoCProvider.RegisterSingleton<IDeviceService>(new DeviceService());

            base.InitializeFirstChance();
        }
    }
}