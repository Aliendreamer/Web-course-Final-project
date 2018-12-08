namespace FanFiction.ViewModels.OutputModels.Stories
{
    public class StoryDetailsOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Summary { get; set; }

        public string Author { get; set; }

        public string StoryType { get; set; }

        public string CreatedOn { get; set; }

        public string LastEditedOn { get; set; }

        public double Rating { get; set; }
    }
}