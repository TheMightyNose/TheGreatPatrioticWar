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

		public static Vector2f CameraPos = new Vector2f(0, 0);

		public static Vector2f ToWorld(Vector2f v)
		{
			return v - CameraPos;
		}
	}
}
