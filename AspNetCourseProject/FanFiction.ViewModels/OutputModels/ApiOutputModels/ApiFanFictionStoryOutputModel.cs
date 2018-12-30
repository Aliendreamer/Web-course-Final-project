namespace FanFiction.ViewModels.OutputModels.ApiOutputModels
{
	using Chapters;
	using System.Collections.Generic;

	public class ApiFanFictionStoryOutputModel
	{
		public ApiFanFictionStoryOutputModel()
		{
			this.Chapters = new HashSet<ChapterOutputModel>();
		}

		public int Id { get; set; }

		public string Author { get; set; }

		public string Title { get; set; }

		public string Summary { get; set; }

		public double Rating { get; set; }

		public string CreatedOn { get; set; }

		public string LastUpdated { get; set; }

		public string ImageUrl { get; set; }

		public string Genre { get; set; }

		public ICollection<ChapterOutputModel> Chapters { get; set; }
	}
}