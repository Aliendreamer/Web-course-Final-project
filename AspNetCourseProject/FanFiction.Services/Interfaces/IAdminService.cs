namespace FanFiction.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using ViewModels.OutputModels.Users;

    public interface IAdminService
    {
        Task<IEnumerable<UserAdminViewModel>> AllUsers();

        Task DeleteUser(string Id);

        Task<IdentityResult> ChangeRole(ChangingRoleModel model);

        ChangingRoleModel AdminModifyRole(string Id);
    }
}