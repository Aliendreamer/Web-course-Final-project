namespace FanFiction.Services.Utilities
{
    public class GlobalConstants
    {
        public const string Error = "Error";

        public const string ModelError = "LoginError";

        public const string LoginError = "Nickname or password don't match!";

        public const string NicknameUnique = "Choose new nickname,this {0} is already in use!";

        public const string NoSummary = "No summary included";

        public const string TooShortAnnouncement = "Your announcement has to be at least 5 characters long";

        public const string Admin = "admin";

        public const string Moderator = "moderator";

        public const string DefaultRole = "user";

        public const string DbConstName = "dataFromDb";

        public const string ErrorOnDeleteUser = "User was not deleted.Something went wrong!";

        public const string RoleChangeError = "User already has no role make another choice!";

        public class RouteConstants
        {
            public const string UserProfileRoute = "/Users/Profile/{nickname}";

            public const string Administration = "Administration";
        }
    }
}