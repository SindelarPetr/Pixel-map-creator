using GameEngine.CameraEngine;
using GameEngine.Menu;
using GameEngine.Menu.ScreensAs;
using GameEngine.Options;
using GameEngine.Primitives;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Properties;

namespace PixelMapCreator.Menu.MenuLevelCreator
{
	public class Border : ScreenContainer
	{
		#region Static

		private static readonly float ButtonThick = DisplayOptions.Resolution.X / 40f;
		#endregion

		private readonly ScreenTextButton _leftPlusButton;
		private ScreenTextButton _leftMinusButton;
		private ScreenTextButton _rightPlusButton;
		private ScreenTextButton _rightMinusButton;
		private ScreenTextButton _topPlusButton;
		private ScreenTextButton _topMinusButton;
		private ScreenTextButton _botPlusButton;
		private ScreenTextButton _botMinusButton;

		#region Events
		public event Action OnLeftPlus
		{
			remove => _leftPlusButton.OnClick -= t => value?.Invoke();
			add => _leftPlusButton.OnClick += t => value?.Invoke();
		}
		public event Action OnLeftMinus
		{
			remove => _leftMinusButton.OnClick -= t => value?.Invoke();
			add => _leftMinusButton.OnClick += t => value?.Invoke();
		}
		public event Action OnRightPlus
		{
			remove => _rightPlusButton.OnClick -= t => value?.Invoke();
			add => _rightPlusButton.OnClick += t => value?.Invoke();
		}
		public event Action OnRightMinus
		{
			remove => _rightMinusButton.OnClick -= t => value?.Invoke();
			add => _rightMinusButton.OnClick += t => value?.Invoke();
		}
		public event Action OnBotPlus
		{
			remove => _botPlusButton.OnClick -= t => value?.Invoke();
			add => _botPlusButton.OnClick += t => value?.Invoke();
		}
		public event Action OnBotMinus
		{
			remove => _botMinusButton.OnClick -= t => value?.Invoke();
			add => _botMinusButton.OnClick += t => value?.Invoke();
		}
		public event Action OnTopPlus
		{
			remove => _topPlusButton.OnClick -= t => value?.Invoke();
			add => _topPlusButton.OnClick += t => value?.Invoke();
		}
		public event Action OnTopMinus
		{
			remove => _topMinusButton.OnClick -= t => value?.Invoke();
			add => _topMinusButton.OnClick += t => value?.Invoke();
		}
		#endregion

		public Border(Camera camera, Vector2 mapSize, IParentObject parent = null) : base(camera, Vector2.Zero, mapSize, parent)
		{
			_leftPlusButton = CreateButton(() => new Vector2(-widthPositionX, -widthPositionY), () => widthSize, "+");
			_leftMinusButton = CreateButton(() => new Vector2(-widthPositionX, widthPositionY), () => widthSize, "-");
			_rightPlusButton = CreateButton(() => new Vector2(widthPositionX, -widthPositionY), () => widthSize, "+");
			_rightMinusButton = CreateButton(() => new Vector2(widthPositionX, widthPositionY), () => widthSize, "-");
			_topPlusButton = CreateButton(() => new Vector2(-heightPositionX, -heightPositionY), () => heightSize, "+");
			_topMinusButton = CreateButton(() => new Vector2(heightPositionX, -heightPositionY), () => heightSize, "-");
			_botPlusButton = CreateButton(() => new Vector2(-heightPositionX, heightPositionY), () => heightSize, "+");
			_botMinusButton = CreateButton(() => new Vector2(heightPositionX, heightPositionY), () => heightSize, "-");

			OnBasicSizeChanged += v => ResolutionChanged();
		}

		private float widthPositionX => BasicSize.X / 2f + ButtonThick / 2f;
		private float widthPositionY => BasicSize.Y / 4f;
		private Vector2 widthSize => new Vector2(ButtonThick, BasicSize.Y / 2f);

		private float heightPositionX => BasicSize.X / 4f;
		private float heightPositionY => BasicSize.Y / 2f + ButtonThick / 2f;
		private Vector2 heightSize => new Vector2(BasicSize.X / 2f, ButtonThick);

		private ScreenTextButton CreateButton(Func<Vector2> positionProvider, Func<Vector2> sizeProvider, string text)
		{
			var button = new ScreenTextButton(Camera, positionProvider, sizeProvider, text, () => ButtonThick * 0.9f, () => Vector2.Zero, this);
			button.ChangeColor((MyColor)new Color(60,60,60));
			//button.OnClick += t => clickAction?.Invoke();
			AddNestedObject(button, 3);
			return button;
		}
	}
}
