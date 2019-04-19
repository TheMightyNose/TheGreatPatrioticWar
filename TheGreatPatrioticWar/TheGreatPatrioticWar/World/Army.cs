﻿using System.Collections.Generic;

namespace TheGreatPatrioticWar
{
	public class Army
	{
		public Global.Faction faction;
		public float infantry;
		public float tanks;

		public int daysUntilArival = 0;
		//destination

		public bool dead = false;

		public Army(Global.Faction faction, int infantry, int tanks)
		{
			this.faction = faction;
			this.infantry = infantry;
			this.tanks = tanks;
		}

		public static List<Army> MergeArmies(List<Army> armies)
		{
			foreach(Army army in armies)
			{
                if (army.infantry == 0 && army.tanks == 0)
                {
                    army.dead = true;
                }
				if (army.daysUntilArival == 0 && !army.dead)
				{
					foreach (Army secondArmy in armies)
					{
						if (army != secondArmy && army.faction == secondArmy.faction && secondArmy.daysUntilArival == 0 && !secondArmy.dead)
						{
							army.infantry += secondArmy.infantry;
							army.tanks += secondArmy.tanks;

							secondArmy.dead = true;
						}
					}
				}	
			}

			for (int i = armies.Count - 1; i >= 0; i--)
			{
				if (armies[i].dead)
				{
					armies.RemoveAt(i);
				}
			}

			return armies;
		}
	}
}
