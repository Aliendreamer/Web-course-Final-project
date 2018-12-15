namespace FanFiction.ViewModels.InputModels
{
    using System;
    using Utilities;
    using System.ComponentModel.DataAnnotations;

    public class MessageInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.MessageLength)]
        public string Message { get; set; }

        public DateTime SendDate { get; set; }

        public string SenderName { get; set; }

        public string ReceiverName { get; set; }
    }
}