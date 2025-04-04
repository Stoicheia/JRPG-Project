using System.Collections.Generic;
using System.Text;
using Cards;
using Script.Entity;
using Script.Entity.Clothing;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Script.Card
{
    [CreateAssetMenu(fileName = "Card", menuName = "Cards/Card", order = 0)]
    public class CardData : SerializedScriptableObject
    {
        [field: OdinSerialize] public List<AbilityEffect> Effects { get; private set; }
        [field: SerializeField] public string CardName { get; private set; }

        public string GetDescription(Combatant dealer)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var effect in Effects)
            {
                sb.AppendLine(effect.GetDescription(dealer));
            }
            return sb.ToString();
        }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public ClothingType Type { get; private set; }
    }
}