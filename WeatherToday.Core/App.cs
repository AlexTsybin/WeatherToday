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
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //RegisterAppStart<WeatherListVM>();
            RegisterCustomAppStart<WeatherAppStart>();
        }
    }
}
