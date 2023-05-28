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
        public CoordinateSystem CoordSystem { get; protected set; }
        public Point InitialPoint { get; protected set; }
        public Vector Direction { get; protected set; }

        public void SetCoordSystem(CoordinateSystem coordinateSystem)
        {
            CoordSystem = coordinateSystem;
        }
        public void SetInitialPoint(Point ptr)
        {
            InitialPoint = ptr;
        }
        public void SetDirection(Vector vector)
        {
            Direction = vector;
        }

        public Ray(CoordinateSystem cS, Point initialPoint, Vector direction) : this(initialPoint, direction)
        {
            CoordSystem = cS;
        }

        protected Ray(Point initialPoint, Vector direction)
        {
            InitialPoint = initialPoint;
            Direction = direction;
        }

        public void Normalize()
        {
            Direction = Direction.Normalize();
        }
    }
}
