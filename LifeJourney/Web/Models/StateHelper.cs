using BL.DTOs;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Web.Models
{
    public class StateHelper
    {
        private IHttpContextAccessor _httpContextAccessor;
        private GetUserDTO? _userData;

        public StateHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userData = GetUserData();
        }

        public GetUserDTO? GetUserData()
        {
            var userData = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.UserData);
            if (userData != null)
                return JsonConvert.DeserializeObject<GetUserDTO>(userData);
            else
                return null;
        }
    }

}
