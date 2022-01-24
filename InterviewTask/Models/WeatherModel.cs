using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.Models
{
    public class WeatherModel
    {
        public List<WeatherDesciptionModel> Weather { get; set; }
        public WeatherMainModel Main { get; set; }
        public WeatherWindModel Wind { get; set; }
    }

}