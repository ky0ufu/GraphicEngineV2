using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class PointException
    {
        public static EngineException InitError()
        {
            throw new EngineException("Point initialization exception");
        }
        public static EngineException BadSizes()
        {
            throw new EngineException("Not equal size of Point and Vector");
        }
    }
}
