using System;
using GameEngine.CameraEngine;
using GameEngine.Menu.Screens;
using GameEngine.Pixel;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;

namespace PixelMapCreator.Menu.Pixels
{
	public class PixelDragable : FocusableButton
	{
		public PixelDragable(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider, IScreenParentObject parent = null) : base(camera, positionProvider, sizeProvider, parent)
		{
			IsDraggingAllowed = true;
			IsUnfocusedClickAllowed = false;

			ChangeColor(new MyColor(Color.LightGray));
		}

		protected override Vector2 DragValueModify(Vector2 value)
		{
			return PixelMath.GameToStrictPixelVector(value) * PixelOptions.PixelSize;
		}
	}
}
