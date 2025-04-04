using System.Collections.Generic;
using System.Text;
using Script.Entity;
using Script.Game;
using UnityEngine;

namespace Cards
{
    public class CombinedAbility
    {
        private List<AbilityEffect> _abilityEffects;

        public CombinedAbility(List<AbilityEffect> abilityEffects)
        {
            _abilityEffects = abilityEffects;
        }
        
        public void Execute(Combatant dealer, CombatManager combat)
        {
            _abilityEffects.ForEach(abilityEffect => abilityEffect.Execute(dealer, combat));
        }

        public string GetDescription(Combatant dealer)
        {
            StringBuilder fullString = new StringBuilder();
            _abilityEffects.ForEach(abilityEffect => fullString.AppendLine(abilityEffect.GetDescription(dealer)));
            if (fullString.Length > 0)
            {
                return fullString.ToString().TrimEnd();
            }
            else
            {
                return "No effect";
            }
        }
    }
}