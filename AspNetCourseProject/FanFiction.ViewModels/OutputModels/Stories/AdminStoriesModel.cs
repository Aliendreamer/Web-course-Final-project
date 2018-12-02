namespace FanFiction.ViewModels.OutputModels.Stories
{
    using System.Collections.Generic;

    public class AdminStoriesModel
    {
        public AdminStoriesModel()
        {
            this.Stories = new List<StoryOutputModel>();
            this.Genres = new List<StoryTypeOutputModel>();
        }

        public ICollection<StoryOutputModel> Stories { get; set; }

        public ICollection<StoryTypeOutputModel> Genres { get; set; }
    }
}