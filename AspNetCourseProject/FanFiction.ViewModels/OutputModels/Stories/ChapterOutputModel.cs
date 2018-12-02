namespace FanFiction.ViewModels.OutputModels.Stories
{
    using System;
    using System.Collections.Generic;

    public class ChapterOutputModel
    {
        public ChapterOutputModel()
        {
            this.Comments = new List<CommentOutputModel>();
        }

        public int Id { get; set; }

        public int Length { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<CommentOutputModel> Comments { get; set; }
    }
}