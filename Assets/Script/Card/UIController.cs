using System.Collections;
using System.Collections.Generic;
using Cards;
using Cysharp.Threading.Tasks;
using Script.Entity;
using Script.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Card
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private CardSpinner cardSpinner;
        [SerializeField] private TextMeshProUGUI _enemyHealthField;
        [SerializeField] private TextMeshProUGUI _teamEnergyField;
        [SerializeField] private TextMeshProUGUI _enemyAttackWarning;
        [SerializeField] private CombatManager _combatManager;
        [SerializeField] private CardDeckRenderer _deckRenderer;
        [SerializeField] private Button _executeButton;
        [SerializeField] private List<CombatantRenderer> _combatRenderers;
        [SerializeField] private ActiveInfoRenderer _activeInfoRenderer;

        private UniTaskCompletionSource<CardInstance> _waitForSelectCard;
        private UniTaskCompletionSource<CombinedAbility> _waitForExecute;

        private void OnEnable()
        {
            _combatManager.OnToggleCardSelections += HandleToggleCardSelection;
            _combatManager.OnExecutionPhase += HandleEnterExecutionPhase;
            _combatManager.OnChange += HandleChange;
            _deckRenderer.OnClickCard += HandleClickCard;
        }

        private void OnDisable()
        {
            _combatManager.OnToggleCardSelections -= HandleToggleCardSelection;
            _deckRenderer.OnClickCard -= HandleClickCard;
        }

        private void Update()
        {
            _deckRenderer.RenderDeck(_combatManager.Deck);
        }

        private void HandleToggleCardSelection(bool on, UniTaskCompletionSource<CardInstance> selTask)
        {
            _deckRenderer.ToggleCanSelect(on);
            _waitForSelectCard = selTask;
        }

        private void HandleClickCard(CardInstance card)
        {
            _waitForSelectCard.TrySetResult(card);
        }

        private void HandleEnterExecutionPhase(UniTaskCompletionSource<CombinedAbility> waitExecute)
        {
            _executeButton.onClick.AddListener(HandleClickExecute);
            _waitForExecute = waitExecute;
        }

        public void HandleClickExecute()
        {
            StartCoroutine(ExecuteAbilityCoroutine());
        }
        public IEnumerator ExecuteAbilityCoroutine()
        {
            if (_waitForExecute != null)
            {
                cardSpinner.SetSpinning(true);
                yield return new WaitForSeconds(0.5f);


                cardSpinner.SetSpinning(false);
                yield return new WaitForSeconds(0.5f);
                CombinedAbility executedAbility = _combatManager.ActiveCombatant.Execute();
                _waitForExecute.TrySetResult(executedAbility);
                _executeButton.onClick.RemoveAllListeners();
            }
        }
        private void HandleChange()
        {
            List<Combatant> combatantsInOrder = _combatManager.CombatantsInOrder;
            for (int i = 0; i < combatantsInOrder.Count; i++)
            {
                Combatant combatant = combatantsInOrder[i];
                CombatantRenderer cRenderer = _combatRenderers[i];
                cRenderer.Load(combatant);
            }

            _activeInfoRenderer.Load(_combatManager.ActiveCombatant);
            _deckRenderer.RenderDeck(_combatManager.Deck);
            _enemyHealthField.text = $"{_combatManager.Enemy.Health}/{_combatManager.Enemy.MaxHealth}";
            _teamEnergyField.text = $"{_combatManager.PlayerTeam.Energy}/{_combatManager.PlayerTeam.MaxEnergy}";
        }

        public void StartEnemyAttack(int damage)
        {
            StartCoroutine(EnemyAttackSequence(damage));
        }

        private IEnumerator EnemyAttackSequence(int damage)
        {
            _enemyAttackWarning.gameObject.SetActive(true);
            _enemyAttackWarning.text = $"ENEMY ATTACKS YOU FOR {damage}HP";
            yield return new WaitForSeconds(1f);
            _enemyAttackWarning.gameObject.SetActive(false);
        }
    }
}
