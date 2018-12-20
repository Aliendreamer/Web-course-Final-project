namespace FanFiction.Services.Interfaces
{
    using ViewModels.InputModels;
    using ViewModels.OutputModels.Chapters;

    public interface IChapterService
    {
        void DeleteChapter(int storyId, int chapterid, string username);

        void AddChapter(ChapterInputModel inputModel);

        ChapterEditModel GetChapterToEditById(int id);

        void EditChapter(ChapterEditModel editModel);
    }
}