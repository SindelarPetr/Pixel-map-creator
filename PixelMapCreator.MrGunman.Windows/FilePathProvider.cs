using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


//[assembly: Xamarin.Forms.Dependency(typeof(FilePathProvider))]
namespace PixelMapCreator.MrGunman.Windows
{
	public class FilePathProvider : IFilePathProvider
	{
		public string SelectPathForOpen()
		{
			var dialog = new OpenFileDialog();
			dialog.Multiselect = false;
			dialog.CheckFileExists = true;
			dialog.ShowDialog();

			var fileName = dialog.FileName;
			return fileName;

		}

		public async Task<string> SelectPathForSave()
		{
			Action action = () =>
			{
				SaveFileDialog save = new SaveFileDialog();
				save.Title = "Save level";
				DialogResult result = save.ShowDialog();
				if (result == DialogResult.OK)
				{
					var savePatch = save.FileName;
					//Level.Save(savePatch);
				}
			};
			Thread thread = new Thread(new ThreadStart(action));
			thread.SetApartmentState(ApartmentState.STA);

			thread.Start();

			return "";
			//var dialog = new SaveFileDialog();
			//dialog.OverwritePrompt = true;
			//var result = dialog.ShowDialog();
			//if (result != DialogResult.OK && result != DialogResult.Yes)
			//	throw new FileNotFoundException("Dialog has been aborted.");

			//var fileName = dialog.FileName;
			//return fileName;

			//return "C://Users/petrs/Desktop/SavedMap";
		}
	}
}
