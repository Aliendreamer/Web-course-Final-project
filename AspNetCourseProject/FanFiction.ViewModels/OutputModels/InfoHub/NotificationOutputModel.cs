namespace FanFiction.ViewModels.OutputModels.InfoHub
{
    public class NotificationOutputModel
    {
        public int Id { get; set; }

        public bool Seen { get; set; }

        public string Message { get; set; }

        public string Username { get; set; }
    }
}