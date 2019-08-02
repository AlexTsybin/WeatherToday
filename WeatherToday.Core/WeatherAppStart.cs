using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherToday.Core.ViewModels.Base;

namespace WeatherToday.Core
{
    public class WeatherAppStart : MvxAppStart
    {
        public WeatherAppStart(IMvxApplication application, IMvxNavigationService navigationService)
            : base(application, navigationService)
        {
        }

        protected async override Task NavigateToFirstViewModel(object hint = null)
        {
            NavigationService.Navigate<WeatherViewModel>();
        }
    }
}
