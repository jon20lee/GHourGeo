using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using GoldenHourGeo;
using GoldenHourGeo.Infrastructure;
using System.Dynamic;

namespace GoldenHourGeo
{
    public class WeatherService
    {
        public string GetWeather(string lat, string lon)
        {
            if (String.IsNullOrEmpty(lat) || String.IsNullOrEmpty(lon)) { return "{ Error: Lat and long values unavailable. }" ; }

            var queryString = $"?lat={lat}&lon={lon}&APPID={env.WEATHER_API_KEY}";

            var client = new RestClient(env.OPEN_WEATHER_API_HOST_URL);
            var request = new RestRequest(env.OPEN_WEATHER_API_RESOUCRE_WEATHER + queryString, Method.GET);
            var queryResult = client.Execute(request);
            return queryResult.Content;
        }
    }
}
