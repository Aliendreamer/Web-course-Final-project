namespace FanFiction.Models
{
    using System.Collections.Generic;

    public class StoryType
    {
        public StoryType()
        {
            this.Stories = new HashSet<FanFictionStory>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<FanFictionStory> Stories { get; set; }
    }
}