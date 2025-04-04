using System;
using System.Linq;
using Cards;
using Cysharp.Threading.Tasks;
using Script.Card;
using UnityEngine;

namespace Script.Game
{
    public class CombatSequencer : MonoBehaviour
    {
        [SerializeField] private CombatManager _combatManager;
        [SerializeField] private UIController _ui;

        private async void Start()
        {
            CombatResultType combatResult = await PlayCombat();
        }

        public async UniTask<CombatResultType> PlayCombat()
        {
            // Initialization
            _combatManager.LoadCombat();
            _combatManager.Deck.ShuffleDrawPile();
            
            // Start round
            _combatManager.PlayerTeam.Block = 0;
            
            // Start phase
            while (true)
            {
                await PlayerPhase();
                CombatResultType endCheck = CheckEndConditions();
                if (endCheck != CombatResultType.Ongoing)
                {
                    return endCheck;
                }
                if (_combatManager.PlayerTeam.Energy <= 0)
                {
                    await EnemyPhase();
                    CombatResultType endCheck2 = CheckEndConditions();
                    if (endCheck2 != CombatResultType.Ongoing)
                    {
                        return endCheck2;
                    }
                }
                else
                {
                    _combatManager.NextCombatant();
                }
            }
        }

        private async UniTask PlayerPhase()
        {
            _combatManager.NextCombatant();
            _combatManager.Deck.DrawToHandSize();
            UniTaskCompletionSource<CardInstance> waitForSelectCard = new UniTaskCompletionSource<CardInstance>();
            _combatManager.ToggleCardSelections(true, waitForSelectCard);
            CardInstance selectedCard = await waitForSelectCard.Task;
            _combatManager.ActiveCombatant.Equip(selectedCard);
            _combatManager.Deck.Discard(selectedCard);
            _combatManager.ToggleCardSelections(false, null);
            UniTaskCompletionSource<CombinedAbility> waitForExecute = new UniTaskCompletionSource<CombinedAbility>();
            _combatManager.GotoExecutionPhase(waitForExecute);
            CombinedAbility combatAbility = await waitForExecute.Task;
            //_combatManager.Execute(_combatManager.ActiveCombatant, combatAbility);
        }

        private async UniTask EnemyPhase()
        {
            int damage = _combatManager.Enemy.Damage;
            _ui.StartEnemyAttack(damage);
            await UniTask.WaitForSeconds(0.3f);
            _combatManager.ActiveCombatant.TakeDamage(damage);
            _combatManager.PlayerTeam.Energy = _combatManager.PlayerTeam.MaxEnergy;
        }

        private CombatResultType CheckEndConditions()
        {
            if (_combatManager.PlayerCombatants.Any(x => x.Health == 0))
            {
                return CombatResultType.Defeat;
            }

            if (_combatManager.Enemy.Health <= 0)
            {
                return CombatResultType.Victory;
            }

            return CombatResultType.Ongoing;
        }
    }
}