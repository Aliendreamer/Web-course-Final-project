namespace FanFiction.ViewModels.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MessageInputModel
    {
        [Required]
        [StringLength(400)]
        public string Message { get; set; }

        public DateTime SendDate { get; set; }

        public string SenderName { get; set; }

        public string ReceiverName { get; set; }
    }
}