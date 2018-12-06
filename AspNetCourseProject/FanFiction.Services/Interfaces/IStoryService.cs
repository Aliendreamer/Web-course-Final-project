namespace FanFiction.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels.InputModels;
    using ViewModels.OutputModels.Stories;

    public interface IStoryService
    {
        ICollection<StoryOutputModel> CurrentStories(string type);

        ICollection<StoryOutputModel> UserStories(string username);

        Task DeleteStory(int id, string username);

        ICollection<StoryTypeOutputModel> Genres();

        Task<int> CreateStory(StoryInputModel inputModel);
    }
}