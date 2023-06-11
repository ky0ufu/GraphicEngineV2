using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using CursesSharp;
using System.Windows.Input;

namespace GraphicEngineV2
{
    public class Program
    {
        private static Game CreateGame(CoordinateSystem cs, EntitiesList el)
        {
            return new Game(cs, el, new EventSystem());
        }
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
            Game g = CreateGame(cs, el);

            float[] p1 = new float[3] { 0, 0, 25 };
            Point ptr2 = new Point(p1);
            HyperEllipsoid ellips = g.CreateHyperEllipsoid(ptr2, vec2, new float[3] { 10, 10, 10 }); 
            g.Entities.AppendEntity(ellips);
            
            Ray ray = new Ray(cs, ptr, vec3);
            float[] p3 = new float[3] { 0, 0, 2 };
            Camera camera = g.CreateCamera(ptr, vec3, 40f, RoundedFloat.RoundFloat((float)(Math.PI/2)));

            Game.Canvas output = new Game.Canvas(100, 100, g, camera);


            output.Update();
            output.Draw();
            while (ConsoleKey.Escape != Console.ReadKey().Key)
                MoveSystem(ref camera, ref output);            
        }
        public static void MoveSystem(ref Camera camera, ref Game.Canvas output)
        {
            Vector direction = camera.GetProperty("direction");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {

                case ConsoleKey.D:
                    direction = Vector.RotateVector(Matrix.RotateXY(-RoundedFloat.PI() / 8), direction);
                    break;

                case ConsoleKey.A:
                    direction = Vector.RotateVector(Matrix.RotateXY(RoundedFloat.PI() / 8), direction);
                    break;

                case ConsoleKey.W:
                    direction = Vector.RotateVector(Matrix.RotateYZ(RoundedFloat.PI() / 8), direction);
                    break;

                case ConsoleKey.S:
                    direction = Vector.RotateVector(Matrix.RotateYZ(-RoundedFloat.PI() / 8), direction);
                    break;

                default:
                    break;
            }
            camera.SetDirection(direction);
            output.Update();
            output.Draw();           
        }
    }
}  
 
