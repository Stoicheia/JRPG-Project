using System.Collections.Generic;
using Script.Entity;
using Script.Entity.Clothing;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Script.Card
{
    public class CombatantRenderer : SerializedMonoBehaviour
    {
        private Combatant _loadedCombatant;
        [Header("Fields")]
        [SerializeField] private TextMeshPro _nameField;
        [SerializeField] private Dictionary<ClothingType, SpriteRenderer> _clothingTypeToRenderer;
        [SerializeField] private Dictionary<ClothingType, Sprite> _defaultClothes;
        [SerializeField] private TextMeshPro _healthField;
        
        public void Load(Combatant combatant)
        {
            _loadedCombatant = combatant;
            var wearer = combatant.ClothesWearer;
            _nameField.text = combatant.name;
            foreach (var slot in wearer.Slots)
            {
                if (slot.Card == null)
                {
                    _clothingTypeToRenderer[slot.Type].sprite = _defaultClothes[slot.Type];
                }
                else
                {
                    _clothingTypeToRenderer[slot.Type].sprite = slot.Card.Data.Sprite;
                }
            }

            _healthField.text = $"{combatant.Health}/{combatant.MaxHealth}";
        }
    }
}