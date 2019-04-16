using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatPatrioticWar
{
	class GameState : State
	{

		RectangleShape bobi = new RectangleShape(new Vector2f(20,20));
		float timePassedSinceLastDay = 0.0f;

		public override void Update(float deltaTime)
		{
			World.Update(deltaTime);

			timePassedSinceLastDay += deltaTime;

			while (timePassedSinceLastDay > World.secondsPerDay)
			{
				timePassedSinceLastDay -= World.secondsPerDay;
				Daily();
			}


			Camera.CameraPos += new Vector2f(20 * deltaTime, 20 * deltaTime);




			bobi.Position = Camera.ToWorld(new Vector2f(100, 100));
			bobi.FillColor = Color.Red;

			
		}

		public override void Draw()
		{
			Game.window.Draw(bobi);
			World.Draw();
		}

		public void Daily()
		{
			
			World.Daily();
		}
	}
}
