using System;
using System.Runtime.CompilerServices;

namespace GraphicEngineV2
{
    public class RoundedFloat
    {
        private static int precision = 3;

        public static void SetPrecision(int val)
        {
            precision = val;
        }

        public static float[,] RoundedMatrix(float[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    arr[i, j] = (float)Math.Round(arr[i, j], precision);
            return arr;
        }

        public static float PI()
        {
            return (float)Math.Round(Math.PI, precision + 2);
        }

        public static float RoundFloat(float val)
        {
            return (float)Math.Round(val, precision);
        }

        public static float[] RoundArray(float[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = (float)Math.Round(arr[i], precision);

            return arr;
        }


    }
}
