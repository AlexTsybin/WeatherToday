using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using WeatherToday.Core.ViewModels;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Weather;

namespace WeatherToday.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            new API.App().Initialize();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterCustomAppStart<WeatherAppStart<MainViewModel>>();
        }
    }
}
