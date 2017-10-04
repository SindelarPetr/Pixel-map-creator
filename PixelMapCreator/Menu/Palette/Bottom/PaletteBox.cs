using System;
using GameEngine.CameraEngine;
using GameEngine.Input;
using GameEngine.Menu.Screens;
using GameEngine.Primitives;
using GameEngine.Properties;
using GameEngine.RunBasics;
using Microsoft.Xna.Framework;

namespace PixelMapCreator.Menu.Palette
{
	public class PalettePixelRectangle : PaletteObject
	{
		public PalettePixelRectangle(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider, IScreenParentObject parent = null) : base(camera, positionProvider, sizeProvider, "Rectangle", null, null, parent)
		{
			OnClick += OnOnClick;
		}

		private void OnOnClick(MyTouch myTouch)
		{
			MenuScreenManager.GetScreen<ScreenLevelCreator>().CreatePixelRectangle();
		}
	}
}
