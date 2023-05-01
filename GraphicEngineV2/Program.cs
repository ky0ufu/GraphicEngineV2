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

            EntitiesList e1 = new EntitiesList();
            Entity enot = new Entity();
            e1.AppendEntity(enot);
            Console.WriteLine(e1[enot.Id].Id);
        }
        
   
    }
}