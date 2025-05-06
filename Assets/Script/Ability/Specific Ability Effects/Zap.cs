using System;
using Script.Entity;
using Script.Game;
using UnityEngine;

namespace Cards.Specific_Ability_Effects
{
    [Serializable]
    public class Zap : AbilityEffect
    {
        public int Amount;
        
        public override void Execute(Combatant dealer, CombatManager combat)
        {
            combat.Deck.DiscardRandom();
        }

        public override string GetDescription(Combatant dealer)
        {
            return $"Zap 1";
        }
    }
}