using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;

namespace MCSAndroidAPI.Contracts
{
    public interface IAuthenticateRepository : IRepositoryBase<MUser>
    {
        Task<ResponseModel<JWTTokenResponse>> AuthenticateAsync(LoginModel model);
    }
}
