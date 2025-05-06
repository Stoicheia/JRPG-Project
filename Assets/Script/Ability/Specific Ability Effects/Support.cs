using System;
using System.Linq;
using Script.Entity;
using Script.Game;
using UnityEngine;

namespace Cards.Specific_Ability_Effects
{
    [Serializable]
    public class Support : AbilityEffect
    {
        public int Amount;
        
        public override void Execute(Combatant dealer, CombatManager combat)
        {
            bool x = true;
            var options = combat.CombatantsInOrder.Where(x => x != dealer).ToList();
            while (x)
            {
                Combatant other = options[UnityEngine.Random.Range(0, options.Count)];
                if (other != dealer)
                {
                    other.BaseDamage += Amount;
                    x = false;
                }
            }

        }

        public override string GetDescription(Combatant dealer)
        {
            return $"Increase a random ally's damage by {Amount}.";
        }
    }
}