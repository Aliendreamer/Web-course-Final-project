namespace FanFiction.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels.OutputModels.Users;

    public interface IAdminService
    {
        Task<IEnumerable<UserAdminViewModel>> AllUsers();
    }
}