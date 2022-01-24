using InterviewTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace InterviewTask.Services
{
    public class WeatherService : IWeatherService
    {
        string apiToken = ConfigurationManager.AppSettings["weatherAPIToken"];
        public WeatherModel GetWeather(int cityId)
        {
            using (var client = new HttpClient())
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?units=metric&appid={apiToken}&id={cityId}";
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<WeatherModel>().Result;
            }
        }
    }
}