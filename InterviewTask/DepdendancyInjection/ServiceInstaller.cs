using Castle.MicroKernel.Registration;
using InterviewTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.DepdendancyInjection
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container,
        Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<IHelperServiceRepository>()
                    .ImplementedBy<HelperServiceRepository>()
                    .LifestyleSingleton());
            container.Register(
                Component
                    .For<ILogService>()
                    .ImplementedBy<LogService>()
                    .LifestyleSingleton());
            container.Register(
                Component
                    .For<IWeatherService>()
                    .ImplementedBy<WeatherService>()
                    .LifestyleSingleton());
        }
    }
}
