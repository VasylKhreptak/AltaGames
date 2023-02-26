using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class List
    {
        public static T Last<T>(this List<T> array)
        {
            return array[array.Count - 1];
        }

        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);

                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
