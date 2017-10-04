using System;
using GameEngine.CameraEngine;
using GameEngine.Menu.Screens;
using GameEngine.Menu.ScreensAs.Buttons;
using GameEngine.Options;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PixelMapCreator.Menu.ColorPicker
{
	public class ColorSelector : ScreenButton
	{
		#region Static

		public static MyTexture2D GenerateSpectreTexture()
		{
			var texture = new Texture2D(GeneralOptions.GraphicsDevice, 255, 2);
			texture.SetData(GetSpectreColors(255));

			return new MyTexture2D(texture);
		}

		private static Color[] GetSpectreColors(int arrayWidth)
		{
			var colors = new Color[arrayWidth * 2];
			float colorMax = 255;
			float colorStep = 510f / arrayWidth;

			// From R to G
			for (int i = 0; i < arrayWidth / 2f; i++)
			{
				var color = new Color((byte)(colorMax - colorStep * i), (byte)(colorStep * i), 0);
				colors[i] = color;
				colors[i + 255] = new Color((byte)(colorMax - colorStep * i), (byte)(colorStep * i), 0);
			}

			// From G to B
			for (int i = 0; i < arrayWidth / 2f; i++)
			{
				colors[i] = new Color(0, (byte)(colorMax - colorStep * i), (byte)(colorStep * i));
				colors[i + 255] = new Color(0, (byte)(colorMax - colorStep * i), (byte)(colorStep * i));
			}

			return colors;
		}
		#endregion

		public ColorSelector(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider, IScreenParentObject parent = null) : base(camera, positionProvider, sizeProvider, parent, GenerateSpectreTexture())
		{

		}
	}
}
