using Script.Entity;
using TMPro;
using UnityEngine;

namespace Script.Card
{
    public class ActiveInfoRenderer : MonoBehaviour
    {
        private Combatant _loadedCombatant;
        [Header("Fields")] 
        [SerializeField] private TextMeshProUGUI _clothesField;
        [SerializeField] private TextMeshProUGUI _abilitiesField;
        
        public void Load(Combatant activeCombatant)
        {
            _loadedCombatant = activeCombatant;
            _clothesField.text = activeCombatant.GetClothingDescription();
            _abilitiesField.text = activeCombatant.GetAbilityDescription();
        }
    }
}