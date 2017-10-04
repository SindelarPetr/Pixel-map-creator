using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using GameEngine.Menu;
using GameEngine.Menu.Screens;
using GameEngine.Options;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;
using PixelMapCreator.Menu.Palette;
using System;
using PixelMapCreator.Menu.MenuLevelCreator;

namespace PixelMapCreator.Menu.ColorPicker
{
	public class ColorPicker : ScreenTextureContainer
	{
		#region Static
		public static Vector2 PickerSize => new Vector2(DisplayOptions.Resolution.X / 6f, DisplayOptions.Resolution.X / 10f);
		public static Vector2 PickerPosition => DisplayOptions.MiddleOfScreen - PickerSize / 2f - new Vector2(BottomPalette.PaletteSize.Y) * new Vector2(0.1f, 1.1f);

		public static Vector2 SliderSize => new Vector2(PickerSize.X / 6f, PickerSize.Y * 0.9f);
		public static Vector2 HueSliderPosition => -new Vector2(0, 0.9f * PickerSize.Y / 2 - SliderSize.X / 2);
		public static Vector2 SaturationSliderPosition => Vector2.Zero;
		public static Vector2 LightnessSliderPosition => -HueSliderPosition;
		#endregion

		private readonly ScreenSlider _lightnessSlider;
		private readonly ScreenSlider _saturationSlider;
		private readonly ScreenSlider _hueSlider;

		public Color PickedColor
		{
			get => MyMath.HslToRgb(_hueSlider.Ratio, _saturationSlider.Ratio, _lightnessSlider.Ratio);
			set
			{
				double h, s, l;
				MyMath.RgbToHsl(value, out h, out s, out l);
				_hueSlider.Ratio = (float)h;
				_saturationSlider.Ratio = (float)s;
				_lightnessSlider.Ratio = (float)l;
			}
		}

		public event Action<Color> OnColorChanged;

		public ColorPicker(Camera camera, IScreenParentObject parent = null) : base(camera, () => PickerPosition, () => PickerSize, parent)
		{
			ColorChanger.ResetColor(Color.DarkGray);

			_hueSlider = CreateSlider(() => HueSliderPosition, () => SliderSize);
			_saturationSlider = CreateSlider(() => SaturationSliderPosition, () => SliderSize);
			_lightnessSlider = CreateSlider(() => LightnessSliderPosition, () => SliderSize);

		}

		private ScreenSlider CreateSlider(Func<Vector2> positionProvider, Func<Vector2> sizeProvider, MyTexture2D texture = null)
		{
			var slider = new ScreenSlider(Camera, positionProvider, sizeProvider, this, texture);
			slider.BasicRotation = MathHelper.PiOver2;
			slider.OnValueChanged += v =>
			{
				OnColorChanged?.Invoke(PickedColor);
			};
			slider.ChangeColor(new MyColor(new Color(100, 100, 100)));

			AddNestedObject(slider, 4);
			return slider;
		}

		public void QuietResetColor(Color color)
		{

			double h, s, l;
			MyMath.RgbToHsl(color, out h, out s, out l);

			_hueSlider.QuietResetRatio(h);
			_saturationSlider.QuietResetRatio(s);
			_lightnessSlider.QuietResetRatio(l);
		}
	}
}
