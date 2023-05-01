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
            IEnumerable<long> GaloisLFSR(int seed)
            {
                long lfsr = seed;
                do yield return (lfsr = (lfsr >> 1) ^ (-(lfsr & 1u) & 0xD0000001u));
                while (lfsr != seed);
            }
            float[,] v = new float[3, 3] { { 1.51515f, 3.5f, 1.5f, }, { 1.5f, 3.5f, 1.5f, }, { 1.5f, 3.5f, 1.5f, } };
            Matrix A = new Matrix(v);
            Vector B = new Vector(3);
            Vector C = new Vector(3);
            (B + C).Print();
        }
        
   
    }
}