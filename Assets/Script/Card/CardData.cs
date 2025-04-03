using Cards;
using Script.Entity.Clothing;
using UnityEngine;

namespace Script.Card
{
    [CreateAssetMenu(fileName = "Card", menuName = "Cards/Card", order = 0)]
    public class CardData : ScriptableObject
    {
        [field: SerializeField] public AbilityEffect Effect { get; private set; }
        [field: SerializeField] public string CardName { get; private set; }
        public string Description => Effect.GetDescription();
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public ClothingType Type { get; private set; }
    }
}