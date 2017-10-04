using System.Threading.Tasks;

namespace PixelMapCreator
{
	public interface IFilePathProvider
	{
		string SelectPathForOpen();
		Task<string> SelectPathForSave();
	}
}
