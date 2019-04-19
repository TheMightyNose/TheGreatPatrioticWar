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
		public static int width = 150;
		public static int height = 100;
		public static int cellSize = 20;

        static Text tekst = new Text();
        static Font bobtabor = new Font(@"C:\Windows\Fonts\Arial.ttf");

		public static Field[,] fields = new Field[width, height];

		static Grid()
		{
            tekst.Font = bobtabor;
            tekst.CharacterSize = "!Bob"[0];

			Random bob = new Random();

			for (int y = 0; y < height; ++y)
			{
				for (int x  = 0; x < width; ++x)
				{
                    fields[x, y] = new Field(x * cellSize, y * cellSize)
                    {
                        civilians = bob.Next(0, 2) * 50,
                        armies = new List<Army>() { new Army(Faction.Germany, bob.Next(0,2255), 1), new Army(Faction.Finland,34,12538)},
                  
                    };
                }
			}
        }


		public static void Draw(Color gridColor)
        { 
            
			for (int x = 0; x < width; ++x)
			{
				var vs = new[] {
					new Vertex(Camera.WorldToCamera(new Vector2f(x * cellSize,0)), gridColor),
					new Vertex(Camera.WorldToCamera(new Vector2f(x * cellSize,height*cellSize)), gridColor),
				};
				Game.window.Draw(vs, 0, (uint)vs.Length, PrimitiveType.Lines);
			}

			for (int y = 0; y < height; ++y)
			{
				var vs = new[] {
					new Vertex(Camera.WorldToCamera(new Vector2f(0, y * cellSize)), gridColor),
					new Vertex(Camera.WorldToCamera(new Vector2f(width*cellSize, y * cellSize)), gridColor),
				};
				Game.window.Draw(vs, 0, (uint)vs.Length, PrimitiveType.Lines);
			}
            
			for (int y = 0; y < height; ++y)
			{
				for (int x = 0; x < width; ++x)
				{
					fields[x, y].Draw();
				}
			}
		}

        public static void DrawMouseInfo()
        {
            int offset = 20;
            var gridPos = Camera.CameraToWorld(Camera.mousePos) / cellSize;
            tekst.FillColor = Color.Red;
            tekst.OutlineThickness = 2;
            tekst.OutlineColor = Color.Green;
            if (gridPos.X < width && gridPos.X > 0 && gridPos.Y < height && gridPos.Y > 0)
            {
                tekst.Position = Camera.mousePos + new Vector2f(offset,offset);
                tekst.DisplayedString = fields[(int)gridPos.X, (int)gridPos.Y].ToString();
                Game.window.Draw(tekst);
            }

        }

	}
}
