namespace TheGreatPatrioticWar
{
    class Finland : Faction
    {
        public Finland()
        {
            Alliance = ALLIANCE.AXIS;
        }

        public override float InfantryDamage => 0.1f;

        public override float TankDamage => throw new System.NotImplementedException();
    }
}
