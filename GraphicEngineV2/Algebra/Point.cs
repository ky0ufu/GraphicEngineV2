using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class Point
    {
        public float[] point { get; set; }

        public Point(float[] point)
        {
            if (point == null)
                PointException.InitError();
            this.point = point;
        }
        public int PointSize()
        {
            return point.Length;
        }


        static public Point operator +(Point ptr, Vector vec)
        {
            vec = Vector.ToNotTransposeVector(vec);
            if (vec.VectorSize() != ptr.PointSize())
                PointException.BadSizes();
            Point res = new Point(new float[ptr.PointSize()]);
            for (int i = 0; i < ptr.PointSize(); i++)
            {
                res.point[i] = ptr.point[i] + vec.Data[i, 0];
            }
            return res;
        }

        static public Point operator -(Point ptr, Vector vec)
        {
            return ptr - ((-1) * vec);
        }
    }
}
