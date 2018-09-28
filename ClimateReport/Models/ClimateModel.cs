using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;//added by me 
namespace ClimateReport.Models
{
    public class ClimateModel
    {
        public string apiResponse { get; set; }
        public Dictionary<string, string> cities
        {
            get;
            set;
        }
    }
}