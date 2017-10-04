using GameEngine.CameraEngine;
using GameEngine.Menu;
using GameEngine.Properties;
using GameEngine.RunBasics;
using Microsoft.Xna.Framework;
using PCLStorage;

namespace PixelMapCreator.Menu.Palette
{
	public class ScreenPalettes : MenuScreen
	{
		private const string SAVE_PATH = "C://Users/petrs/Desktop/Map";

		#region Static part
		public static ScreenPalettes Instance { get; private set; }

		public static ScreenPalettes CreateInstance(Camera camera)
		{
			Instance = new ScreenPalettes(camera);
			return Instance;
		}

		public static MyColor MenuColor = new MyColor(new Color(0, 76, 146));
		#endregion



		public ScreenPalettes(Camera camera) : base(camera)
		{
			DrawPriorities = DrawPriorities.MiddleTop;
			var topPalette = new TopPalette(Camera, this);
			topPalette.OnSave += Save;
			topPalette.OnGroup += TopPalette_OnGroup;
			topPalette.OnUnGroup += TopPalette_OnUnGroup;
			topPalette.OnLoad += TopPalette_OnLoad;
			topPalette.OnRemove += TopPalette_OnRemove;
			topPalette.OnUndoRemove += TopPalette_OnUndoRemove;

			AddNestedObject(topPalette, 3);
			AddNestedObject(new BottomPalette(Camera, this), 3);
		}

		private void TopPalette_OnUndoRemove()
		{

		}

		private void TopPalette_OnRemove()
		{
			MenuScreenManager.GetScreen<ScreenLevelCreator>().RemoveSelection();
		}

		private void TopPalette_OnLoad()
		{
			var path = "C://Users/petrs/Desktop/oldLevel";
			if (FileSystem.Current.LocalStorage.CheckExistsAsync(path).Result == ExistenceCheckResult.NotFound)
			{
				// TODO: Show a message that the file is not there
				return;
			}

			MenuScreenManager.GetScreen<ScreenLevelCreator>().LoadMap(FileSystem.Current.LocalStorage.GetFileAsync(path).Result.ReadAllTextAsync().Result);
		}

		private void TopPalette_OnUnGroup()
		{
			MenuScreenManager.GetScreen<ScreenLevelCreator>().DestroyGroup();
		}

		private void TopPalette_OnGroup()
		{
			MenuScreenManager.GetScreen<ScreenLevelCreator>().CreateGroup();
		}



		private void Save()
		{
			//int num = 0;

			//while (FileSystem.Current.LocalStorage.CheckExistsAsync(SAVE_PATH + ++num).Result != ExistenceCheckResult.NotFound);
			var path = "C://Users/petrs/Desktop/newLevel";//new FilePathProviderP().SelectPathForSave().Result;//SAVE_PATH + num;
			var serializedMap = SerializeMap();
			FileSystem.Current.LocalStorage.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting).Result.WriteAllTextAsync(SerializeMap());
		}

		private string SerializeMap()
		{
			return MenuScreenManager.GetScreen<ScreenLevelCreator>().SerializeMap();
		}
	}
}
