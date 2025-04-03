using System;
using System.Collections.Generic;
using System.Linq;
using Script.Entity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.Game
{
    public class CombatManager : SingletonMonoBehaviour<CombatManager>
    {
        private List<Combatant> _friendlies;
        private CombatTeam _playerTeam;
        private DamageSponge _sponge;
            
        [SerializeField] private List<Transform> _friendlySlots;

        [SerializeField] private CombatDefinition _loadOnStart;

        private void Start()
        {
            LoadCombat(_loadOnStart);
        }

        [Button]
        public void LoadCombat(CombatDefinition def)
        {
            _friendlies = LoadCombatantList(def.PlayerCombatants, _friendlySlots);
            _playerTeam = Instantiate(def.Team);
            _playerTeam.Init();
        }

        private List<Combatant> LoadCombatantList(List<Combatant> prefabs, List<Transform> slots)
        {
            List<Combatant> instances = new List<Combatant>();
            for (int i = 0; i < prefabs.Count; i++)
            {
                Combatant prefab = prefabs[i];
                Transform spot = slots[i];
                Combatant instance = Instantiate(prefab, spot);
                instance.Init();
                instances.Add(instance);
            }
            return instances;
        }
    }
}