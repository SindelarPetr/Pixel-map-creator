using System;

namespace PixelMapCreator.Menu
{
	public interface IFocusableObject
	{
		event EventHandler OnFocused;
		event EventHandler OnUnFocused;

		bool IsFocused { get; }

		void Focus();

		void UnFocus();
	}
}
