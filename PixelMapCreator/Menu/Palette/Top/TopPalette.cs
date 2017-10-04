using GameEngine.CameraEngine;
using GameEngine.Input;
using GameEngine.Options;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Menu.Screens;

namespace PixelMapCreator.Menu.Palette
{
	public class TopPalette : BasicPalette
	{
		#region Static
		public static Vector2 PaletteSize => new Vector2(DisplayOptions.Resolution.X, DisplayOptions.Resolution.Y / 13);
		public static Vector2 PalettePosition => new Vector2(0, -DisplayOptions.MiddleOfScreen.Y + PaletteSize.Y / 2);
		#endregion

		public event Action OnSave;
		public event Action OnLoad;
		public event Action OnGroup;
		public event Action OnUnGroup;
		public event Action OnRemove;
		public event Action OnUndoRemove;
		public event Action OnDuplicate;

		public TopPalette(Camera camera, IScreenParentObject parent = null, MyTexture2D texture = null) : base(camera, PalettePosition, PaletteSize, parent, texture)
		{
			CreatePaletteButton("Save", t => OnSave?.Invoke());
			CreatePaletteButton("Load", t => OnLoad?.Invoke());
			CreatePaletteButton("Group", t => OnGroup?.Invoke());
			CreatePaletteButton("Ungroup", t => OnUnGroup?.Invoke());
			CreatePaletteButton("Remove", t => OnRemove?.Invoke());
			CreatePaletteButton("Undo remove", t => OnUndoRemove?.Invoke());
			CreatePaletteButton("Duplicate", t => OnDuplicate?.Invoke());

			ColorChanger.ResetColor(Color.LightGray);
		}

		private void CreatePaletteButton(string text, Action<MyTouch> clickAction)
		{
			int count = PaletteObjects.Count;
			var button = new PaletteObject(Camera, () => GetPaletteObjectPosition(count), () => PaletteObjectSize, text, null, null, this);
			button.OnClick += clickAction;
			AddPaletteObject(button);
		}
	}
}
