using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherToday.Core.ViewModels.Collection;

namespace WeatherToday.Core.ViewModels.Base.Interfaces
{
    public interface IBaseCollectionVM<ItemType> : IBaseVM
        where ItemType : CollectionItemVM
    {
        IMvxAsyncCommand<ItemType> ItemSelectedCommand { get; }

        IMvxAsyncCommand ReloadCommand { get; }

        ObservableCollection<ItemType> Items { get; set; }

        List<ItemType> AllItems { get; }

        bool IsRefreshing { get; set; }

        bool IsItemsAvailable { get; set; }
    }
}
