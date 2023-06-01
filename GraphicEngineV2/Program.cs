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
            Entity e1 = new Entity();
            e1.SetProperty("cringe", 5);
            Console.WriteLine(e1["cringe"]);
            e1.SetProperty("cringe", "LMAO");
            Console.WriteLine(e1["cringe"]);

        }   
    }
    
}
