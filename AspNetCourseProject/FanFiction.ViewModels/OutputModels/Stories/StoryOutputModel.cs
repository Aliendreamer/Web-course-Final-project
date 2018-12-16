namespace FanFiction.ViewModels.OutputModels.Stories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Users;

    public class StoryOutputModel
    {
        public StoryOutputModel()
        {
            this.Ratings = new List<double>();
            this.Comments = new List<CommentOutputModel>();
            this.Followers = new List<UserOutputViewModel>();
            this.Chapters = new List<ChapterOutputModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Summary { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastEditedOn { get; set; }

        public double Rating => this.Ratings.Any() ? this.Ratings.Average() : 0;

        public ICollection<ChapterOutputModel> Chapters { get; set; }

        public ICollection<UserOutputViewModel> Followers { get; set; }

        public ICollection<CommentOutputModel> Comments { get; set; }

        public ICollection<double> Ratings { get; set; }

        public StoryTypeOutputModel Type { get; set; }

        public UserOutputStoryModel Author { get; set; }
    }
}