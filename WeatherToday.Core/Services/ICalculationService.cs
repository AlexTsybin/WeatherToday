using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.Core.Services
{
    public interface ICalculationService
    {
        double TipAmount(double subTotal, int generosity);
    }
}
