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
            .First();

            return new BookingResponse
            {
                BookingId = bookingResponse.BookingId,
                CheckIn = bookingResponse.CheckIn,
                CheckOut = bookingResponse.CheckOut,
                GuestQuant = bookingResponse.GuestQuant,
                Room = bookingResponse.Room
            };
        }

        public BookingResponse? GetBooking(int bookingId, string email)
        {
            Booking? booking = _context.Bookings
            .Where(booking => booking.BookingId == bookingId)
            .Include(booking => booking.User)
            .Include(booking => booking.Room)
            .ThenInclude(room => room.Hotel)
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
                Room = booking.Room
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