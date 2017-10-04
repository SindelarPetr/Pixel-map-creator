using System;
using GameEngine.CameraEngine;
using GameEngine.Menu.ScreensAs.Buttons;
using GameEngine.Pixel;
using GameEngine.RunBasics;
using Microsoft.Xna.Framework;
using MrGunman.Contracts;
using PixelMapCreator.Menu.MenuLevelCreator;
using PixelMapCreator.Menu.Pixels;

namespace PixelMapCreator.Menu
{
	public class PixelRectangle : PixelMapObject
	{
		#region Constants

		private const float DRAGABLE_CORNER_OPACITY = 0.30f;
		private const float DRAGABLE_SIDE_OPACITY = 0.20f;
		#endregion

		#region Static
		#region Corners
		private static Vector2 CornerDragableSize => PixelOptions.PixelSize * 2;
		private static Vector2 GetDragableLeftTopPosition(Vector2 ownerSize) => new Vector2(-GetDragableRightBotPosition(ownerSize).X, -GetDragableRightBotPosition(ownerSize).Y);
		private static Vector2 GetDragableLeftBotPosition(Vector2 ownerSize) => new Vector2(-GetDragableRightBotPosition(ownerSize).X, GetDragableRightBotPosition(ownerSize).Y);
		private static Vector2 GetDragableRightTopPosition(Vector2 ownerSize) => new Vector2(GetDragableRightBotPosition(ownerSize).X, -GetDragableRightBotPosition(ownerSize).Y);
		private static Vector2 GetDragableRightBotPosition(Vector2 ownerSize) => ownerSize / 2f + CornerDragableSize / 2f - PixelOptions.PixelSize / 2f;
		#endregion

		#region Sides
		private static Vector2 HorizontalSideSize(Vector2 ownerSize) => new Vector2(GetDragableRightBotPosition(ownerSize).X * 2 - CornerDragableSize.X, CornerDragableSize.Y);
		private static Vector2 VerticalSideSize(Vector2 ownerSize) => new Vector2(CornerDragableSize.X, GetDragableRightBotPosition(ownerSize).Y * 2 - CornerDragableSize.Y);
		private Vector2 GetDragableTopPosition(Vector2 ownerSize) => new Vector2(0, GetDragableLeftTopPosition(ownerSize).Y);
		private Vector2 GetDragableLeftPosition(Vector2 ownerSize) => new Vector2(GetDragableLeftTopPosition(ownerSize).X, 0);
		private Vector2 GetDragableBotPosition(Vector2 ownerSize) => -GetDragableTopPosition(ownerSize);
		private Vector2 GetDragableRightPosition(Vector2 ownerSize) => -GetDragableLeftPosition(ownerSize);
		#endregion

		public static Vector2 GetLeftTopIndexPosition(Vector2 basicPosition, Vector2 basicSize)
		{
			return PixelMath.GameToPixelPosition(basicPosition - basicSize / 2f + PixelOptions.PixelSize / 2, MenuScreenManager.GetScreen<ScreenLevelCreator>().MapSize);
		}

		public static Vector2 GetMiddlePosition(Vector2 leftTopIndexPosition, Vector2 basicSize, Vector2 pixelsCountInMap)
		{
			var result = PixelMath.PixelToGamePosition(leftTopIndexPosition, pixelsCountInMap) + basicSize / 2f -
						 PixelOptions.PixelSize;
			return result;
		}
		#endregion

		#region Dragables
		private readonly PixelDragable _dragableLeftTop;
		private readonly PixelDragable _dragableLeftBot;
		private readonly PixelDragable _dragableRightTop;
		private readonly PixelDragable _dragableRightBot;

		private readonly PixelDragable _dragableTop;
		private readonly PixelDragable _dragableLeft;
		private readonly PixelDragable _dragableRight;
		private readonly PixelDragable _dragableBot;
		#endregion

		public PixelRectangle(Camera camera, Vector2 position, Vector2 pixelSize, MapGroup parent = null, string id = null)
			: base(camera, position, pixelSize * PixelOptions.PixelSize, parent, id)
		{
			#region Corners
			_dragableLeftTop = CreateCornerDragable(() => GetDragableLeftTopPosition(BasicSize), DragableLeftTopOnDragging);
			_dragableLeftBot = CreateCornerDragable(() => GetDragableLeftBotPosition(BasicSize), DragableLeftBotOnDragging);
			_dragableRightTop = CreateCornerDragable(() => GetDragableRightTopPosition(BasicSize), DragableRightTopOnDragging);
			_dragableRightBot = CreateCornerDragable(() => GetDragableRightBotPosition(BasicSize), DragableRightBotOnDragging);
			#endregion

			#region Sides
			_dragableTop = CreateDragable(() => GetDragableTopPosition(BasicSize), () => HorizontalSideSize(BasicSize), DragableTop_OnDragging, DRAGABLE_SIDE_OPACITY);
			_dragableLeft = CreateDragable(() => GetDragableLeftPosition(BasicSize), () => VerticalSideSize(BasicSize), DragableLeft_OnDragging, DRAGABLE_SIDE_OPACITY);
			_dragableBot = CreateDragable(() => GetDragableBotPosition(BasicSize), () => HorizontalSideSize(BasicSize), DragableBot_OnDragging, DRAGABLE_SIDE_OPACITY);
			_dragableRight = CreateDragable(() => GetDragableRightPosition(BasicSize), () => VerticalSideSize(BasicSize), DragableRight_OnDragging, DRAGABLE_SIDE_OPACITY);
			#endregion
		}

		private PixelDragable CreateCornerDragable(Func<Vector2> positionProvider,
			Action<ScreenButton, Vector2> draggingAction)
		{
			return CreateDragable(positionProvider, () => CornerDragableSize, draggingAction, DRAGABLE_CORNER_OPACITY);
		}
		private PixelDragable CreateDragable(Func<Vector2> positionProvider, Func<Vector2> sizeProvider, Action<ScreenButton, Vector2> draggingMethod, float opacity)
		{
			var dragable = new PixelDragable(Camera, positionProvider, sizeProvider, this);
			dragable.OnDragging += draggingMethod;
			AddDragable(dragable, opacity);

			return dragable;
		}

		private void AddDragable(PixelDragable dragable, float defaultOpacity)
		{
			dragable.SmoothOpacity.ResetValue(0);
			dragable.DefaultOpacity = defaultOpacity;
			AddNestedObject(dragable, 3);
			AddFocusableButton(dragable);
		}


		#region Reposition
		private void RepositionDragables()
		{
			RepositionCorners();

			RepositionSides();
		}

		private void RepositionCorners()
		{
			_dragableLeftTop.BasicPosition = GetDragableLeftTopPosition(BasicSize);
			_dragableLeftBot.BasicPosition = GetDragableLeftBotPosition(BasicSize);
			_dragableRightBot.BasicPosition = GetDragableRightBotPosition(BasicSize);
			_dragableRightTop.BasicPosition = GetDragableRightTopPosition(BasicSize);
		}

		private void RepositionSides()
		{
			_dragableTop.BasicPosition = GetDragableTopPosition(BasicSize);
			_dragableTop.BasicSize = HorizontalSideSize(BasicSize);

			_dragableLeft.BasicPosition = GetDragableLeftPosition(BasicSize);
			_dragableLeft.BasicSize = VerticalSideSize(BasicSize);

			_dragableBot.BasicPosition = GetDragableBotPosition(BasicSize);
			_dragableBot.BasicSize = HorizontalSideSize(BasicSize);

			_dragableRight.BasicPosition = GetDragableRightPosition(BasicSize);
			_dragableRight.BasicSize = VerticalSideSize(BasicSize);
		}
		#endregion

		#region Sides
		private void DragableTop_OnDragging(ScreenButton sender, Vector2 vector)
		{
			DragableLeftTopOnDragging(sender, new Vector2(0, vector.Y));
		}
		private void DragableLeft_OnDragging(ScreenButton sender, Vector2 vector)
		{
			DragableLeftTopOnDragging(sender, new Vector2(vector.X, 0));
		}
		private void DragableBot_OnDragging(ScreenButton sender, Vector2 vector)
		{
			DragableRightBotOnDragging(sender, new Vector2(0, vector.Y));
		}
		private void DragableRight_OnDragging(ScreenButton sender, Vector2 vector)
		{
			DragableRightBotOnDragging(sender, new Vector2(vector.X, 0));
		}
		#endregion

		#region Corners
		private void DragableLeftTopOnDragging(ScreenButton sender, Vector2 vector)
		{
			Vector2 cornerMatrix = new Vector2(-1, -1);
			FinishDrag(vector, cornerMatrix);
		}
		private void DragableRightBotOnDragging(ScreenButton sender, Vector2 vector)
		{
			Vector2 cornerMatrix = new Vector2(1, 1);
			FinishDrag(vector, cornerMatrix);
		}
		private void DragableLeftBotOnDragging(ScreenButton sender, Vector2 vector)
		{
			Vector2 cornerMatrix = new Vector2(-1, 1);
			FinishDrag(vector, cornerMatrix);
		}
		private void DragableRightTopOnDragging(ScreenButton sender, Vector2 vector)
		{
			Vector2 cornerMatrix = new Vector2(1, -1);
			FinishDrag(vector, cornerMatrix);
		}

		private void FinishDrag(Vector2 dragValue, Vector2 cornerMatrix)
		{
			Vector2 originSize = BasicSize;
			BasicSize += dragValue * cornerMatrix;
			CheckSize();

			Vector2 sizeDiff = BasicSize - originSize;


			BasicPosition += sizeDiff / 2f * cornerMatrix;

			RepositionDragables();
		}
		#endregion

		private void CheckSize()
		{
			BasicSize = new Vector2(CheckSideSize(BasicSize.X), CheckSideSize(BasicSize.Y));
		}

		private float CheckSideSize(float side)
		{
			if (side < PixelOptions.PixelSideSize)
				return PixelOptions.PixelSideSize;
			return side;
		}

		public override void SaveToMap(SerializableMap map)
		{
			map.Rectangles.Add(GetSerializableRectangle());
		}
		private SerializableRectangle GetSerializableRectangle()
		{
			return new SerializableRectangle
			{
				Color = ColorChanger.MyColorToGo,
				IndexSize = BasicSize / PixelOptions.PixelSize,
				LeftTopIndexPosition = GetLeftTopIndexPosition(BasicPosition, BasicSize),
				Id = Id
			};
		}
	}
}
