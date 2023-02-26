using System;

namespace Extensions
{
    public static class Array
    {
        public static T Random<T>(this T[] array)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException("Array length is equal to 0.");
            }

            return array[UnityEngine.Random.Range(0, array.Length)];
        }
    }
}
