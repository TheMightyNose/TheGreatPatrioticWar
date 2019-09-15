using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace TheGreatPatrioticWar
{
	static partial class World
	{
		public static Texture worldTexture;
		public static Sprite worldSprite;

		public readonly static float secondsPerDay = 0.1f;

		public static int CurrentDay { get; private set; } = 0;

		static World()
		{
			worldTexture = new Texture("..\\..\\TheWorld.png");
			worldSprite = new Sprite(worldTexture);
            Load("..\\..\\Maps\\Base");
		}

		public static void Update(float deltaTime)
		{
			Camera.Update(deltaTime);
		}
		
		public static void Draw()
		{
			worldSprite.Position = Camera.WorldToCamera(new Vector2f(0,0));
			Game.window.Draw(worldSprite);
			Grid.Draw(new Color(100,100,100));
            Grid.DrawMouseInfo();
		}

		public static void Daily()
		{
            foreach (Field field in Grid.fields)
            {
                field.Daily();
            }

            var gridPos = Camera.CameraToWorld(Camera.mousePos) / Grid.cellSize;

            Field fromField = Grid.fields[(int)gridPos.X, (int)gridPos.Y];
            Field toField = Grid.fields[(int)gridPos.X + 1, (int)gridPos.Y];

            if (!fromField.battleInProgress && fromField.armies.Exists(x => x.faction.Alliance == Faction.ALLIANCE.USSR && x.daysUntilArrival == 0) && gridPos.X < Grid.width - 1 && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                fromField.armies[0].daysUntilArrival = 10;
                toField.armies.Add(fromField.armies[0]);
                fromField.armies.Clear();
            }

            ++CurrentDay;
		}

	}
}
