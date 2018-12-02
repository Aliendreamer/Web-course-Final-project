namespace FanFiction.ViewModels.OutputModels.Stories
{
    public class StoryTypeOutputModel
    {
        //TODO: once upon a time I will allow them to delete genres now only should be added!
        // to allow delete ot genres I have to substituted them with default no genre first or allow nullable in db
        //not right now though!
        public int Id { get; set; }

        public string Type { get; set; }
    }
}