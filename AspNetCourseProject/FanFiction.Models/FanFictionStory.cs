namespace FanFiction.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class FanFictionStory
    {
        public FanFictionStory()
        {
            this.Chapters = new HashSet<Chapter>();
            this.Followers = new HashSet<UserStory>();
            this.Comments = new HashSet<Comment>();
            this.Ratings = new HashSet<FanFictionRating>();
        }

        public int Id { get; set; }

        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Summary { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastEditedOn { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }

        public ICollection<UserStory> Followers { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<FanFictionRating> Ratings { get; set; }

        public int StoryTypeId { get; set; }
        public StoryType Type { get; set; }

        public string AuthorId { get; set; }
        public FanFictionUser Author { get; set; }

        public double Rating => this.Ratings.Any() ? this.Ratings.Average(x => x.StoryRating.Rating) : 0;

        public double Length => this.Chapters.Sum(x => x.Length);
    }
}