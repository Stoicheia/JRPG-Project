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
        [field: SerializeField] public int DamageIncreasePerTurn { get; set; }

        [Header("Floating Text")]
        [SerializeField] private GameObject damageTextPrefab;
        [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 1f, 0f);
        [SerializeField] private float randomOffsetRange = 0.5f;
        [SerializeField] private Canvas _damageTextRoot;

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Debug.Log($"[DamageSponge] Attached to: {GetHierarchyPath(transform)}", gameObject);
            if (damageTextPrefab != null)
            {
                Vector3 randomOffset = new Vector3(
                    UnityEngine.Random.Range(-randomOffsetRange, randomOffsetRange),
                    UnityEngine.Random.Range(-randomOffsetRange, randomOffsetRange),
                    0f
                );

                GameObject instance = Instantiate(
                    damageTextPrefab,
                    transform.position + spawnOffset + randomOffset,
                    Quaternion.identity,
                    _damageTextRoot.transform
                );

                FloatingText floatingText = instance.GetComponent<FloatingText>();
                floatingText?.SetText($"-{damage}");
            }

            OnTakeDamage?.Invoke(Health);
            if (Health <= 0)
            {
                Health = 0;
                OnDie?.Invoke();
            }
        }
        private string GetHierarchyPath(Transform current)
        {
            string path = current.name;
            while (current.parent != null)
            {
                current = current.parent;
                path = current.name + "/" + path;
            }
            return path;
        }
    }
}
