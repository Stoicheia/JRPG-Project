using UnityEngine;

namespace Cards
{
    public abstract class AbilityEffect : ScriptableObject
    {
        public abstract void Execute();
        public abstract string GetDescription();
    }
}