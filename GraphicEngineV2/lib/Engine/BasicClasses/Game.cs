using GraphicEngineV2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Game
    {
        public CoordinateSystem CS { get; set; }
        public EntitiesList Entities { get; set; }
        public EventSystem Events { get; set; }
        public static EngineConfiguration config = new EngineConfiguration();

        public Game(CoordinateSystem cS, EntitiesList entities, EventSystem eventSystem)
        {
            CS = cS;
            Entities = entities;
            Events = eventSystem;
        }

        public void Run()
        {

        }

        public void Update()
        {

        }

        public void Exit()
        {

        }
        public class GameRay: Ray
        {
            public GameRay(Point initialPoint, Vector direction, Game game) : base(initialPoint, direction)
            {
                CoordSystem = game.CS;
            }
        }

        public Camera CreateCamera(Point position,Vector direction, float drawDistance, float fov )
        {
            return new Camera(position, direction, this, RoundedFloat.RoundFloat(drawDistance), fov);
        }

        public Camera CreateCamera(Point position, Point lookAt, float drawDistance, float fov)
        {
            return new Camera(position, lookAt, this, RoundedFloat.RoundFloat(drawDistance), fov);
        }

        public Camera CreateCamera(Point position, Vector direction, float drawDistance, float fov, float vfov)
        {
            return new Camera(position, direction, this, RoundedFloat.RoundFloat(drawDistance), fov, vfov);
        }

        public Camera CreateCamera(Point position, Point lookAt, float drawDistance, float fov, float vfov)
        {
            return new Camera(position, lookAt, this, RoundedFloat.RoundFloat(drawDistance), fov, vfov);
        }

        public Object CreateObject(Point position, Vector direction)
        {
            return new Object(position, direction, this);
        }

        public HyperEllipsoid CreateHyperEllipsoid(Point position, Vector direction, float[] semiAxes)
        {
            return new HyperEllipsoid(position, this, direction, semiAxes);
        }

        public HyperPlane CreateHyperPlane(Point position, Vector normal)
        {
            return new HyperPlane(position, this, normal);
        }

        public class Canvas
        {
            protected int n;
            protected int m;          
            public Matrix distances;
            protected Game game;
            protected Camera camera;
            protected char[] charmap = ".:;><+r*zsvfwqkP694VOGbUAKXH8RD#$B0MNWQ%&@".ToCharArray();

            public Canvas(int n, int m, Game game, Camera camera) 
            {
                distances = new Matrix(n, m);
                this.game = game;
                this.camera = camera;

                this.n = n;
                this.m = m;
            }

            public virtual void Draw()              
            {
                Console.Clear();
                float deltaDist = camera.GetProperty("drawDistance") / charmap.Length;
                for (int i = 0; i < n; i++) 
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (distances[i, j] > camera.GetProperty("drawDistance") || distances[i, j] == float.PositiveInfinity)
                            System.Console.Write(' ');
                        else
                        {
                            char outChar = charmap[charmap.Length - 1 - (int)(distances[i,j] / deltaDist)];
                            System.Console.Write(outChar);
                        }
                        
                    }
                    System.Console.WriteLine();

                }
                System.Console.SetCursorPosition(n / 2, m / 2);
            }
            public virtual void Update()
            {
                Ray[,] rayMatrix = camera.GetRaysMatrix(n, m);
                for (int i = 0; i < n; i++) 
                {
                    for (int j = 0; j < m; j++)
                    {
                        float distValue = float.PositiveInfinity;
                        foreach (var obj in game.Entities.Entities)
                        {
                            float dist = obj.IntersectionDistance(rayMatrix[i, j]);
                            distValue = Math.Min(dist, distValue);
                        }
                        distances[i, j] = distValue;
                    }
                }
            }           
        }
      
       
    }

}
