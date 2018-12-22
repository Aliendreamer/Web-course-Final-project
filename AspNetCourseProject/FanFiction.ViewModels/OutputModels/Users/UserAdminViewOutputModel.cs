namespace FanFiction.ViewModels.OutputModels.Users
{
    public class UserAdminViewOutputModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Nickname { get; set; }

        public string Role { get; set; }

        public int Comments { get; set; }

        public int Stories { get; set; }

        public int MessageCount { get; set; }
    }
}