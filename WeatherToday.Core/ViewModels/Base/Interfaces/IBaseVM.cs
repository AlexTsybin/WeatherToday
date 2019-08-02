using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.Core.ViewModels.Base.Interfaces
{
    public interface  IBaseVM : IMvxViewModel
    {
        bool Loading { get; set; }

        string PageTitle { get; set; }

        string PageSubtitle { get; set; }

        void Unbind();

        void Reload();

        void CloseVM();
    }
}
