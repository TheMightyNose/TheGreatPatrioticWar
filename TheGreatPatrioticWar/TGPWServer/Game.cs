using System;
using SFML.System;

namespace TGPWServer
{
	static class Game
	{
		static float timePassedSinceLastDay = 0.0f;

		static void Main()
		{
			Clock clock = new Clock();

			while (true)
			{
				float deltaTime = clock.Restart().AsSeconds();
				Update(deltaTime);
			}
		}

		static void Update(float deltaTime)
		{
			World.Update(deltaTime);

			timePassedSinceLastDay += deltaTime;

			while(timePassedSinceLastDay > World.secondsPerDay)
			{
				timePassedSinceLastDay -= World.secondsPerDay;
				Daily();
			}
		}

		static void Daily()
		{
			Logger.Log("Game", "Day is over!");
			World.Daily();
		}
	}
}
