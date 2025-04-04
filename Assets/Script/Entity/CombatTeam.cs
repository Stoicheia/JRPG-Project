﻿using System.Collections.Generic;
using UnityEngine;

namespace Script.Entity
{
    public class CombatTeam : MonoBehaviour
    {
        [field: SerializeField] public int Block { get; set; }
        [field: SerializeField] public int Energy { get; set; }
        [field: SerializeField] public int MaxEnergy { get; set; }
        [field: SerializeField] public List<Combatant> Members { get; set; }

        public void Init(List<Combatant> members)
        {
            Block = 0;
            Energy = MaxEnergy;
            Members = members;
        }
    }
}