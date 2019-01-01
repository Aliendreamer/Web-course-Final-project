namespace FanFiction.ViewModels.OutputModels.Stories
{
	using System;

	public class StoryHomeOutputModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Summary { get; set; }

		public string Author { get; set; }

		public string StoryType { get; set; }

		public DateTime CreatedOn { get; set; }

		public double Rating { get; set; }
	}
}