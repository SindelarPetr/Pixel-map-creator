using GameEngine.CameraEngine;
using GameEngine.Properties;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameEngine.Menu;
using GameEngine.Menu.Screens;
using GameEngine.Menu.ScreensAs;
using GameEngine.Primitives;
using Microsoft.Xna.Framework.Graphics;

namespace PixelMapCreator.Menu
{
	public class BasicPalette : ScreenTextureContainer
	{
		protected readonly List<PaletteObject> PaletteObjects;

		protected Vector2 PaletteObjectSize => new Vector2(BasicSize.Y * 0.90f) * new Vector2(2.5f, 1);

		public BasicPalette(Camera camera, Vector2 position, Vector2 size, IScreenParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent, texture)
		{
			PaletteObjects = new List<PaletteObject>();
		}

		protected void AddPaletteObject(PaletteObject paletteObject)
		{
			PaletteObjects.Add(paletteObject);
			AddNestedObject(paletteObject, 4);
		}

		protected Vector2 GetPaletteObjectPosition(int? index = null)
		{
			return new Vector2(-BasicSize.X / 2 + BasicSize.X / 10f + 1.2f * PaletteObjectSize.X * (index ?? PaletteObjects.Count), 0);
		}
	}
}
