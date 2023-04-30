using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    [Serializable]
    public class EngineException : Exception
    {
        public EngineException() { }
        public EngineException(string message) : base(message) { }
        public EngineException(string message, Exception innerException) : base(message, innerException) { }
    }
}
