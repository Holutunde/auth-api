using Auth.Dto;
using Auth.Models;

namespace Auth.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<User> Login(LoginDto loginDto);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<ICollection<User>> GetAllUsers();
        Task UpdateUser(int id, UserDto userDto);
        Task DeleteUser(int id);
    }
}
