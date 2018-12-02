namespace FanFiction.ViewModels.OutputModels.Stories
{
    using System.Collections.Generic;
    using Users;

    public class ChapterOutputModel
    {
        public ChapterOutputModel()
        {
            this.Comments = new List<CommentOutputModel>();
        }

        public int Id { get; set; }

        public double Length { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public ICollection<CommentOutputModel> Comments { get; set; }
    }
}