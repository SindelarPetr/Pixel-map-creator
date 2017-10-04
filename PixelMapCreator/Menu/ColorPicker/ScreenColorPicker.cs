using GameEngine.CameraEngine;
using GameEngine.Menu;
using GameEngine.Properties;
using Microsoft.Xna.Framework;
using System;
using GameEngine.ValueHolders;

namespace PixelMapCreator.Menu.ColorPicker
{
	public sealed class ScreenColorPicker : MenuScreen
	{
		#region Static part
		public static MyColor MenuColor = new MyColor(new Color(0, 76, 146));
		#endregion

		private readonly ColorPicker _colorPicker;

		public event Action<Color> OnColorPickerValueChanged;

		public ScreenColorPicker(Camera camera) : base(camera)
		{
			_colorPicker = new ColorPicker(Camera, this);
			_colorPicker.OnColorChanged += _colorPicker_OnColorChanged;
			AddNestedObject(_colorPicker, 3);
		}

		private void _colorPicker_OnColorChanged(Color color)
		{
			OnColorPickerValueChanged?.Invoke(color);
		}

		public void QuietResetColor(Color color)
		{
			_colorPicker.QuietResetColor(color);
		}
	}
}
