namespace FanFiction.Services
{
	using Data;
	using Models;
	using Interfaces;
	using AutoMapper;
	using System.Linq;
	using ViewModels.InputModels;
	using Microsoft.AspNetCore.Identity;

	public class CommentService : BaseService, ICommentService
	{
		public CommentService(UserManager<FanFictionUser> userManager,
			FanFictionContext context,
			IMapper mapper)
			: base(userManager, context, mapper)
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

		public void DeleteAllComments(string username)
		{
			var comments = this.Context.Comments.Where(x => x.FanFictionUser.UserName == username).ToList();

			this.Context.Comments.RemoveRange(comments);

			this.Context.SaveChanges();
		}
	}
}