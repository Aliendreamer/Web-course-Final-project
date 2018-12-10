namespace FanFiction.Services
{
    using AutoMapper;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using ViewModels.InputModels;

    public class CommentService : BaseService, ICommentService
    {
        public CommentService(UserManager<FanFictionUser> userManager,
            SignInManager<FanFictionUser> signInManager,
            FanFictionContext context,
            IMapper mapper)
            : base(userManager, signInManager, context, mapper)
        {
        }

        public void AddComment(CommentInputModel inputModel)
        {
            var user = this.UserManager.FindByNameAsync(inputModel.CommentAuthor).GetAwaiter().GetResult();

            var comment = Mapper.Map<Comment>(inputModel);
            comment.FanFictionUser = user;

            this.Context.Comments.Add(comment);
            user.Comments.Add(comment);

            this.Context.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            var comment = this.Context.Comments.Find(id);
            this.Context.Comments.Remove(comment);
            this.Context.SaveChanges();
        }
    }
}