namespace FanFiction.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels.OutputModels.Stories;

    public interface IStoryService
    {
        ICollection<StoryOutputModel> CurrentStories();

        Task DeleteStory(int id, string username);
    }
}