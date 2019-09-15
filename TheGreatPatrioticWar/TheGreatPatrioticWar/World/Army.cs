using System.Collections.Generic;

namespace TheGreatPatrioticWar
{
	public class Army
	{
        const int tankWeight = 10;

		public Faction faction;
        private float _infantry;
        private float _tanks;

		public float Infantry { get =>_infantry; set => _infantry = value < 0 ? 0 : value; }
		public float Tanks { get => _tanks; set => _tanks = value < 0 ? 0 : value; }

		public int daysUntilArrival = 0;
		//destination

		public bool dead = false;

        public float InfantryWeight { get => Infantry; }
        public float TanksWeight { get => Tanks * tankWeight; }
        public float Weight { get => InfantryWeight + TanksWeight; }

		public Army(Faction faction, int infantry, int tanks)
		{
			this.faction = faction;
			Infantry = infantry;
			Tanks = tanks;
		}

		public static List<Army> MergeArmies(List<Army> armies)
		{
			foreach(Army army in armies)
			{
                if (army.Infantry == 0 && army.Tanks == 0)
                {
                    army.dead = true;
                }
				if (army.daysUntilArrival == 0 && !army.dead)
				{
					foreach (Army secondArmy in armies)
					{
						if (army != secondArmy && army.faction == secondArmy.faction && secondArmy.daysUntilArrival == 0 && !secondArmy.dead)
						{
							army.Infantry += secondArmy.Infantry;
							army.Tanks += secondArmy.Tanks;

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
