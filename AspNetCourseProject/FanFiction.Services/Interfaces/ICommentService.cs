namespace FanFiction.Services.Interfaces
{
    using ViewModels.InputModels;

    public interface ICommentService
    {
        void AddComment(CommentInputModel inputModel);

        void DeleteComment(int id);

        void DeleteAllComments(string username);
    }
}