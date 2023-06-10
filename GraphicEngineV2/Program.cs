using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace GraphicEngineV2
{
    public class Program
    {
        static void Main(string[] args)
        {


            Vector vec1 = new Vector(3);
            Vector vec2 = new Vector(3);
            Vector vec3 = new Vector(3);
            vec1[0] = 1;
            vec2[1] = 1;
            vec3[2] = 1;
            Vector[] based = new Vector[3] { vec1, vec2, vec3 };
            VectorSpace vs = new VectorSpace(based);
            float[] p = new float[3] { 0, 0, 0 };
            Point ptr = new Point(p);
            CoordinateSystem cs = new CoordinateSystem(ptr, vs);
            EntitiesList el = new EntitiesList();
            Game g = new Game(cs, el);
            float[] p1 = new float[3] { 0, 0, 3 };
            Point ptr2 = new Point(p1);
            HyperEllipsoid ellips = g.CreateHyperEllipsoid(ptr2, vec2, new float[3] { 1, 1, 1 });
            Ray ray = new Ray(cs, ptr, vec3);
            Console.WriteLine(ellips.IntersectionDistance(ray));

        }   
    }
    
}
