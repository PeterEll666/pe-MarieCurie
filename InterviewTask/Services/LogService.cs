using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace InterviewTask.Services
{
    public class LogService : ILogService
    {
        string fileName = ConfigurationManager.AppSettings["logFileLocation"];
        public void LogMessage(string message)
        {
            File.AppendAllText(fileName, $"{DateTime.Now.ToString()} {message}\n\r");
        }
    }
}