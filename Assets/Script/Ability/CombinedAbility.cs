using System.Collections.Generic;
using System.Text;
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
        
        public void Execute()
        {
            _abilityEffects.ForEach(abilityEffect => abilityEffect.Execute());
        }

        public string GetDescription()
        {
            StringBuilder fullString = new StringBuilder();
            _abilityEffects.ForEach(abilityEffect => fullString.Append(abilityEffect.GetDescription()));
            return fullString.ToString().TrimEnd();
        }
    }
}