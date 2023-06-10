using GraphicEngineV2;
using System;
using System.Collections.Generic;

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
            private int n;
            private int m;          
            private Matrix distances;
            private Game game;

            public Canvas(int n, int m, Game game) 
            {
                this.n = n;
                this.m = m;
                distances = new Matrix(n, m);
                this.game = game;
            }

            public void Draw()
            {


            }
            public void Update(Camera camera)
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
