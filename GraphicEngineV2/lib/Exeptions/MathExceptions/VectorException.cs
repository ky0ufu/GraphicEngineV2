using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class VectorException
    {
        public static EngineException InitException()
        {
            throw new EngineException("Vector Initialization Exception");
        }
        public static EngineException DiffirentSizes()
        {
            throw new EngineException("Diffirent Vector Sizes");
        }
        public static EngineException FormException()
        {
            throw new EngineException("One of vector bad form");
        }
        public static EngineException BadDimmensional()
        {
            throw new EngineException("Bad dimmensional size");
        }
    }
}
