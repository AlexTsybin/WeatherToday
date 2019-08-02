using MvvmCross;
using MvvmCross.ViewModels;

namespace WeatherToday.API
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterSingleton<IAppService>(new AppService());
        }
    }
}
