using System;
using Script.Entity;
using Script.Game;
using UnityEngine;

namespace Cards.Specific_Ability_Effects
{
    [Serializable]
    public class DealDamage : AbilityEffect
    {
        public float Multiplier;
        
        public override void Execute(Combatant dealer, CombatManager combat)
        {
            combat.Enemy.TakeDamage((int)(dealer.BaseDamage * Multiplier));
        }

        public override string GetDescription(Combatant dealer)
        {
            return $"Deal {(int)(dealer.BaseDamage * Multiplier)} damage.";
        }
    }
}