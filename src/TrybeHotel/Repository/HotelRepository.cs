using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<HotelDto> GetHotels()
        {
            List<HotelDto> hotels = _context.Hotels
            .Include(h => h.City)
            .Select(hotel => new HotelDto
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Address = hotel.Address,
                CityId = hotel.CityId,
                CityName = hotel.City.Name,
                State = hotel.City.State
            }).ToList();

            return hotels;
        }

        public HotelDto AddHotel(Hotel hotel)
        {
            EntityEntry<Hotel> createdHotel = _context.Hotels.Add(hotel);
            _context.SaveChanges();

            City cityData = _context.Cities.Find(createdHotel.Entity.CityId);

            return new HotelDto
            {
                HotelId = createdHotel.Entity.HotelId,
                Name = createdHotel.Entity.Name,
                Address = createdHotel.Entity.Address,
                CityId = createdHotel.Entity.CityId,
                CityName = cityData.Name,
                State = cityData.State
            };
        }
    }
}