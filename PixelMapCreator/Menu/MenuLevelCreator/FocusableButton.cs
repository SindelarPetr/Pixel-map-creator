using GameEngine.CameraEngine;
using GameEngine.Input;
using GameEngine.MathEngine;
using GameEngine.Menu.ScreensAs.Buttons;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GameEngine.Menu.Screens;

namespace PixelMapCreator.Menu
{
	public class FocusableButton : ScreenButton, IFocusableObject
	{
		public bool IsFocused
		{
			get;
			private set;
		}
		protected bool IsUnfocusedClickAllowed { get; set; }

		public int FocusableButtonsCount => _focusableObjects.Count;
		private readonly List<FocusableButton> _focusableObjects;

		public event EventHandler OnFocused;
		public event EventHandler OnUnFocused;

		#region Constructors
		public FocusableButton(Camera camera, Vector2 position, Vector2 size, IScreenParentObject parent = null)
			: base(camera, position, size, parent)
		{
			Construct(out _focusableObjects);
		}

		public FocusableButton(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider, IScreenParentObject parent = null) : base(camera, positionProvider, sizeProvider, parent)
		{
			Construct(out _focusableObjects);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Construct(out List<FocusableButton> focusableObjects)
		{
			focusableObjects = new List<FocusableButton>();
			OnClick += Focusable_OnClick;
			IsUnfocusedClickAllowed = true;
		} 

		private void Focusable_OnClick(MyTouch e)
		{
			if (IsFocused) UnFocus();
			else Focus();
		}
		#endregion

		public void AddFocusableButton(FocusableButton focusableButton)
		{
			_focusableObjects.Add(focusableButton);
		}

		public void RemoveFocusableButton(FocusableButton focusableButton)
		{
			_focusableObjects.Remove(focusableButton);
		}

		public void ForEachFocusableButton(Action<FocusableButton> action)
		{
			_focusableObjects.ForEach(action);
		}

		public void Focus()
		{
			if (IsFocused) return;

			IsFocused = true;

			_focusableObjects.ForEach(o => o.Focus());

			OnFocused?.Invoke(this, null);

			SetFocusStyle();
		}

		protected virtual void SetFocusStyle()
		{
			SmoothOpacity.ValueToGo = DefaultOpacity;
		}

		public void UnFocus()
		{
			if (!IsFocused) return;

			IsFocused = false;

			_focusableObjects.ForEach(o => o.UnFocus());

			OnUnFocused?.Invoke(this, null);

			SetUnFocusStyle();
		}

		protected virtual void SetUnFocusStyle()
		{
			if (!IsUnfocusedClickAllowed)
				SmoothOpacity.ValueToGo = 0;
		}

		protected override bool CanBeTouched()
		{
			return base.CanBeTouched() && (IsFocused || IsUnfocusedClickAllowed);
		}
	}
}
