using ClimateReport.Class;
using ClimateReport.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ClimateReport.Controllers
{
    public class ClimateMapController : Controller
    {
        // GET: ClimateMap
        //public ActionResult Index()
        //{
        //    return View();
        //}




        public ActionResult Index()
        {
            ClimateModel openWeatherMap = FillCity();
            return View(openWeatherMap);
        }

        public ClimateModel FillCity()
        {
            ClimateModel openWeatherMap = new ClimateModel();
            openWeatherMap.cities = new Dictionary<string, string>();
            openWeatherMap.cities.Add("Mumbai", "1275339");
            openWeatherMap.cities.Add("New Delhi", "1261481");
            openWeatherMap.cities.Add("Bangalore", "1277333");
            openWeatherMap.cities.Add("Bhubaneshwar", "1275817");
            openWeatherMap.cities.Add("Khurda", "1266616");
            openWeatherMap.cities.Add("Cuttack", "1273780");
            openWeatherMap.cities.Add("Hyderabad", "1269843");
            openWeatherMap.cities.Add("Philadelphia", "4560349");
            return openWeatherMap;
        }


        [HttpPost]
        public ActionResult Index(string cities)
        {
            ClimateModel openWeatherMap = FillCity();

            if (cities != null)
            {

                string apiKey = "61cb374e7df0bee2ff6d3cc46f278eb1";
                HttpWebRequest apiRequest = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?id=" + cities + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;
                //apiRequest.UseDefaultCredentials = true;// added by me to solve the error
                //apiRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;// added by me to solve the error
                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }

                ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);//

                StringBuilder sb = new StringBuilder();
                sb.Append("<table><tr><th>Weather Description</th></tr>");
                sb.Append("<tr><td>City:</td><td>" + rootObject.name + "</td></tr>");
                sb.Append("<tr><td>Country:</td><td>" + rootObject.sys.country + "</td></tr>");
                sb.Append("<tr><td>Country Sun Rise:</td><td>" + rootObject.sys.sunrise + "</td></tr>");
                sb.Append("<tr><td>Country Sun Sete:</td><td>" + rootObject.sys.sunset + "</td></tr>");
                sb.Append("<tr><td>Wind:</td><td>" + rootObject.wind.speed + " Km/h</td></tr>");
                sb.Append("<tr><td>Current Temperature:</td><td>" + rootObject.main.temp + " °C</td></tr>");
                sb.Append("<tr><td>Max. Temperature:</td><td>" + rootObject.main.temp_max + " °C</td></tr>");
                sb.Append("<tr><td>Min. Temperature:</td><td>" + rootObject.main.temp_min + " °C</td></tr>");
                sb.Append("<tr><td>Pressure:</td><td>" + rootObject.main.pressure + "</td></tr>");
                sb.Append("<tr><td>Humidity:</td><td>" + rootObject.main.humidity + "</td></tr>");
                sb.Append("<tr><td>Weather:</td><td>" + rootObject.weather[0].description + "</td></tr>");
                sb.Append("</table>");
                openWeatherMap.apiResponse = sb.ToString();
            }
            else
            {
                if (Request.Form["submit"] != null)
                {
                    TempData["SelectOption"] = -1;
                }
            }
            return View(openWeatherMap);
        }


    }
}