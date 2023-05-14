using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class MatrixException
    {
        public static EngineException NotSquareException()
        {
            throw new EngineException("Matrix not square size or Determinant equal 0");
        }
        public static EngineException InvalidSizes()
        {
            throw new EngineException("Invalid sizes of matrixes");
        }
        public static EngineException DiffirentSizes()
        {
            throw new EngineException("Diffirent matrix sizes");
        }
        public static EngineException InitException()
        {
            throw new EngineException("Initialization exception");
        }
    }
}
