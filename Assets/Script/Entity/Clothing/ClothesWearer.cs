using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using Script.Card;
using UnityEngine;

namespace Script.Entity.Clothing
{
    public class ClothesWearer : MonoBehaviour
    {
        public List<ClothesSlot> Slots => _slots;
        
        [SerializeField] private List<ClothesSlot> _slots;
        private Dictionary<ClothingType, ClothesSlot> _typeToSlot;

        public void Init()
        {
            _slots = new List<ClothesSlot>()
            {
                ClothesSlot.Empty(ClothingType.Hands),
                ClothesSlot.Empty(ClothingType.Body),
                ClothesSlot.Empty(ClothingType.Feet),
            };
            ValidateDictionary();
        }

        public void Equip(CardInstance card)
        {
            ClothingType slot = card.Data.Type;
            _typeToSlot[slot].Card = card;
            ValidateDictionary();
        }

        private void ValidateDictionary()
        {
            _typeToSlot = _slots.ToDictionary(x=> x.Type, x=> x);
        }

        public List<AbilityEffect> GetAbilityEffects()
        {
            List<AbilityEffect> effects = new List<AbilityEffect>();
            foreach (var slot in Slots)
            {
                var card = slot.Card;
                if (card != null)
                {
                    effects.AddRange(card.Data.Effects);
                }
            }

            return effects;
        }
    }

    [Serializable]
    public class ClothesSlot
    {
        public ClothingType Type;
        public CardInstance Card;

        public static ClothesSlot Empty(ClothingType type)
        {
            return new ClothesSlot()
            {
                Type = type,
                Card = null
            };
        }
    }
}