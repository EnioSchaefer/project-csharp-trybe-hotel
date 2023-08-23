using TrybeHotel.Models;
using Microsoft.EntityFrameworkCore;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 6. Desenvolva o endpoint GET /room/:hotelId
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            List<RoomDto> rooms = _context.Rooms
            .Include(r => r.Hotel)
            .ThenInclude(h => h.City)
            .Where(room => room.HotelId == HotelId)
            .Select(room =>
                new RoomDto
                {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = room.Hotel.HotelId,
                        Name = room.Hotel.Name,
                        Address = room.Hotel.Address,
                        CityId = room.Hotel.CityId,
                        CityName = room.Hotel.City.Name,
                        State = room.Hotel.City.State
                    }
                }).ToList();

            return rooms;
        }

        // 7. Desenvolva o endpoint POST /room
        public RoomDto AddRoom(Room room)
        {
            EntityEntry<Room> createdRoom = _context.Rooms.Add(room);
            _context.SaveChanges();

            Hotel hotel = _context.Hotels
            .Include(h => h.City)
            .Where(hotel => hotel.HotelId == room.HotelId)
            .First();

            return new RoomDto
            {
                RoomId = createdRoom.Entity.RoomId,
                Name = createdRoom.Entity.Name,
                Capacity = createdRoom.Entity.Capacity,
                Image = createdRoom.Entity.Image,
                Hotel = new HotelDto
                {
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    CityId = hotel.CityId,
                    CityName = hotel.City.Name,
                }
            };
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        public void DeleteRoom(int RoomId)
        {
            Room room = _context.Rooms.Find(RoomId);
            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return;
        }
    }
}