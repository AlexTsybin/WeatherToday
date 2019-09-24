using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters;
using System.Collections.Generic;
using System.Reflection;
using WeatherToday.Android.MvxBindings;
using WeatherToday.Android.Services;
using WeatherToday.API.Services.Platform;
using WeatherToday.Core;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Android.Presenter;
using MvvmCross.Converters;
using WeatherToday.Android.Converters.Date;
using Android.Widget;
using WeatherToday.Android.Bindings;
using WeatherToday.Android.Converters.String;

namespace WeatherToday.Android
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(NavigationView).Assembly,
            typeof(CoordinatorLayout).Assembly,
            typeof(FloatingActionButton).Assembly,
            typeof(global::Android.Support.V7.Widget.Toolbar).Assembly,
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
            registry.RegisterFactory(new MvxCustomBindingFactory<ImageView>("WeatherImage", (view) => new WeatherImageBinding(view)));
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

            registry.AddOrOverwrite("HourFormat", new HourValueConverter());
            registry.AddOrOverwrite("WeekDayFormat", new WeekDayValueConverter());
            registry.AddOrOverwrite("ShortDateFormat", new ShortDateValueConverter());
            registry.AddOrOverwrite("LongDateFormat", new LongDateValueConverter());
            registry.AddOrOverwrite("TimeDateFormat", new TimeValueConverter());

            registry.AddOrOverwrite("CelsiusFormat", new CelsiusValueConverter());
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