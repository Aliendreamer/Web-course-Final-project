namespace FanFiction.ViewModels.OutputModels.InfoHub
{
    using System;

    public class MessageOutputModel
    {
        public int Id { get; set; }

        public DateTime SendOn { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string Text { get; set; }

        public bool? IsReaden { get; set; }
    }
}