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
		}

		public override void Draw()
		{
			World.Draw();
		}

		public void Daily()
		{
			
			World.Daily();
		}
	}
}
