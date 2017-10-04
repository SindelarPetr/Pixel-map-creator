//using GameEngine.Options;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace PixelMapCreator.MrGunman
//{
//	public static class MrGunmanMain
//	{
//		private static Game _game;

//		/// <summary>
//		/// Load all the game content.
//		/// </summary>
//		/// <param name="game">Instance of game which calls this method.</param>
//		/// <param name="platform">The platform program runs on.</param>
//		public static void Load(Game game, Platform platform)
//		{
//			_game = game;

//			PixelMapCreatorMain.Load(game, platform);
//		}

//		/// <summary>
//		/// Update all game logic here. Firstly is updated options and then all menus (which includes even gameplay)
//		/// </summary>
//		/// <param name="gameTime">Duration of one frame.</param>
//		/// <param name="isActive">Indicates whether the window with this game is focused.</param>
//		public static void Update(GameTime gameTime, bool isActive)
//		{
//			PixelMapCreatorMain.Update(gameTime, isActive);
//		}

//		/// <summary>
//		/// Draw all menus (including gameplay in a specific menu).
//		/// </summary>
//		/// <param name="spriteBatch">Element for drawing.</param>
//		public static void Draw(SpriteBatch spriteBatch)
//		{
//			PixelMapCreatorMain.Draw(spriteBatch);
//		}

//		/// <summary>
//		/// Changes resolution of all elements in the game - just calls ResolutionChanged method of all elements in the game.
//		/// </summary>
//		/// <param name="newResolution">Value of resolution after change.</param>
//		public static void ResolutionChanged(Vector2 newResolution)
//		{
//			PixelMapCreatorMain.ResolutionChanged(newResolution);
//		}

//		/// <summary>
//		/// Quits the application - doesnt ask anything just terminates the game.
//		/// </summary>
//		public static void Exit()
//		{
//			PixelMapCreatorMain.Exit();
//		}

//	}
//}
