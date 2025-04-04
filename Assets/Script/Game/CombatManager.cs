using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using Cysharp.Threading.Tasks;
using Script.Card;
using Script.Entity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Game
{
    public class CombatManager : SingletonMonoBehaviour<CombatManager>
    {
        public event Action OnChange;
        public event Action OnNextCombatant;
        public event Action<bool, UniTaskCompletionSource<CardInstance>> OnToggleCardSelections;
        public event Action<UniTaskCompletionSource<CombinedAbility>> OnExecutionPhase;

        public List<Combatant> CombatantsInOrder => new List<Combatant>()
        {
            PlayerCombatants[ActiveCombatantIndex % PlayerCombatants.Count],
            PlayerCombatants[(ActiveCombatantIndex+1) % PlayerCombatants.Count],
            PlayerCombatants[(ActiveCombatantIndex+2) % PlayerCombatants.Count]
        };
        [field: SerializeField][field: ReadOnly]  public List<Combatant> PlayerCombatants { get; set; }
        public Combatant ActiveCombatant => PlayerCombatants[ActiveCombatantIndex % PlayerCombatants.Count];
        [field: SerializeField][field: ReadOnly]  public int ActiveCombatantIndex { get; set; }
        [field: SerializeField][field: ReadOnly] public CombatTeam PlayerTeam { get; set; }
        [field: SerializeField] public DamageSponge Enemy { get; set; }
        [field: SerializeField] public CardDeck Deck { get; set; }
            
        [SerializeField] private List<Transform> _friendlySlots;

        [SerializeField] private CombatDefinition _loadOnStart;

        [Button]
        public void LoadCombat()
        {
            LoadCombat(_loadOnStart);
        }

        public void LoadCombat(CombatDefinition def)
        {
            PlayerCombatants = LoadCombatants(def.Team.Members, _friendlySlots);
            PlayerTeam = Instantiate(def.Team);
            PlayerTeam.Init(PlayerCombatants);
            Deck.Init(def.Deck, 4);
            ActiveCombatantIndex = 0;
            OnChange?.Invoke();
        }

        public void NextCombatant()
        {
            ActiveCombatantIndex++;
            OnNextCombatant?.Invoke();
            OnChange?.Invoke();
        }

        public void Execute(Combatant dealer, CombinedAbility ability)
        {
            ability.Execute(dealer, this);
            OnChange?.Invoke();
        }

        public void ToggleCardSelections(bool on, UniTaskCompletionSource<CardInstance> utcs)
        {
            OnToggleCardSelections?.Invoke(on, utcs);
            OnChange?.Invoke();
        }

        public void GotoExecutionPhase(UniTaskCompletionSource<CombinedAbility> waitForExecute)
        {
            OnExecutionPhase?.Invoke(waitForExecute);
            OnChange?.Invoke();
        }

        private List<Combatant> LoadCombatants(List<Combatant> prefabs, List<Transform> slots)
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