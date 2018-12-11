namespace FanFiction.Services.Interfaces
{
    using ViewModels.InputModels;

    public interface IChapterService
    {
        void DeleteChapter(int storyId, int chapterid, string username);

        void AddChapter(ChapterInputModel inputModel);
    }
}