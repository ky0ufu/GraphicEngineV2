using System;
using System.Runtime.CompilerServices;

namespace GraphicEngineV2
{
    public class RoundedFloat
    {
        private static int n = 3;

        public static void SetPrecision(int val)
        {
            n = val;
        }

        public static float[,] RoundedMatrix(float[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    arr[i, j] = (float)Math.Round(arr[i, j], n);
            return arr;
        }

        public static float PI()
        {
            return (float)Math.Round(Math.PI, n + 2);
        }

        public static float RoundFloat(float val)
        {
            return (float)Math.Round(val, n);
        }

        public static float[] RoundArray(float[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = (float)Math.Round(arr[i], n);

            return arr;
        }


    }
}
