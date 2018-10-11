using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using GoldenHourGeo;
using GoldenHourGeo.Infrastructure;

namespace GoldenHourGeo
{
    public class WeatherService
    {
        public string GetWeather(string lat, string lon)
        {
            var queryString = $"data/2.5/weather?lat=" + lat + "&lon=" + lon + "&APPID={env.WEATHER_API_KEY}";

            var client = new RestClient(env.OPEN_WEATHER_API_HOST_URL);
            var request = new RestRequest(queryString + , Method.GET);
            var queryResult = client.Execute(request);
            return queryResult.Content;
        }
    }
}
