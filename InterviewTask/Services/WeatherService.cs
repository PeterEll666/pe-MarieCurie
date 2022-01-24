using InterviewTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace InterviewTask.Services
{
    public class WeatherService : IWeatherService
    {
        public WeatherModel GetWeather(int cityId)
        {
            using (var client = new HttpClient())
            {
                string url = "https://api.openweathermap.org/data/2.5/weather?units=metric&appid=9333356f4951733069fb98f9af82c464&id="
                                + cityId.ToString();
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<WeatherModel>().Result;
            }
        }
    }
}