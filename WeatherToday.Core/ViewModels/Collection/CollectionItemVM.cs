using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherToday.Core.Models.BO.Base;

namespace WeatherToday.Core.ViewModels.Collection
{
    public abstract class CollectionItemVM<TModel> : CollectionItemVM
        where TModel : BaseBO
    {
        public virtual TModel Model { get; set; }

        public CollectionItemVM()
        {
        }

        protected CollectionItemVM(TModel model)
        {
            Model = model;
        }
    }

    public abstract class CollectionItemVM : MvxViewModel
    {
    }
}
