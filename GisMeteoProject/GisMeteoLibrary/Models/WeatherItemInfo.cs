﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Models
{
    public class WeatherItemInfo
    {
        public string WeatherCondition { get; set; }
        public string Date { get; set; }
        public string TemperatureMin { get; set; }
        public string TemperatureMax { get; set; }
        public string Precipitation { get; set; }
    }
}