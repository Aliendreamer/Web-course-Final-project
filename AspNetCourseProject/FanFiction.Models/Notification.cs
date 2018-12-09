namespace FanFiction.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public bool Seen { get; set; }

        public string Message { get; set; }

        public string FanFictionUserId { get; set; }
        public FanFictionUser FanFictionUser { get; set; }
    }
}