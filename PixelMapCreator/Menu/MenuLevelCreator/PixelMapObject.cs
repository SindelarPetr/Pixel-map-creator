using System;
using System.Collections.Generic;
using GameEngine.CameraEngine;
using GameEngine.Menu;
using GameEngine.Pixel;
using GameEngine.Primitives;
using Microsoft.Xna.Framework;
using MrGunman.Contracts;
using PixelMapCreator.Menu.MenuLevelCreator;

namespace PixelMapCreator.Menu
{
	public abstract class PixelMapObject : FocusableButton
	{
		public string Id { get; }

		protected PixelMapObject(Camera camera, Vector2 pixelPosition, Vector2 size, MapGroup parent = null, string id = null)
			: base(camera, pixelPosition * PixelOptions.PixelSize, size, parent)
		{
			Id = id ?? Guid.NewGuid().ToString();
			IsDraggingAllowed = true;
			OnParentRemoved += PixelMapObject_OnParentRemoved;
			OnParentChanged += PixelMapObject_OnParentChanged;
		}

		private void PixelMapObject_OnParentChanged(IParentObject obj)
		{
			BasicPosition -= obj.GetGamePosition();
		}

		private void PixelMapObject_OnParentRemoved(IParentObject obj)
		{
			BasicPosition += obj.GetGamePosition();
		}

		protected override Vector2 DragValueModify(Vector2 value)
		{
			return PixelMath.GameToStrictPixelVector(value) * PixelOptions.PixelSize;
		}

		public abstract void SaveToMap(SerializableMap map);

		public void CleanMetaElements()
		{
			ForEachNestedObjects(o => o.Hide());
		}
	}
}
