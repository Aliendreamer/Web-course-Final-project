namespace FanFiction.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using ViewModels.InputModels;
    using ViewModels.OutputModels;
    using ViewModels.OutputModels.Users;

    public interface IUserService
    {
        SignInResult LogUser(LoginInputModel loginModel);

        Task<SignInResult> RegisterUser(RegisterInputModel registerModel);

        HomeLoggedModel GetHomeViewDetails();

        string GetUserNickname(string username);

        void Logout();

        UserOutputModel GetUser(string nickName);
    }
}