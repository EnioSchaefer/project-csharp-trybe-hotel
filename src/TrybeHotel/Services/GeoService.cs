using System.Net.Http;
using System.Text.Json;
using FluentAssertions.Types;
using TrybeHotel.Dto;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Services
{
    public class GeoService : IGeoService
    {
        private readonly HttpClient _client;
        public GeoService(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<object> GetGeoStatus()
        {
            using (HttpResponseMessage Response = await _client.GetAsync("https://nominatim.openstreetmap.org/status.php?format=json"))
            {
                if (Response.IsSuccessStatusCode)
                {
                    object Json = await Response.Content.ReadAsStringAsync();

                    return Json;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<GeoDtoResponse> GetGeoLocation(GeoDto geoDto)
        {
            string Url = $"https://nominatim.openstreetmap.org/search?street={geoDto.Address}&city={geoDto.City}&country=Brazil&state={geoDto.State}&format=json&limit=1";
            using (HttpResponseMessage Response = await _client.GetAsync(Url))
            {
                Response.Headers.Add("User-Agent", "aspnet-user-agent");
                if (Response.IsSuccessStatusCode)
                {
                    Console.WriteLine(Response.Content.ReadAsStringAsync());
                    var Json = await Response.Content.ReadFromJsonAsync<GeoDtoResponse[]>();

                    return Json.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {
            GeoDtoResponse BaseGeoLocation = await GetGeoLocation(geoDto);
            IEnumerable<HotelDto> Hotels = repository.GetHotels();
            List<GeoDtoHotelResponse> HotelsDistanceList = new();

            foreach (HotelDto hotel in Hotels)
            {
                GeoDto HotelGeoDto = new GeoDto { Address = hotel.Address, City = hotel.CityName, State = hotel.State };
                GeoDtoResponse HotelGeoLocation = await GetGeoLocation(HotelGeoDto);

                int Distance = CalculateDistance(BaseGeoLocation.lat, BaseGeoLocation.lon, HotelGeoLocation.lat, HotelGeoLocation.lon);

                HotelsDistanceList.Add(new GeoDtoHotelResponse
                {
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    CityName = hotel.CityName,
                    State = hotel.State,
                    Distance = Distance
                });
            }

            List<GeoDtoHotelResponse> OrderedHotelsDistanceList = HotelsDistanceList.OrderBy(hotel => hotel.Distance).ToList();
            return OrderedHotelsDistanceList;
        }



        public int CalculateDistance(string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny)
        {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.', ','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.', ','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.', ','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.', ','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;
            return int.Parse(Math.Round(distance, 0).ToString());
        }

        public double radiano(double degree)
        {
            return degree * Math.PI / 180;
        }

    }
}