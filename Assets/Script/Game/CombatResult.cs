using System;

namespace Script.Game
{
    public struct CombatResult
    {
        public bool Win;
        
        public static CombatResult Victory() => new CombatResult() { Win = true };
        public static CombatResult Defeat() => new CombatResult() { Win = false };
    }

    [Serializable]
    public enum CombatResultType
    {
        Ongoing, Victory, Defeat
    }
}