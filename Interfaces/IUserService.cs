using Auth.Dto;
using Auth.Models;

namespace Auth.Interfaces
{
    public interface IUserService
    {
        User Register(RegisterDto registerDto);
        User Login(LoginDto loginDto);
        User GetUserById(int id);
        User GetUserByEmail(string email);
        IEnumerable<User> GetAllUsers();
        void UpdateUser(int id, UserDto userDto);
        void DeleteUser(int id);
    }

}

