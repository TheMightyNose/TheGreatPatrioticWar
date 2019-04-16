using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatPatrioticWar
{
	static class StateHandler
	{
		private static State currentState;

		public static void ChangeState(State bob)
		{
			currentState = bob; 
		}

		public static void Update(float time) => currentState.Update(time);

		public static void Draw() => currentState.Draw();
	}

	abstract class State
	{
		public abstract void Draw();
		public abstract void Update(float time);
	}
}
