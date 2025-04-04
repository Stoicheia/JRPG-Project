using System;
using Script.Entity;
using Script.Game;
using UnityEngine;

namespace Cards.Specific_Ability_Effects
{
    [Serializable]
    public class DamageBuff : AbilityEffect
    {
        public int Amount;
        
        public override void Execute(Combatant dealer, CombatManager combat)
        {
            dealer.BaseDamage += Amount;
        }

        public override string GetDescription(Combatant dealer)
        {
            return $"Increase attack damage by {Amount}.";
        }
    }
}