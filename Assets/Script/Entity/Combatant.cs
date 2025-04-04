using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cards;
using Script.Card;
using Script.Entity.Clothing;
using Script.Game;
using UnityEngine;

namespace Script.Entity
{
    public class Combatant : MonoBehaviour
    {
        public event Action<int> OnTakeDamage;
        public event Action OnDie;
        public event Action<CardInstance> OnEquip;
        
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

        public void TakeDamage(int damage)
        {
            Health -= damage;
            OnTakeDamage?.Invoke(Health);
            if (Health <= 0)
            {
                Health = 0;
                OnDie?.Invoke();
            }
        }

        public void Equip(CardInstance card)
        {
            ClothesWearer.Equip(card);
            OnEquip?.Invoke(card);
        }

        public CombinedAbility Execute()
        {
            var ability = GetAbility();
            ability.Execute(this, CombatManager.Instance);
            return ability;
        }

        public CombinedAbility GetAbility()
        {
            List<AbilityEffect> effects = ClothesWearer.GetAbilityEffects();
            return new CombinedAbility(effects);
        }

        public string GetClothingDescription()
        {
            List<ClothesSlot> slots = ClothesWearer.Slots;
            StringBuilder sb = new StringBuilder();
            foreach (var slot in slots)
            {
                sb.AppendLine($"{slot.Type}: {(slot.Card == null ? "Default Crap" : slot.Card.Data.CardName)}");
            }
            return sb.ToString();
        }

        public string GetAbilityDescription()
        {
            return GetAbility().GetDescription(this);
        }
    }
}