using GameEngine.CameraEngine;
using System;
using GameEngine.Menu.ScreensAs.Buttons;
using Microsoft.Xna.Framework;

namespace PixelMapCreator.Menu.MenuLevelCreator
{
	public class MapControlGroup : MapGroup
	{
		public event Action<PixelMapObject> OnMemberChangedFocus;

		public bool DragWholeGroup { get; set; }

		public MapControlGroup(Camera camera, MapGroup parent = null) : base(camera, parent)
		{
			IsSaveable = false;
		}

		protected override void MemberOnOnFocus(object sender, EventArgs args)
		{
			FocusChanged((PixelMapObject)sender);
		}

		protected override void MemberOnOnUnFocus(object sender, EventArgs args)
		{
			FocusChanged((PixelMapObject)sender);
		}

		protected override void OnMemberDragging(ScreenButton button, Vector2 value)
		{
			if (DragWholeGroup) base.OnMemberDragging(button, value);
			else
			{
				button.BasicPosition += value;
			}
		}

		private void FocusChanged(PixelMapObject obj)
		{
			RemoveMember(obj);

			OnMemberChangedFocus?.Invoke(obj);
		}
	}
}
