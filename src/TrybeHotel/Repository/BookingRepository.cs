using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            User userData = _context.Users.Where(user => user.Email == email).First();
            Booking bookingToInsert = new Booking
            {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                UserId = userData.UserId,
                RoomId = booking.RoomId
            };

            Booking createdBooking = _context.Bookings.Add(bookingToInsert).Entity;
            _context.SaveChanges();

            Booking bookingResponse = _context.Bookings
            .Where(booking => booking.BookingId == createdBooking.BookingId)
            .Include(booking => booking.Room)
            .ThenInclude(room => room.Hotel)
            .ThenInclude(hotel => hotel.City)
            .First();

            return new BookingResponse
            {
                BookingId = bookingResponse.BookingId,
                CheckIn = bookingResponse.CheckIn,
                CheckOut = bookingResponse.CheckOut,
                GuestQuant = bookingResponse.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = bookingResponse.Room.RoomId,
                    Name = bookingResponse.Room.Name,
                    Capacity = bookingResponse.Room.Capacity,
                    Image = bookingResponse.Room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = bookingResponse.Room.Hotel.HotelId,
                        Name = bookingResponse.Room.Hotel.Name,
                        Address = bookingResponse.Room.Hotel.Address,
                        CityId = bookingResponse.Room.Hotel.CityId,
                        CityName = bookingResponse.Room.Hotel.City.Name,
                        State = bookingResponse.Room.Hotel.City.State,
                    }
                }
            };
        }

        public BookingResponse? GetBooking(int bookingId, string email)
        {
            Booking? booking = _context.Bookings
            .Where(booking => booking.BookingId == bookingId)
            .Include(booking => booking.User)
            .Include(booking => booking.Room)
            .ThenInclude(room => room.Hotel)
            .ThenInclude(hotel => hotel.City)
            .First();

            if (booking.User.Email != email)
            {
                return null;
            }

            return new BookingResponse
            {
                BookingId = booking.BookingId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = booking.Room.RoomId,
                    Name = booking.Room.Name,
                    Capacity = booking.Room.Capacity,
                    Image = booking.Room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = booking.Room.Hotel.HotelId,
                        Name = booking.Room.Hotel.Name,
                        Address = booking.Room.Hotel.Address,
                        CityId = booking.Room.Hotel.CityId,
                        CityName = booking.Room.Hotel.City.Name,
                        State = booking.Room.Hotel.City.State,
                    }
                }
            };
        }

        public Room GetRoomById(int RoomId)
        {
            Room? room = _context.Rooms
            .Where(room => room.RoomId == RoomId)
            .Include(room => room.Hotel)
            .Include(room => room.Bookings)
            .First();

            return room;
        }

    }

}