namespace FanFiction.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using ViewModels.InputModels;

    public interface IUserService
    {
        Task<SignInResult> LogUser(LoginInputModel loginModel);
    }
}