namespace FanFiction.ViewModels.OutputModels.Stories
{
    using System;

    public class CommentOtputModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Message { get; set; }

        public DateTime CommentedOn { get; set; }
    }
}