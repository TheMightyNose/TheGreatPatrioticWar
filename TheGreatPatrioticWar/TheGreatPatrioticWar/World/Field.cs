using System;
using System.Collections.Generic;
using SFML.Graphics;
using System.Linq;
using SFML.System;

namespace TheGreatPatrioticWar
{
	class Field 
	{
		public RectangleShape bob = new RectangleShape();

		public int x;
		public int y;

		public List<Army> armies = new List<Army>();

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
			bob.FillColor = new Color((byte)civilians, 0, 0,230);
            bob.Position = new Vector2f(x, y);
			bob.Size = new Vector2f(Grid.cellSize, Grid.cellSize);

            Camera.DrawOnCamera(bob);
			
		}

        public override string ToString()
        {
            var nl = Environment.NewLine;
            var armyInfo = string.Concat(armies.Select(army =>
            {
                return $"Army: {nl}" + string.Concat(typeof(Army).GetFields().Select(field =>
                {
                    return $" - {field.Name} = {field.GetValue(army)}{nl}";
                }));
            }));

            return $"Armies: {nl}" + armyInfo;
        }


        //enum terrain

    }
}
