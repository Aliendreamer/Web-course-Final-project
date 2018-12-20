namespace FanFiction.ViewModels.Utilities
{
    public class ViewModelsConstants
    {
        public const int UserModelNicknameMaxLength = 100;

        public const int UserModelNicknameMinLength = 5;

        public const string ErrorMessageInRegisterModel = "Your nickname should contain only alphabet symbols";

        public const string RegexForValidationNicknameOrUsername = "[A-Za-z]+";

        public const string ConfirmPasswordDisplay = "Confirm Password";

        public const string ChapterInputContentError = "Your chapter should not have less than 500 charaters or  more than 3000 characters";

        public const int ChapterLength = 3000;

        public const int ChapterMinLength = 500;

        public const int TitleMaxLength = 100;

        public const int TitleMinLength = 5;

        public const int AnnouncementMaxLength = 400;

        public const int AnnouncementMinLength = 5;

        public const int CommentLength = 100;

        public const int MessageLength = 400;

        public const int StorySummaryLength = 200;

        public const string StoryImageDisplay = "Story Image";
    }
}