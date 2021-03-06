﻿using System;
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
		const float cameraMultiplier = 500;
        public static Vector2f mousePos = new Vector2f(0, 0);

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

            mousePos = new Vector2f(blob.X, blob.Y);

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


        public static bool DrawOnCamera(Shape s)
        {
            int Marign = -Grid.cellSize;
            var oldPos = s.Position;
            s.Position = WorldToCamera(s.Position);
            var bounds = s.GetGlobalBounds();

            if (bounds.Left + bounds.Width < Settings.Current.windowWidth - Marign
                && bounds.Left > 0 + Marign
                && bounds.Top + bounds.Height < Settings.Current.windowHeight - Marign
                && bounds.Top > 0 + Marign)
            {

                Game.window.Draw(s);
                return true;
               
            }
            s.Position = oldPos;
            return false;
        }


		public static Vector2f WorldToCamera(Vector2f pos)
		{
            return pos - CameraPos;
		}

        public static Vector2f CameraToWorld(Vector2f pos)
        {
            return pos + CameraPos;
        }

	}
}
