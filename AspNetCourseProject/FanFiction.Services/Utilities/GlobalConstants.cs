namespace FanFiction.Services.Utilities
{
	public class GlobalConstants
	{
		public const string Error = "Error";

		public const string ModelError = "LoginError";

		public const string LoginError = "Nickname or password don't match!";

		public const string NicknameUnique = "Choose new nickname,this {0} is already in use!";

		public const string NoSummary = "No summary included";

		public const string NullName = "Name can't be empty";

		public const string Anonymous = "anonymous";

		public const string Success = "Success";

		public const string Failed = "Failed the task";

		public const string TooShortAnnouncement = "Your announcement has to be at least 5 characters long";

		public const string UserLackRights = "User don't have rights to make this action";

		public const string AlreadyExistsInDb = "Genre: {0} already exists";

		public const string NoRecordInDb = "No such record!";

		public const string PaidUser = "paidUser";

		public const string Admin = "admin";

		public const string Moderator = "moderator";

		public const string DefaultRole = "user";

		public const string DbConstName = "dataFromDb";

		public const string ErrorOnDeleteUser = "User was not deleted.Something went wrong!";

		public const string ReturnAllStories = "All";

		public static readonly string[] imageFormat = { "png", "jpeg" };

		public const string WrongFileType = "Image file should be png";

		public const string NotValidChapterStoryConnection = "This story with id: {0} or chapter with id: {1} don't have a connection";

		public const string NotAuthor = "For this action user need to be author of the fiction story";

		public const string UserHasNoRights = "User need role admin or moderator for this action";

		public const string MissingStory = "No Fiction story with such Id";

		public const string UsernameHolder = "username";

		public const string NoTitleAdded = "Chapter has no title";

		public const string UserDontFollow = "User has no following on this story";

		public const string DefaultNoImage = "https://res.cloudinary.com/dbwuk5rsq/image/upload/v1544052092/noimagedefault.jpg";

		public const string EmptyMessage = "Message can't be empty";

		public const string ChapterLength = "ChapterLength";

		public const string NotificationMessage = "{0} Story you are following just added new Chapter";

		public const string UserFollowAlready = "{0} already is following this story";

		public const string AlreadyRated = "User already rated this story";

		public const string RedirectAfterAction = "redirectAfterAction";

		public const string ChapterId = "chapterId";

		public const string CommentsLength = "Comment should not be empty";

		public const string DeletedUser = "Deleted Author";

		public const string UserId = "userId";

		public const string StoryId = "storyId";

		public const string Id = "id";

		public const string NoSuchGenre = "Searched genre: {0}, does not exist";

		public const int Zero = 0;

		public class RouteConstants
		{
			public const string UserProfileRoute = "/Users/Profile/{username}";

			public const string UserBlockRoute = "/Users/BlockUser/{username}";

			public const string UserStories = "/Stories/UserStories/{username}";

			public const string Administration = "Administration";

			public const string AddChapterRoute = "/Chapters/AddChapter/{storyId}";

			public const string ApiRoute = "api/fanfiction/";

			public const string Authors = "Authors";

			public const string Stories = "Stories";

			public const string TopStories = "TopStories";

			public const string StoriesByGenre = "StoriesByGenre/{genre}";

			public const string ErrorPageRoute = "/Home/Error";
		}

		public class CLoudinarySetup
		{
			public const string AccCloudinarySecret = "mWiphHpa4VxGBv1Ftzcv63Bzv-w";
			public const string AccCloudinaryApiKey = "963367669286161";
			public const string CloudinaryCloudName = "dbwuk5rsq";
		}
	}
}