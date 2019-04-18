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
		public static Texture worldTexture;
		public static Sprite worldSprite;

		public readonly static float secondsPerDay = 1f;

		public static int CurrentDay { get; private set; } = 0;

		static World()
		{
			worldTexture = new Texture("..\\..\\TheWorld.png");
			worldSprite = new Sprite(worldTexture);
		}

		public static void Update(float deltaTime)
		{
			Camera.Update(deltaTime);
		}
		
		public static void Draw()
		{
			worldSprite.Position = Camera.ToWorld(new Vector2f(0,0));
			Game.window.Draw(worldSprite);
			Grid.Draw(new Color(100,100,100));
		}

		public static void Daily()
		{
			++CurrentDay;
		}

	}
}
