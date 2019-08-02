﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherToday.Core.Services.Implementation
{
    public class CalculationService : ICalculationService
    {
        public double TipAmount(double subTotal, int generosity)
        {
            return subTotal * ((double)generosity) / 100.0;
        }
    }
}
