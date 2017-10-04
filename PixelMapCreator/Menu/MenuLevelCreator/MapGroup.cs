using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using GameEngine.Menu.ScreensAs.Buttons;
using Microsoft.Xna.Framework;
using MrGunman.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Properties;

namespace PixelMapCreator.Menu.MenuLevelCreator
{
	/// <summary>
	/// Represents group of objects in the map. This groups enables focusing and unfocusing all objects int the group, to move the whole group by dragging one object and allows to create tree structure from other groups where single objects are the leafs
	/// </summary>
	public class MapGroup : PixelMapObject
	{
		private List<PixelMapObject> _members;
		protected bool IsSaveable { get; set; }

		public MapGroup(Camera camera, MapGroup parent = null, string id = null) : base(camera, Vector2.Zero, Vector2.One, parent, id)
		{
			_members = new List<PixelMapObject>();
			IsSaveable = true;

			OnFocused += OnOnFocused;
			OnUnFocused += OnOnUnFocused;


			OnColorChanged += OnOnColorChanged;
		}

		private void OnOnColorChanged(Color color)
		{
			ForEachMember(m => m.ChangeColor((MyColor)color));
		}

		private void OnOnFocused(object sender, EventArgs eventArgs)
		{
			ForEachMember(m => m.Focus());
		}

		private void OnOnUnFocused(object sender, EventArgs eventArgs)
		{
			ForEachMember(m => m.UnFocus());
		}

		/// <summary>
		/// Adds a new part to the group. This group will care about drawing and updating of the part.
		/// </summary>
		/// <param name="member">The new part for the group.</param>
		public void AddMember(PixelMapObject member)
		{
			AddNestedObject(member, 3);
			AddFocusableButton(member);
			_members.Add(member);

			member.ChangeParent(this);

			member.OnFocused += MemberOnOnFocus;
			member.OnUnFocused += MemberOnOnUnFocus;
			member.OnDragging += OnMemberDragging;
		}

		public void RemoveMember(PixelMapObject member)
		{
			RemoveNestedObject(member);
			RemoveFocusableButton(member);
			_members.Remove(member);

			member.RemoveParent();

			member.OnFocused -= MemberOnOnFocus;
			member.OnUnFocused -= MemberOnOnUnFocus;
			member.OnDragging -= OnMemberDragging;
		}

		protected virtual void OnMemberDragging(ScreenButton sender, Vector2 value)
		{
			if (Parent == null) BasicPosition += value;
			else CallDragging(sender, value);
		}

		protected virtual void MemberOnOnUnFocus(object sender, EventArgs args)
		{
			UnFocus();
		}

		protected virtual void MemberOnOnFocus(object sender, EventArgs args)
		{
			Focus();
		}

		public void RemoveAllMembers()
		{
			ForEachMember(RemoveMember);
		}

		public void ForEachMember(Action<PixelMapObject> action)
		{
			_members.ForEach(action);
		}

		public override void SaveToMap(SerializableMap map)
		{
			ForEachMember(m => m.SaveToMap(map));

			if(IsSaveable)
			map.Groups.Add(ToSerializableGroup());
		}

		private SerializableGroup ToSerializableGroup()
		{
			return new SerializableGroup
			{
				Id = Id,
				MemberIds = _members.Select(o => o.Id).ToArray()
			};
		}

		internal void FOO()
		{ }
	}
}
