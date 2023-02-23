namespace CBA.Extensions
{
    public static class Mathf
    {
        public static int ClampInt(int value, int min, int max)
        {
            if (value > max)
            {
                return max;
            }

            return value < min ? min : value;
        }

        public static int ClampInt01(int value)
        {
            return ClampInt(value, 0, 1);
        }

        public static int ClampInt1BothSign(int value)
        {
            return ClampInt(value, -1, 1);
        }

        public static int InverseRaw01(int value)
        {
            return Inverse01(ClampInt01(value));
        }

        public static int Inverse01(int value)
        {
            return value == 0 ? 1 : 0;
        }
    }
}
