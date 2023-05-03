using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace GraphicEngineV2
{
    public class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<long> UnickGenerator(int seed)
            {
                long value = seed;
                do yield return (value = (value >> 1) ^ (-(value & 1u) & 0xD0000001u));
                while (value != seed);
            }
            float[,] v = { { 1 }, { 2 }, { 3 } };
            Vector A = new Vector(v);
            Vector B = new Vector(3);
        }
        
   
    }
}