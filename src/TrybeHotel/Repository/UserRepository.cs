using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public UserDto? GetUserById(int userId)
        {
            User? user = _context.Users.Find(userId);

            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = user.UserType
            };
        }

        public UserDto? Login(LoginDto login)
        {
            User? user = _context.Users
            .Where(user => user.Email == login.Email && user.Password == login.Password)
            .FirstOrDefault();

            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = user.UserType
            };
        }

        public UserDto Add(UserDtoInsert user)
        {
            User userToAdd = new()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            };

            EntityEntry<User> createdUser = _context.Users.Add(userToAdd);
            _context.SaveChanges();

            return new UserDto
            {
                UserId = createdUser.Entity.UserId,
                Name = createdUser.Entity.Name,
                Email = createdUser.Entity.Email,
                UserType = createdUser.Entity.UserType
            };
        }

        public UserDto? GetUserByEmail(string userEmail)
        {
            User? user = _context.Users.Where(user => user.Email == userEmail).FirstOrDefault();

            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = user.UserType
            };
        }

        public IEnumerable<UserDto> GetUsers()
        {
            IEnumerable<UserDto> users = _context.Users.Select(user => new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                UserType = user.UserType
            }).ToList();

            return users;
        }

    }
}