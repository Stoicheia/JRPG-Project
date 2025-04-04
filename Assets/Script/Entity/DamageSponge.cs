using System;
using UnityEngine;

namespace Script.Entity
{
    public class DamageSponge : MonoBehaviour
    {
        public event Action<int> OnTakeDamage;
        public event Action OnDie;
        
        [field: SerializeField] public int Health { get; set; }
        [field: SerializeField] public int MaxHealth { get; set; }
        [field: SerializeField] public int Damage { get; set; }

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
    }
}