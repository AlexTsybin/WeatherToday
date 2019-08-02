using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace WeatherToday.API
{
    public class AppService : IAppService
    {
        private Binding binding;
        private EndpointAddress endpoint;

        public bool IsServiceUp { get { return binding != null && endpoint != null; } }
    }
}
