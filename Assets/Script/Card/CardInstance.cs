using System;
using UnityEngine;

namespace Script.Card
{
    [Serializable]
    public class CardInstance
    {
        public CardData Data;
        
        public CardInstance(CardData data)
        {
            Data = data;
        }
    }
}