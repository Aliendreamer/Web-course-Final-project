namespace FanFiction.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using ViewModels.InputModels;

    public interface IUserService
    {
        SignInResult LogUser(LoginInputModel loginModel);

        Task<SignInResult> RegisterUser(RegisterInputModel registerModel);

        void Logout();
    }
}