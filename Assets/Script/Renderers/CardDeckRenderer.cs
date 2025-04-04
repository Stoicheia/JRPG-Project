using System;
using System.Collections.Generic;
using Script.Game;
using TMPro;
using UnityEngine;

namespace Script.Card
{
    public class CardDeckRenderer : MonoBehaviour
    {
        public event Action<CardInstance> OnClickCard;
        
        [SerializeField] private TextMeshProUGUI _drawSizeField;
        [SerializeField] private TextMeshProUGUI _discardSizeField;
        [SerializeField] private List<CardRenderer> _handRenderers;

        private bool _canSelect;

        private void OnEnable()
        {
            foreach (var cRenderer in _handRenderers)
            {
                cRenderer.OnClick += HandleClickCard;
            }
        }

        private void OnDisable()
        {
            foreach (var cRenderer in _handRenderers)
            {
                cRenderer.OnClick -= HandleClickCard;
            }
        }

        public void RenderDeck(CardDeck deck)
        {
            for (int i = 0; i < deck.Hand.Count; i++)
            {
                CardRenderer handRenderer = _handRenderers[i];
                CardInstance card = deck.Hand[i];
                handRenderer.Load(card);
            }

            for (int i = deck.Hand.Count; i < _handRenderers.Count; i++)
            {
                CardRenderer handRenderer = _handRenderers[i];
                handRenderer.LoadEmpty();
            }

            _drawSizeField.text = deck.DrawPile.Count.ToString();
            _discardSizeField.text = deck.DiscardPile.Count.ToString();
        }

        public void ToggleCanSelect(bool on)
        {
            _canSelect = on;
        }
        
        private void HandleClickCard(CardInstance card)
        {
            if (_canSelect)
            {
                OnClickCard?.Invoke(card);
            }
        }
    }
}