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

		public Faction owner;
        public Terrain terrain = new Terrain();

        public string tagName = string.Empty;

		public float civilians;
		public int droppedBombs = 0;
        public int craters = 0;

        public bool battleInProgress = false;

        public Field(int x, int y, Faction owner)
		{
            this.owner = owner;
			this.x = x;
			this.y = y;
		}

		public void Draw()
		{
            if (!terrain.isWater)
            {
                bob.FillColor = new Color((byte)(armies.Where(x => x.faction == Faction.USSR).Count() * 255), (byte)(armies.Where(x => x.faction == Faction.Germany).Count() * 255), (byte)(armies.Where(x => x.faction == Faction.Finland).Count() * 255), (byte)(civilians + 75));
            }
            else
            {
                bob.FillColor = Color.Blue;
            }
            bob.Position = new Vector2f(x, y);
			bob.Size = new Vector2f(Grid.cellSize, Grid.cellSize);

            Camera.DrawOnCamera(bob);
			
		}

        public bool Combat()
        {
            float defenderWeight = 0;
            float attackerWeight = 0;
            List<Army> defenderArmies = new List<Army>();
            List<Army> attackerArmies = new List<Army>();
            
            foreach(Army army in armies.Where(x => x.daysUntilArrival == 0))
            {
                if (army.faction.Alliance == owner.Alliance)
                {
                    defenderArmies.Add(army);
                    defenderWeight += army.Weight;
                }
                else
                {
                    attackerArmies.Add(army);
                    attackerWeight += army.Weight;
                }
            }

            if (attackerWeight == 0) return false; //peace in our time

            if (defenderWeight == 0)
            {
                owner = attackerArmies[0].faction; return false; //anschluss
            }

            foreach(Army attacker in attackerArmies)
            {
                foreach(Army defender in defenderArmies)
                {
                    float attackerInfantryDefense = 1.0f;
                    float defenderInfantryDefense = 1.0f;
                    float attackerTankDefense = defender.faction.TankDefense;
                    float defenderTankDefense = defender.faction.TankDefense;

                    float attackerAttack = attacker.Infantry * attacker.faction.InfantryDamage + attacker.Tanks * attacker.faction.TankDamage;
                    float defenderAttack = defender.Infantry * defender.faction.InfantryDamage + attacker.Tanks * attacker.faction.TankDamage;

                    defender.Infantry -= attackerAttack * defender.InfantryWeight/defenderWeight/defenderInfantryDefense; 
                    attacker.Infantry -= defenderAttack * attacker.InfantryWeight/attackerWeight/attackerInfantryDefense;

                    defender.Tanks -= attackerAttack * defender.TanksWeight / defenderWeight/defenderTankDefense;
                    attacker.Tanks -= defenderAttack * attacker.TanksWeight / attackerWeight/attackerTankDefense;
                }
            }

            return true;
        }

        public override string ToString()
        {
            var nl = Environment.NewLine;
            var armyInfo = string.Concat(armies.Select(army =>
            {
                return $"Army: {nl}" + string.Concat(typeof(Army).GetFields().Select(field =>
                {
                    return $" - {field.Name} = {field.GetValue(army)}{nl}";
                }))
                + string.Concat
                (typeof(Army).GetProperties().Select(field =>
                {
                    return $" - {field.Name} = {field.GetValue(army)}{nl}";
                }));
            }));

            return $"SwimmingPeople : {terrain.swimmingPeople}{nl}{tagName}{nl}Civilians: {civilians}{nl}Armies: {nl}" + armyInfo;
        }

        public void Daily()
        {
            armies.Where(x => x.daysUntilArrival > 0).ToList().ForEach(x => x.daysUntilArrival--);

            battleInProgress = Combat();

            Army.MergeArmies(armies);
        }
    }
}
