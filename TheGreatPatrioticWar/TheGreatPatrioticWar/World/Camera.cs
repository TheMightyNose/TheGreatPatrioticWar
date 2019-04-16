using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace TheGreatPatrioticWar
{
	static class Camera
	{

		static Vector2f cameraSpeed = new Vector2f(0,0);
		static float cameraMultiplier = 500;

		static readonly int moveMargin = 50;

		static Camera()
		{
			Game.window.MouseMoved += OnMouseMoved;
		}

		public static void Update(float time)
		{
			CameraPos += cameraSpeed * cameraMultiplier * time;

			if (CameraPos.X < 0) CameraPos = new Vector2f(0,CameraPos.Y);
			if (CameraPos.Y < 0) CameraPos = new Vector2f(CameraPos.X,0);

			int w = Settings.Current.windowWidth;
			int h = Settings.Current.windowHeight;

			if (CameraPos.X + w > Grid.width * Grid.cellSize) CameraPos = new Vector2f(Grid.width * Grid.cellSize - w,CameraPos.Y);
			if (CameraPos.Y + h > Grid.height * Grid.cellSize) CameraPos = new Vector2f(CameraPos.X,Grid.height * Grid.cellSize - h);

		}


		public static void OnMouseMoved(object sender, MouseMoveEventArgs blob)
		{

			cameraSpeed = new Vector2f(0, 0);
			if (!Game.window.HasFocus())
			{
				return;
			}

			if (blob.X < moveMargin)
				cameraSpeed += new Vector2f(-1, 0);
			if (blob.X > Settings.Current.windowWidth - moveMargin)
				cameraSpeed += new Vector2f(1, 0);
			if (blob.Y < moveMargin)
				cameraSpeed += new Vector2f(0, -1);
			if (blob.Y > Settings.Current.windowHeight - moveMargin)
				cameraSpeed += new Vector2f(0, 1);
		}

		public static Vector2f CameraPos = new Vector2f(0, 0);

		public static Vector2f ToWorld(Vector2f v)
		{
			return v - CameraPos;
		}
	}
}
