namespace FanFiction.ViewModels.OutputModels.Users
{
    public class UserAdminViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string NickName { get; set; }

        public string Role { get; set; }

        public int Stories { get; set; }

        public int Comments { get; set; }

        public int Messages { get; set; }

        public string Email { get; set; }
    }
}