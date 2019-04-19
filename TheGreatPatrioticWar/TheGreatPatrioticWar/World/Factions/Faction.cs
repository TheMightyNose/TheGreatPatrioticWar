namespace TheGreatPatrioticWar
{
    public abstract class Faction
    {
        public string Name { get => GetType().Name; }
        public ALLIANCE Alliance { get; protected set; }

        public enum ALLIANCE { NEUTRAL ,AXIS, USSR  };

        public static Faction USSR = new USSR();
        public static Faction Germany = new Germany();
        public static Faction Finland = new Finland();
    }
}
