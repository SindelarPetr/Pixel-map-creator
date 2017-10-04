using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using GameEngine.Menu.Screens.TextureObjects;
using GameEngine.Menu.ScreensAs.Buttons;
using GameEngine.Pixel;
using GameEngine.Primitives;
using GameEngine.Properties;
using GameEngine.RunBasics;
using Microsoft.Xna.Framework;
using MrGunman.Contracts;
using PixelMapCreator.Menu.ColorPicker;
using PixelMapCreator.Menu.MenuLevelCreator;
using System.Collections.Generic;
using GameEngine.Menu.Screens;

namespace PixelMapCreator.Menu
{
	public sealed class PixelMap : ScreenButton
	{
		private readonly List<PixelMapObject> _mapObjects;
		private readonly MapControlGroup _unfocusedMapObjects;
		private readonly MapControlGroup _focusedMapObjects;

		public Vector2 IndexSize
		{
			get => BasicSize / PixelOptions.PixelSize;
			set => BasicSize = value * PixelOptions.PixelSize;
		}

		private readonly Border _border;

		private readonly ScreenCover _cover;

		#region Construct
		public PixelMap(Camera camera, Vector2 pixelCount, IScreenParentObject parent = null)
			: base(camera, Vector2.Zero, pixelCount * PixelOptions.PixelSize, parent)
		{
			_mapObjects = new List<PixelMapObject>();
			_focusedMapObjects = CreateFocusedGroup();

			_cover = new ScreenCover(Color.Black);
			AddNestedObject(_cover, 5);

			_unfocusedMapObjects = CreateUnfocusedGroup();

			IsTouchable = false;

			camera.OnClick += t => UnfocusAll();

			_border = CreateBorder();

			ChangeColor((MyColor)Color.Black);
			//ScreenColorPicker.Instance.OnColorPickerValueChanged += InstanceOnOnColorPickerValueChanged;
		}

		public override void AllScreensLoaded()
		{
			base.AllScreensLoaded();

			MenuScreenManager.GetScreen<ScreenColorPicker>().OnColorPickerValueChanged += OnOnColorPickerValueChanged;
		}

		private void OnOnColorPickerValueChanged(Color color)
		{
			_focusedMapObjects.ForEachMember(m => m.ChangeColor((MyColor)color));
			CleanMetaElements();
		}

		private MapControlGroup CreateFocusedGroup()
		{
			var group = new MapControlGroup(Camera);
			group.OnMemberChangedFocus += FocusedMapObjectsOnOnMemberChangedFocus;
			group.DragWholeGroup = true;
			AddNestedObject(group, 6);
			return group;
		}

		private MapControlGroup CreateUnfocusedGroup()
		{
			var group = new MapControlGroup(Camera);
			group.OnMemberChangedFocus += FocusedMapObjectsOnOnMemberChangedFocus;
			AddNestedObject(group, 4);

			return group;
		}

		#region Border
		private Border CreateBorder()
		{
			var border = new Border(Camera, BasicSize, this);
			AddNestedObject(border, 5);

			border.OnLeftPlus += () =>
			{
				AddWidth();
				MoveAllRight();
			};

			border.OnLeftMinus += () =>
			{
				RemoveWidth();
				MoveAllLeft();
			};

			border.OnRightPlus += () =>
			{
				AddWidth();
				MoveAllLeft();
			};

			border.OnRightMinus += () =>
			{
				RemoveWidth();
				MoveAllRight();
			};

			border.OnTopPlus += () =>
			{
				AddHeight();
				MoveAllBot();
			};

			border.OnTopMinus += () =>
			{
				RemoveHeight();
				MoveAllTop();
			};

			border.OnBotPlus += () =>
			{
				AddHeight();
				MoveAllTop();
			};

			border.OnBotMinus += () =>
			{
				RemoveHeight();
				MoveAllBot();
			};

			OnBasicSizeChanged += s => border.BasicSize = BasicSize;

			return border;
		}

		#region Size change
		private void ChangeSize(Vector2 changeValue)
		{
			BasicSize += changeValue;
			_border.BasicSize = BasicSize;
		}

		private void AddWidth()
		{
			ChangeSize(new Vector2(2 * PixelOptions.PixelSize.X, 0));
		}

		private void RemoveWidth()
		{
			ChangeSize(new Vector2(-2 * PixelOptions.PixelSize.X, 0));
		}

		private void AddHeight()
		{
			ChangeSize(new Vector2(0, 2 * PixelOptions.PixelSize.Y));
		}

		private void RemoveHeight()
		{
			ChangeSize(new Vector2(0, -2 * PixelOptions.PixelSize.Y));

		}
		#endregion

		#region Move all objects
		private void MoveAllLeft()
		{
			MoveAllObjects(-new Vector2(PixelOptions.PixelSize.X, 0));
		}
		private void MoveAllRight()
		{
			MoveAllObjects(new Vector2(PixelOptions.PixelSize.X, 0));
		}
		private void MoveAllTop()
		{
			MoveAllObjects(new Vector2(0, -PixelOptions.PixelSize.Y));
		}

		private void MoveAllBot()
		{
			MoveAllObjects(new Vector2(0, PixelOptions.PixelSize.Y));
		}

		private void MoveAllObjects(Vector2 value)
		{
			_unfocusedMapObjects.ForEachMember(m => m.BasicPosition += value);
		}
		#endregion
		#endregion

		private void FocusedMapObjectsOnOnMemberChangedFocus(PixelMapObject pixelMapObject)
		{
			if (pixelMapObject.IsFocused) _focusedMapObjects.AddMember(pixelMapObject);
			else _unfocusedMapObjects.AddMember(pixelMapObject);

			if (_focusedMapObjects.FocusableButtonsCount > 0)
			{
				_cover.Show();
				MenuScreenManager.GetScreen<ScreenColorPicker>().Show(null);

				if (pixelMapObject.IsFocused)
				{
					MenuScreenManager.GetScreen<ScreenColorPicker>().QuietResetColor(pixelMapObject.DefaultColor);
				}
			}
			else
			{
				_cover.Hide();
				MenuScreenManager.GetScreen<ScreenColorPicker>().Hide();
			}
		}
		#endregion

		public void UnfocusAll()
		{
			_focusedMapObjects.ForEachFocusableButton(b => b.UnFocus());
		}

		private void AddPixelMapObject(PixelMapObject obj)
		{
			_mapObjects.Add(obj);
			_unfocusedMapObjects.AddMember(obj);
		}

		private void RemovePixelMapObject(PixelMapObject obj)
		{
			_mapObjects.Remove(obj);

			if (obj.IsFocused) obj.UnFocus();

			_unfocusedMapObjects.RemoveMember(obj);
		}

		#region Objects creation
		public void CreatePixelRectangle()
		{
			var rectangle = new PixelRectangle(Camera, Vector2.Zero, new Vector2(6));
			rectangle.Show();
			//_pixelRectangles.Add(rectangle);
			AddPixelMapObject(rectangle);
		}

		public void CreateGroup()
		{
			if (_focusedMapObjects.FocusableButtonsCount <= 1) return;

			MapGroup newGroup = new MapGroup(Camera);
			AddPixelMapObject(newGroup);

			_focusedMapObjects.ForEachMember(m =>
			{
				_focusedMapObjects.RemoveMember(m);
				newGroup.AddMember(m);
			});

			newGroup.Focus();
		}
		#endregion

		#region Serialization
		public SerializableMap GetSerializableMap()
		{
			var map = new SerializableMap(IndexSize);

			FillMap(map);

			return map;
		}

		private void FillMap(SerializableMap map)
		{
			_unfocusedMapObjects.ForEachMember(o => o.SaveToMap(map));
			_focusedMapObjects.ForEachMember(o => o.SaveToMap(map));
		}
		#endregion


		public void DestroyGroup()
		{
			List<PixelMapObject> newObjects = new List<PixelMapObject>();
			_focusedMapObjects.ForEachMember(o =>
				{
					var group = o as MapGroup;
					if (group == null) return;

					group.ForEachMember(go =>
					{
						group.RemoveMember(go);
						newObjects.Add(go);
					});

					//_mapGroups.Remove(group);
					_mapObjects.Remove(group);
				});

			newObjects.ForEach(o => _focusedMapObjects.AddMember(o));
		}

		#region Load
		public void LoadFromMap(SerializableMap map)
		{
			var deserializedObjects = new Dictionary<string, PixelMapObject>();

			IndexSize = map.IndexSize;

			DeserializeRectangles(map, deserializedObjects);

			DeserializeGroups(map, deserializedObjects);

			deserializedObjects.ForEach(p => AddPixelMapObject(p.Value));
		}

		private void DeserializeRectangles(SerializableMap map, Dictionary<string, PixelMapObject> deserializedObjects)
		{
			map.Rectangles.ForEach(r =>
			{
				var basicSize = PixelMath.PixelToGameVector(r.IndexSize);
				var position = PixelMath.GameToPixelVector(PixelRectangle.GetMiddlePosition(r.LeftTopIndexPosition, basicSize, IndexSize));
				var rectangle = new PixelRectangle(Camera, position, r.IndexSize, null, r.Id);
				deserializedObjects.Add(rectangle.Id, rectangle);
			});
		}

		private void DeserializeGroups(SerializableMap map, Dictionary<string, PixelMapObject> deserializedObjects)
		{
			map.Groups.ForEach(g =>
			{
				var group = new MapGroup(Camera, null, g.Id);
				g.MemberIds.ForEach(id =>
				{
					group.AddMember(deserializedObjects[id]);
					deserializedObjects.Remove(id);
				});

				deserializedObjects.Add(group.Id, group);
			});
		}
		#endregion

		public void RemoveSelection()
		{
			_focusedMapObjects.ForEachMember(RemovePixelMapObject);
		}

		/// <summary>
		/// Hides all meta elements so the selection will look like in the game.
		/// </summary>
		public void CleanMetaElements()
		{
			_focusedMapObjects.ForEachMember(m => m.CleanMetaElements());
		}
	}
}
