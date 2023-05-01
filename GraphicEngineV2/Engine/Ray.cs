using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEngineV2;

namespace Engine
{
    public class Ray
    {
        public CoordinateSystem CoordSystem { get; set; }
        public Point InitialPoint { get; set; }
        public Vector Direction { get; set; }

        public Ray(CoordinateSystem cS, Point initialPoint, Vector direction)
        {
            CoordSystem = cS;
            InitialPoint = initialPoint;
            Direction = direction;
        }

        protected Ray(Point initialPoint, Vector direction)
        {
            InitialPoint = initialPoint;
            Direction = direction;
        }
    }
}
