namespace FanFiction.Services.Interfaces
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using ViewModels.OutputModels.ApiOutputModels;

	public interface IApiService
	{
		IEnumerable<ApiUserOutputModel> Authors();

		IEnumerable<ApiFanFictionStoryOutputModel> TopStories();

		IEnumerable<ApiFanFictionStoryOutputModel> Stories();

		IEnumerable<ApiFanFictionStoryOutputModel> StoriesByGenre(string genre);
	}
}