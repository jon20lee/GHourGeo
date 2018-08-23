using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GoldenHourGeo
{
    public class WeatherService
    {
        public string GetWeather(string lat, string lon)
        {
            var apiKey = "9031f2290c8e051a16c1f23373951e8a";

            var client = new RestClient("http://api.openweathermap.org");
            var request = new RestRequest("data/2.5/weather?lat=" + lat + "&lon=" + lon + "&APPID=" + apiKey, Method.GET);
            var queryResult = client.Execute(request);
            return queryResult.Content;
        }
    }
}
