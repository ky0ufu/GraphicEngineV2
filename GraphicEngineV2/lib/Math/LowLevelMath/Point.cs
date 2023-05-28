using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class Point
    {
        public float[] Data { get; protected set; }

        public void SetPoint(float[] data)
        {
            Data = RoundedFloat.RoundArray(data);
        }

        public Point(float[] point)
        {
            if (point == null)
                PointException.InitError();
            this.Data = RoundedFloat.RoundArray(point);
        }
        public int PointSize()
        {
            return Data.Length;
        }

        static public Point Add(Point ptr, Vector vec)
        {
            vec = Vector.ToNotTransposeVector(vec);
            if (vec.VectorSize() != ptr.PointSize())
                PointException.BadSizes();
            Point res = new Point(new float[ptr.PointSize()]);
            for (int i = 0; i < ptr.PointSize(); i++)
            {
                res.Data[i] = ptr.Data[i] + vec.Data[i, 0];
            }
            return res;
        }
        static public Point operator +(Point ptr, Vector vec)
        {
            return Add(ptr, vec);
        }

        static public Point operator -(Point ptr, Vector vec)
        {
            return Add(ptr, -vec);
        }

        static public bool operator ==(Point ptr1, Point ptr2)
        {
            return AreEqual(ptr1, ptr2);
        }

        static public bool operator !=(Point ptr1, Point ptr2)
        {
           return !AreEqual(ptr1, ptr2);
        }

        static public bool AreEqual(Point ptr1, Point ptr2)
        {
            if (ptr1.PointSize() != ptr2.PointSize())
                return false;

            for (int i = 0; i < ptr1.PointSize(); i++)
                if (ptr1.Data[i] != ptr2.Data[i])
                    return false;

            return true;
        }

        public float this[int index]
        {
            get
            {
                return Data[index];
            }
            set
            {
                float data = value;
                Data[index] = RoundedFloat.RoundFloat(data);
            }
        }
}
