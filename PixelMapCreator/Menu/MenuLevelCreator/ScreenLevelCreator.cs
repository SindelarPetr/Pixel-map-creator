using GameEngine.CameraEngine;
using GameEngine.Menu;
using GameEngine.Menu.Screens;
using GameEngine.Pixel;
using GameEngine.Properties;
using GameEngine.RunBasics;
using Microsoft.Xna.Framework;
using MrGunman.Contracts;
using Newtonsoft.Json;
using PixelMapCreator.Menu.Palette;

namespace PixelMapCreator.Menu
{
	[MainScreen]
	public class ScreenLevelCreator : MenuScreen
	{

		#region Static part
		public Vector2 MapSize => _map.BasicSize / PixelOptions.PixelSize;

		public static MyColor MenuColor = new MyColor(new Color(0, 76, 146));

		private static SerializableMap DeserializeMap(string data)
		{
			return JsonConvert.DeserializeObject<SerializableMap>(data);
		}
		#endregion

		private readonly PixelMap _map;
		private readonly Camera _mapCamera;

		public ScreenLevelCreator(Camera camera) : base(camera)
		{
			DrawPriorities = DrawPriorities.MiddleBottom;
			_mapCamera = new Camera();
			_mapCamera.Zoomer.BasicZoom = 0.5f;
			AddNestedObject(_map = new PixelMap(_mapCamera, new Vector2(100, 40), this), 3);
		}

		public override void Update()
		{
			base.Update();
			_mapCamera.Update();
		}

		public void CreatePixelRectangle()
		{
			_map.CreatePixelRectangle();
		}

		public string SerializeMap()
		{
			return JsonConvert.SerializeObject(_map.GetSerializableMap());
		}

		public void CreateGroup()
		{
			_map.CreateGroup();
		}

		public void DestroyGroup()
		{
			_map.DestroyGroup();
		}

		public void LoadMap(string data)
		{
			var map = DeserializeMap(data);

			_map.LoadFromMap(map);
		}

		public void RemoveSelection()
		{
			_map.RemoveSelection();
		}

		public override void Show(IMenuScreenElement enabler)
		{
			base.Show(enabler);

			MenuScreenManager.GetScreen<ScreenPalettes>().Show();
		}
	}
}
