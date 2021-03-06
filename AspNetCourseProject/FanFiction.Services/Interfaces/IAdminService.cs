﻿namespace FanFiction.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using ViewModels.InputModels;
    using ViewModels.OutputModels.Announcements;
    using ViewModels.OutputModels.Users;

    public interface IAdminService
    {
        Task<IEnumerable<UserAdminViewOutputModel>> AllUsers();

        Task DeleteUser(string Id);

        Task<IdentityResult> ChangeRole(ChangingRoleModel model);

        AllAnnouncementsModel AllAnnouncements();

        ChangingRoleModel AdminModifyRole(string Id);

        void AddAnnouncement(AnnouncementInputModel inputModel);

        void DeleteAnnouncement(int id);

        void DeleteAllAnnouncements();

        string AddGenre(string type);
    }
}