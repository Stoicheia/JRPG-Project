using System;
using Script.Card;
using Script.Entity.Clothing;
using UnityEngine;

namespace Script.Entity
{
    public class Combatant : MonoBehaviour
    {
        public event Action<CardData> OnEquip;
        
        [field: Header("Dependencies")]
        [field: SerializeField] public CombatTeam Team { get; set; }
        [field: SerializeField] public ClothesWearer ClothesWearer { get; set; }
        
        [field: Header("Stats")]
        [field: SerializeField] public int Health { get; set; }
        [field: SerializeField] public int MaxHealth { get; set; }
        [field: SerializeField] public int BaseDamage { get; set; }

        public void Init()
        {
            Health = MaxHealth;
            ClothesWearer.Init();
        }

        public void Equip(CardData card)
        {
            ClothesWearer.Equip(card);
            OnEquip?.Invoke(card);
        }
    }
}