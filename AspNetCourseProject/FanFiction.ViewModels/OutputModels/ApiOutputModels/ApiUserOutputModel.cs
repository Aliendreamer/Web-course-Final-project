namespace FanFiction.ViewModels.OutputModels.ApiOutputModels
{
	using System.Collections.Generic;

	public class ApiUserOutputModel
	{
		public ApiUserOutputModel()
		{
			this.Stories = new HashSet<ApiFanFictionStoryOutputModel>();
		}

		public string Username { get; set; }

		public string Nickname { get; set; }

		public ICollection<ApiFanFictionStoryOutputModel> Stories { get; set; }
	}
}