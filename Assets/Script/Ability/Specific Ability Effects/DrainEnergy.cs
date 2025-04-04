using System;
using Script.Entity;
using Script.Game;
using UnityEngine;

namespace Cards.Specific_Ability_Effects
{
    [Serializable]
    public class DrainEnergy : AbilityEffect
    {
        public int Amount;
        
        public override void Execute(Combatant dealer, CombatManager combat)
        {
            combat.PlayerTeam.Energy -= Amount;
        }

        public override string GetDescription(Combatant dealer)
        {
            return $"Lose {Amount} energy.";
        }
    }
}