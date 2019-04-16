using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace TheGreatPatrioticWar
{
	static class Grid
	{
		public static int width = 50;
		public static int height = 50;
		public static int cellSize = 50;

		public static Color gridColor = Color.White;

		public static void Draw()
		{

			for (int x = 0; x < width; ++x)
			{
				var vs = new[] {
					new Vertex(Camera.ToWorld(new Vector2f(x * cellSize,0)), gridColor),
					new Vertex(Camera.ToWorld(new Vector2f(x * cellSize,height*cellSize)), gridColor),
				};
				Game.window.Draw(vs, 0, (uint)vs.Length, PrimitiveType.Lines);
			}

			for (int y = 0; y < height; ++y)
			{
				var vs = new[] {
					new Vertex(Camera.ToWorld(new Vector2f(0, y * cellSize)), gridColor),
					new Vertex(Camera.ToWorld(new Vector2f(width*cellSize, y * cellSize)), gridColor),
				};
				Game.window.Draw(vs, 0, (uint)vs.Length, PrimitiveType.Lines);
			}

		}

	}
}
