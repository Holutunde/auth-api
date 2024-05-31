using Auth.Dto;
using Auth.Models;

namespace Auth.Interfaces
{
    public interface IUserService
    {
        User Register(User user);
        User Login(LoginDto loginDto);
        User GetUserById(int id);
        User GetUserByEmail(string email);
        ICollection<User> GetAllUsers();
        void UpdateUser(int id, UserDto userDto);
        void DeleteUser(int id);
    }

}

