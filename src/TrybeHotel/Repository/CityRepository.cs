using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<CityDto> GetCities()
        {
            List<CityDto> cities = _context.Cities.Select(city =>
                new CityDto
                {
                    CityId = city.CityId,
                    Name = city.Name,
                    State = city.State
                }).ToList();

            return cities;
        }

        public CityDto AddCity(City city)
        {
            EntityEntry<City> createdCity = _context.Cities.Add(city);
            _context.SaveChanges();

            return new CityDto
            {
                CityId = createdCity.Entity.CityId,
                Name = createdCity.Entity.Name,
                State = createdCity.Entity.State
            };
        }

        public CityDto UpdateCity(City city)
        {
            EntityEntry<City> updatedCity = _context.Cities.Update(city);

            return new CityDto
            {
                CityId = updatedCity.Entity.CityId,
                Name = updatedCity.Entity.Name,
                State = updatedCity.Entity.State,
            };
        }

    }
}