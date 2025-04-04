using System;
using Script.Entity;
using Script.Game;
using UnityEngine;

namespace Cards
{
    [Serializable]
    public abstract class AbilityEffect
    {
        public abstract void Execute(Combatant dealer, CombatManager combat);
        public abstract string GetDescription(Combatant dealer);
    }
}