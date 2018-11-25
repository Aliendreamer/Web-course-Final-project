namespace FanFiction.Models
{
    using System;

    public class Message
    {
        public int Id { get; set; }

        public DateTime SendOn { get; set; }

        public string SenderId { get; set; }
        public FanFictionUser Sender { get; set; }

        public string ReceiverId { get; set; }
        public FanFictionUser Receiver { get; set; }

        public string Text { get; set; }

        public bool? IsReaden { get; set; }
    }
}