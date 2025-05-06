using System;
using Script.Entity;
using Script.Game;
using UnityEngine;

namespace Cards.Specific_Ability_Effects
{
    [Serializable]
    public class Heal : AbilityEffect
    {
        public int Amount;
        
        public override void Execute(Combatant dealer, CombatManager combat)
        {
            dealer.Health += Amount;
            dealer.Health = Math.Clamp(dealer.Health, 0, dealer.MaxHealth);
        }

        public override string GetDescription(Combatant dealer)
        {
            return $"Heal {Amount}.";
        }
    }
}