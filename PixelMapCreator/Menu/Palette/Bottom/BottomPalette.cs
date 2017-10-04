using GameEngine.CameraEngine;
using GameEngine.Menu.Screens;
using GameEngine.Options;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;

namespace PixelMapCreator.Menu.Palette
{
	public class BottomPalette : BasicPalette
	{
		#region Static
		public static Vector2 PaletteSize => new Vector2(DisplayOptions.Resolution.X, DisplayOptions.Resolution.Y / 17f);
		public static Vector2 PalettePosition => new Vector2(0, DisplayOptions.MiddleOfScreen.Y) - new Vector2(0, PaletteSize.Y / 2f);
		#endregion

		public BottomPalette(Camera camera, IScreenParentObject parent = null, MyTexture2D texture = null)
			: base(camera, PalettePosition, PaletteSize, parent, texture)
		{
			AddPaletteObject(new PalettePixelRectangle(camera, () => GetPaletteObjectPosition(0), () => PaletteObjectSize, this));
			ColorChanger.ResetColor(Color.LightGray);
		}

		public override void ResolutionChanged()
		{
			base.ResolutionChanged();
			BasicSize = PaletteSize;
			BasicPosition = PalettePosition;

			for (var index = 0; index < PaletteObjects.Count; index++)
			{
				var paletteObject = PaletteObjects[index];
				paletteObject.ResolutionChanged();
				paletteObject.BasicSize = PaletteObjectSize;
				paletteObject.BasicPosition = GetPaletteObjectPosition(index);
			}
		}
	}
}
