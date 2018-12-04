namespace FanFiction.Models
{
    public class BlockedUsers
    {
        public string FanfictionUserId { get; set; }
        public virtual FanFictionUser FanFictionUser { get; set; }

        public string BlockedUserId { get; set; }
        public virtual FanFictionUser BlockedUser { get; set; }
    }
}