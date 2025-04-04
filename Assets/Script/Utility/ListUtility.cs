using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Utility
{
    public static class ListUtility
    {
        public static List<T> Shuffle<T>(this IList<T> list)
        {
            return list.OrderBy(x => UnityEngine.Random.value).ToList();
        }
    }
}