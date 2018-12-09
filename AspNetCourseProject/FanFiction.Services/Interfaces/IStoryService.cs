﻿namespace FanFiction.Services.Interfaces
{
    using System.Threading.Tasks;
    using ViewModels.InputModels;
    using System.Collections.Generic;
    using ViewModels.OutputModels.Stories;

    public interface IStoryService
    {
        ICollection<StoryOutputModel> CurrentStories(string type);

        ICollection<StoryOutputModel> UserStories(string username);

        Task DeleteStory(int id, string username);

        ICollection<StoryTypeOutputModel> Genres();

        Task Follow(string username, int id);

        Task UnFollow(string username, int id);

        bool IsFollowed(string username, int id);

        Task<int> CreateStory(StoryInputModel inputModel);

        StoryDetailsOutputModel GetStoryById(int id);

        void DeleteChapter(int storyId, int chapterid, string username);

        void AddChapter(ChapterInputModel inputModel);
    }
}