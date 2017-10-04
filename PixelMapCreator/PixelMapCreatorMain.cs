//using GameEngine.CameraEngine;
//using GameEngine.Options;
//using GameEngine.RunBasics;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using PixelMapCreator.Menu;
//using PixelMapCreator.Menu.Palette;

//namespace PixelMapCreator
//{
//	public static class PixelMapCreatorMain
//	{
//		private static Game _game;
//		private static Camera _camera;

//		/// <summary>
//		/// Load all the game content.
//		/// </summary>
//		/// <param name="game">Instance of game which calls this method.</param>
//		/// <param name="platform">The platform program runs on.</param>
//		public static void Load(Game game, Platform platform)
//		{
//			_game = game;

//			GameManager.Load(game, platform);

//			_camera = new Camera();

//			GeneralOptions.BackgroundColor = new Color(100, 100, 100);

//			// Show the first menu
//			ScreenLevelCreator.Instance.Show(null);
//			ScreenPalettes.Instance.Show(null);
//		}

//		/// <summary>
//		/// Update all game logic here. Firstly is updated options and then all menus (which includes even gameplay)
//		/// </summary>
//		/// <param name="gameTime">Duration of one frame.</param>
//		/// <param name="isActive">Indicates whether the window with this game is focused.</param>
//		public static void Update(GameTime gameTime, bool isActive)
//		{
//			GameManager.Update(gameTime, isActive);
//			_camera.Update();
//		}

//		/// <summary>
//		/// Draw all menus (including gameplay in a specific menu).
//		/// </summaryWW
//		/// <param name="spriteBatch">Element for drawing.</param>
//		public static void Draw(SpriteBatch spriteBatch)
//		{
//			GameManager.Draw(spriteBatch);
//		}

//		/// <summary>
//		/// Changes resolution of all elements in the game - just calls ResolutionChanged method of all elements in the game.
//		/// </summary>
//		/// <param name="newResolution">Value of resolution after change.</param>
//		public static void ResolutionChanged(Vector2 newResolution)
//		{
//			GameManager.ResolutionChanged(newResolution);
//		}

//		/// <summary>
//		/// Quits the application - doesnt ask anything just terminates the game.
//		/// </summary>
//		public static void Exit()
//		{
//			GameManager.Exit();
//		}

//	}
//}
