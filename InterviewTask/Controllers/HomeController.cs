using InterviewTask.Models;
using InterviewTask.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InterviewTask.Controllers
{
    public class HomeController : Controller
    {
        private IHelperServiceRepository _helperServiceRepository;
        private ILogService _logService;
        private IWeatherService _weatherService;
        public HomeController(IHelperServiceRepository helperServiceRepository, ILogService logService, IWeatherService weatherService)
        {
            _helperServiceRepository = helperServiceRepository;
            _logService = logService;
            _weatherService = weatherService;
        }
        /*
         * Prepare your opening times here using the provided HelperServiceRepository class.       
         */
        public ActionResult Index()
        {
            try
            {
                _logService.LogMessage($"HomeController Index action called from ip {Request.UserHostAddress}");
                var model = new List<HelperServiceViewModel>();
                foreach (var service in _helperServiceRepository.Get())
                {
                    model.Add(GetServiceViewModel(service));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logService.LogMessage($"Exception in Home Controller Index action {ex.ToString()}");
                throw ex;
            }
        }

        public ActionResult GetWeather(int cityId)
        {
            var weather = _weatherService.GetWeather(cityId);
            var weatherViewModel = new WeatherViewModel
            {
                Description = weather.Weather[0].Description,
                Main = weather.Weather[0].Main,
                Temp = weather.Main.Temp,
                FeelsLike = weather.Main.FeelsLike,
                WindSpeed = weather.Wind.Speed
            };
            return PartialView("WeatherPartial", weatherViewModel);
        }

        private HelperServiceViewModel GetServiceViewModel(HelperServiceModel service)
        {
            var viewModel = new HelperServiceViewModel
            {
                Description = service.Description,
                TelephoneNumber = service.TelephoneNumber,
                Title = service.Title,
                OpenText = string.Empty,
                City = service.City,
                CityId =service.CityId
            };

            var timeNow = DateTime.Now;
            var openingHours = GetOpeningHoursByDayOfWeek(service, timeNow.DayOfWeek);
            if (openingHours==null)
            {
                viewModel.OpenText = "Unable to display opening hours at present";
                return viewModel;
            }
            var openAt = timeNow.Date.AddHours(openingHours[0]);
            var closeAt = timeNow.Date.AddHours(openingHours[1]);
            if (timeNow >= openAt && timeNow <= closeAt)
            {
                viewModel.IsOpenNow = true;
                viewModel.OpenText = $"Open Today Until {closeAt.ToString("htt").ToLower()}";
            }
            else
            {
                var nextDay = timeNow.Date.AddDays(1);
                while (nextDay < timeNow.Date.AddDays(7))
                {
                    var nextOpeningHours = GetOpeningHoursByDayOfWeek(service, nextDay.DayOfWeek);
                    if (nextOpeningHours[1] != 0)
                    {
                        var nextOpenAt = nextDay.AddHours(nextOpeningHours[0]);
                        viewModel.OpenText = $"Reopens {nextDay.ToString("dddd")} at {nextOpenAt.ToString("htt").ToLower()}";
                        break;
                    }
                    nextDay = nextDay.Date.AddDays(1);
                }
                if (string.IsNullOrEmpty(viewModel.OpenText))
                {
                    viewModel.OpenText = "Unable to display opening hours at present";
                }
            }
            return viewModel;
        }

        private List<int> GetOpeningHoursByDayOfWeek(HelperServiceModel service, DayOfWeek dayOfWeek)
        {
            switch(dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return service.MondayOpeningHours;
                case DayOfWeek.Tuesday:
                    return service.TuesdayOpeningHours;
                case DayOfWeek.Wednesday:
                    return service.WednesdayOpeningHours;
                case DayOfWeek.Thursday:
                    return service.ThursdayOpeningHours;
                case DayOfWeek.Friday:
                    return service.FridayOpeningHours;
                case DayOfWeek.Saturday:
                    return service.SaturdayOpeningHours;
                case DayOfWeek.Sunday:
                    return service.SundayOpeningHours;
                default:
                    return new List<int> { 0, 0 };
            }
        }

    }
}