using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace TheGreatPatrioticWar
{
	class Field 
	{
		public RectangleShape bob = new RectangleShape();

		public int x;
		public int y;

		public List<Army> armies;

		public Global.Faction owner;

		public float civilians;
		public int droppedBombs;
		public int craters;

		public Field(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public void Draw()
		{
			bob.FillColor = new Color((byte)civilians, 0, 0,100);
            bob.Position = new Vector2f(x, y);
			bob.Size = new Vector2f(Grid.cellSize, Grid.cellSize);

            Camera.DrawOnWorld(bob);
			
		}


		//enum terrain

	}
}
