using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;

namespace WeatherToday.Core.Messages.Common
{
    public class UpdateViewModelMessage : MvxMessage
    {
        public List<Type> UpdateTypes { get; set; }

        public bool HardUpdate { get; set; }

        public UpdateViewModelMessage(object sender) : base(sender)
        {
        }
    }
}
