using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class Program
    {
        static void Main(string[] args)
        {
            float[,] arr =  { {1, 0, 0 },
                              {0, 1, 0 },
                              {0, 0, 1 } };
            float[,] arr1 = { {1, 2, 3 },
                              {4, 5, 6 }
                               };
            Matrix A = new Matrix(arr);
            Matrix B = new Matrix(arr1);
            (A * B).Print();
            
        }

        
    }
}
