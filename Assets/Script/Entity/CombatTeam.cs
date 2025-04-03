using UnityEngine;

namespace Script.Entity
{
    public class CombatTeam : MonoBehaviour
    {
        [field: SerializeField] public int Block { get; set; }
        [field: SerializeField] public int Energy { get; set; }
        [field: SerializeField] public int MaxEnergy { get; set; }

        public void Init()
        {
            Block = 0;
            Energy = MaxEnergy;
        }
    }
}