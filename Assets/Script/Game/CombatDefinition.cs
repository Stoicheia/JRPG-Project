using System.Collections.Generic;
using Script.Card;
using Script.Entity;
using UnityEngine;

namespace Script.Game
{
    [CreateAssetMenu(fileName = "Combat", menuName = "Combat/Definition", order = 0)]
    public class CombatDefinition : ScriptableObject
    {
        public CombatTeam Team;
        public List<CardData> Deck;
    }
}