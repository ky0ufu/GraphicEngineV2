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

            Entity e1 = new Entity();
            Console.WriteLine(e1.Id);
            e1.SetProperty("cringe", 2);
            Console.WriteLine(e1["properties"]);
        }
        
   
    }
}