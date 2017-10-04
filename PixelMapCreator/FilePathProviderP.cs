using System.Threading.Tasks;
using PixelMapCreator.Menu.MenuLevelCreator;
using Xamarin.Forms;

namespace PixelMapCreator
{
	public class FilePathProviderP : IFilePathProvider
	{
		public string SelectPathForOpen()
		{
			return DependencyService.Get<IFilePathProvider>().SelectPathForOpen();
		}

		public async Task<string> SelectPathForSave()
		{
			return await DependencyService.Get<IFilePathProvider>().SelectPathForSave();
		}
	}
}
