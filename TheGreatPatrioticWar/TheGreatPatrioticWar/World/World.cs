using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace TheGreatPatrioticWar
{
	static class World
	{
		public readonly static float secondsPerDay = 1f;

		public static int CurrentDay { get; private set; } = 0;

		public static void Update(float deltaTime)
		{

		}

		public static void Draw()
		{
			Grid.Draw();
		}

		public static void Daily()
		{
			++CurrentDay;
		}

	}
}
