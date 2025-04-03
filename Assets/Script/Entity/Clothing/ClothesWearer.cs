using System;
using System.Collections.Generic;
using System.Linq;
using Script.Card;
using UnityEngine;

namespace Script.Entity.Clothing
{
    public class ClothesWearer : MonoBehaviour
    {
        [SerializeField] private List<ClothesSlot> _slots;
        private Dictionary<ClothingType, ClothesSlot> _typeToSlot;

        public void Init()
        {
            _slots = new List<ClothesSlot>()
            {
                ClothesSlot.Empty(ClothingType.Head),
                ClothesSlot.Empty(ClothingType.Body),
                ClothesSlot.Empty(ClothingType.Feet),
            };
            ValidateDictionary();
        }

        public void Equip(CardData card)
        {
            ClothingType slot = card.Type;
            _typeToSlot[slot].Card = card;
            ValidateDictionary();
        }

        private void ValidateDictionary()
        {
            _typeToSlot = _slots.ToDictionary(x=> x.Type, x=> x);
        }
    }

    [Serializable]
    public class ClothesSlot
    {
        public ClothingType Type;
        public CardData Card;

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