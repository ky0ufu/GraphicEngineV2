using GraphicEngineV2;
using System;
using System.Collections.Generic;

namespace Engine
{
    public class Game
    {
        public CoordinateSystem CS { get; set; }

        private static CoordinateSystem CoordSystem { get; set; }

        public EntitiesList Entities { get; set; }

        public Game(CoordinateSystem cS, EntitiesList entities)
        {
            if (cS != null || entities != null)
                throw new Exception();

            CS = cS;
            Entities = entities;
            CoordSystem = cS;
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

        public class GameEntity : Entity
        {
            public override Identifier Id { get; }
            public GameEntity()
            {
                CoordSystem = Game.CoordSystem;
                Id = new Identifier();
                properties.Add("properties", new HashSet<dynamic>());
            }
        }

        public class GameRay: Ray
        {
            public GameRay(Point initialPoint, Vector direction) : base(initialPoint, direction)
            {
                CoordSystem= Game.CoordSystem;
                InitialPoint= initialPoint;
                Direction = direction;
            }
        }

        public class Object : GameEntity
        {

            protected Object(Point position): base()
            {
                SetProperty("position", position);
                float[,] vec = new float[,] { { 1 }, { 0 }, { 0 } };
                SetProperty("direction",new Vector(vec));
            }

            public Object(Point position, Vector direction) : this(position)
            {
                SetProperty("direction", direction);
            }

            public void Move(Vector direction)
            {

            }
            public void PlanarRotate(int[] inds, float angle)
            {
                if (inds.Length != 2)
                    throw new Exception();
            }

            public void Rotate3D(float alpha, float betta, float gamma)
            {
                
            }
        }
        public class Camera : Object
        {
            public Camera(Point position, Vector direction, float drawDistance, float fov) : base(position, direction)
            {
                SetProperty("fov", fov);
                SetProperty("vfov", 16f / 9 * fov);
                SetProperty("drawDistance", drawDistance);
            }

            public Camera(Point position, Vector direction, float drawDistance, float fov, float vfov) : 
                this(position, direction, drawDistance, fov)  
            {
                SetProperty("vfov", vfov);
            }

            public Camera(Point position, Point lookAt, float fov, float drawDistance) : base(position)
            {
                SetProperty("LookAt", lookAt);
                SetProperty("fov", fov);
                SetProperty("vfov", 16f / 9 * fov);
                SetProperty("drawDistance", drawDistance);
            }

            public Camera(Point position, Point lookAt, float fov, float vfov, float drawDistance) : 
                this(position, lookAt, fov, drawDistance)
            {
                SetProperty("vfov", vfov);
            }
        }
    }
}
