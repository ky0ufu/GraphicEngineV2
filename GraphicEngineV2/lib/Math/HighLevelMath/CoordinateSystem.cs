using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class CoordinateSystem
    {
        public Point InitialPoint { get; set; }
        public VectorSpace Basis { get; set; }

        public CoordinateSystem(Point initialPoint, VectorSpace basis)
        {
            InitialPoint = initialPoint;
            Basis = basis;
        }
    }
}
