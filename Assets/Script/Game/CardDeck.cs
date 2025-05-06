using System.Collections.Generic;
using System.Linq;
using Script.Card;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Game
{
    public class CardDeck : MonoBehaviour
    {
        public List<CardInstance> AllCards => _allOwnedCards;
        public List<CardInstance> DrawPile => _drawPile;
        public List<CardInstance> Hand => _hand;
        public List<CardInstance> DiscardPile => _discardPile;
        
        [SerializeField] private int _defaultHandSize;
        private List<CardInstance> _allOwnedCards;
        private List<CardInstance> _drawPile;
        private List<CardInstance> _hand;
        private List<CardInstance> _discardPile;

        public void Init(List<CardData> cards, int handSize)
        {
            _allOwnedCards = cards.Select(x => new CardInstance(x)).ToList();
            _drawPile = _allOwnedCards.ToList();
            _defaultHandSize = handSize;
            _hand = new List<CardInstance>();
            _discardPile = new List<CardInstance>();
        }

        public List<CardInstance> DrawToHandSize()
        {
            int amount = _defaultHandSize - _hand.Count;
            amount = Mathf.Max(amount, 0);
            return DrawMany(amount);
        }

        public List<CardInstance> DrawMany(int amount)
        {
            List<CardInstance> drawnCards = new List<CardInstance>();
            for (int i = 0; i < amount; i++)
            {
                drawnCards.Add(TryDrawOne());
            }
            return drawnCards;
        }

        public void Discard(CardInstance card)
        {
            if (!_hand.Contains(card))
            {
                Debug.LogError($"Cannot discard card that isn't in hand: {card.Data.CardName}.");
            }
            _discardPile.Add(card);
            _hand.Remove(card);
        }

        public void DiscardRandom()
        {
            Discard(_hand[Random.Range(0, _hand.Count)]);
            TryDrawOne();
        }

        public CardInstance TryDrawOne()
        {
            if (_drawPile.Count == 0)
            {
                ShuffleDiscardIntoDrawPile();
            }

            if (_drawPile.Count == 0)
            {
                return null;
            }
            CardInstance card = _drawPile.Last();
            _drawPile.RemoveAt(_drawPile.Count - 1);
            _hand.Add(card);
            return card;
        }

        public void ShuffleDrawPile()
        {
            _drawPile = _drawPile.Shuffle();
        }

        public void ShuffleDiscardIntoDrawPile()
        {
            _drawPile = _drawPile.Concat(_discardPile).ToList();
            ShuffleDrawPile();
        }
    }
}