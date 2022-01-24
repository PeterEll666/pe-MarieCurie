using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.Models
{
    public class HelperServiceViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TelephoneNumber { get; set; }
        public string City { get; set; }
        public int CityId { get; set; }
        public bool IsOpenNow { get; set; }
        public string OpenText { get; set; }
    }
}