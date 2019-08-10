using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.Core.Messages
{
    public class CitiesUpdatedMessage : MvxMessage
    {
        public CitiesUpdatedMessage(object sender) : base(sender)
        {
        }
    }
}
