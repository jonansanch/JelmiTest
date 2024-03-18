using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserService
{
    public interface IUserService
    {

        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetUserID(Guid vUserID);
        Task<object> AddOrUpdateAsync(UserDto data);
        Task<bool> DeleteUserAsync(Guid vUserID);
    }


}
