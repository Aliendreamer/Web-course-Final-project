namespace FanFiction.Services.Interfaces
{
    using System.Collections.Generic;
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

        IEnumerable<BlockedUserOutputModel> BlockedUsers(string username);

        void Logout();

        UserOutputViewModel GetUser(string nickName);

        Task BlockUser(string currentUser, string name);

        void UnblockUser(string username, string id);
    }
}