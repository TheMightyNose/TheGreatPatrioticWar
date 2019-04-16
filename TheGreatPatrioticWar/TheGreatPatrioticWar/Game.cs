using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace TheGreatPatrioticWar
{
	static class Game
	{
		public static RenderWindow window;

	
		static void Main()
		{
			var width = (uint)Settings.Current.windowWidth;
			var height = (uint)Settings.Current.windowHeight;
			window = new RenderWindow(new VideoMode(width, height), "Tabor", Styles.Titlebar | Styles.Close);
			Clock clock = new Clock();
			StateHandler.ChangeState(new GameState());

			window.Closed += (x, y) => window.Close();

			while (window.IsOpen)
			{
				float deltaTime = clock.Restart().AsSeconds();
				window.DispatchEvents();
				StateHandler.Update(deltaTime);
				StateHandler.Draw();
				window.Display();
				window.Clear(Color.Black);
			}
		}
	}
}
