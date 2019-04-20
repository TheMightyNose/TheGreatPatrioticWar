namespace TheGreatPatrioticWar
{
    class Germany : Faction
    {
        public Germany()
        {
            Alliance = ALLIANCE.AXIS;
        }

        public override float InfantryDamage => 0.1f;

        public override float TankDamage => 1.0f;

        public override float TankDefense => 20.0f;
    }
}
