using System;
using GameEngine.CameraEngine;
using GameEngine.Menu;
using GameEngine.Menu.Screens;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;

namespace PixelMapCreator.Menu
{
	public class PaletteObject : ScreenTextButton
	{
		public PaletteObject(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider, string text, Func<float> textHeightProvider = null, Func<Vector2> textPositionProvider = null, IScreenParentObject parent = null, MyTexture2D texture = null) : base(camera, positionProvider, sizeProvider, text, textHeightProvider, textPositionProvider, parent, texture)
		{
			ChangeColor((MyColor)Color.Gray);
		}
	}
}
