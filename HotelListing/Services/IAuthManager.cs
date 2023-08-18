using HotelListing.Model;

namespace HotelListing.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidadeUser(UserLoginDTO userDTO);
        Task<string> CreateToken();
    }
}
