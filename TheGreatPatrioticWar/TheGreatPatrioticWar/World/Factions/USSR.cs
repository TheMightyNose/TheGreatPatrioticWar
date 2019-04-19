namespace TheGreatPatrioticWar
{
    class USSR : Faction
    {
        public USSR ()
        {
            Alliance = ALLIANCE.USSR;
        }


        public override float InfantryDamage => 0.1f;

        public override float TankDamage => throw new System.NotImplementedException();
    }
}
