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

		public float civilians;
		public int droppedBombs = 0;
        public int craters = 0;


        //enum terrain

        public Field(int x, int y, Faction owner = null)
		{
            this.owner = owner;
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

        public void Combat()
        {
            float defenderWeight = 0;
            float attackerWeight = 0;
            List<Army> defenderArmies = new List<Army>();
            List<Army> attackerArmies = new List<Army>();

            foreach(Army army in armies)
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

            if (attackerWeight == 0) return; //peace in our time

            if (defenderWeight == 0)
            {
                owner = attackerArmies[0].faction; return; //anschluss
            }

            foreach(Army attacker in attackerArmies)
            {
                foreach(Army defender in defenderArmies)
                {
                    float attackerInfantryDefense = 1.0f;
                    float defenderInfantryDefense = 1.0f;
                    float attackerTankDefense = 10.0f;
                    float defenderTankDefense = 10.0f;

                    float attackerAttack = attacker.Infantry * attacker.faction.InfantryDamage + attacker.Tanks * attacker.faction.TankDamage;
                    float defenderAttack = defender.Infantry * defender.faction.InfantryDamage + attacker.Tanks * attacker.faction.TankDamage;

                    defender.Infantry -= attackerAttack * defender.InfantryWeight/defenderWeight/defenderInfantryDefense;
                    attacker.Infantry -= defenderAttack * attacker.InfantryWeight/attackerWeight/attackerInfantryDefense;

                    defender.Tanks -= attackerAttack * defender.TanksWeight / defenderWeight/defenderTankDefense;
                    attacker.Tanks -= defenderAttack * attacker.TanksWeight / attackerWeight/attackerTankDefense;
                }
            }
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

            return $"Civilians: {civilians}{nl}Armies: {nl}" + armyInfo;
        }
    }
}
