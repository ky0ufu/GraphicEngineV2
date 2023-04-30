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
        CoordinateSystem CS { get; set; }
        Point InitialPoint { get; set; }
        Vector Direction { get; set; }

        public Ray(CoordinateSystem cS, Point initialPoint, Vector direction)
        {
            CS = cS;
            InitialPoint = initialPoint;
            Direction = direction;
        }
    }
}
