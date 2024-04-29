using BL.DTOs;

namespace BL.Interfaces
{
    public interface IUserRepo
    {
        GetUserDTO Get(string email);
        GetUserDTO Add(AddUserDTO dto);
    }
}
