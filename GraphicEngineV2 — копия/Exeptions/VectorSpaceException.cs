using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class VectorSpaceException
    {
        public static EngineException BadBasisSize()
        {
            throw new EngineException("Bad basis size");
        }
        public static EngineException InitError()
        {
            throw new EngineException("Initialization exception");
        }
        public static EngineException NotBasis()
        {
            throw new EngineException("THIS IS NOT BASIS");
        }
    }
}
