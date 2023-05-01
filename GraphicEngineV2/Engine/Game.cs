using GraphicEngineV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            public Point Position { get; set; }

            public Vector Direction
            {
                get
                {
                    return Direction;
                }
                set
                {
                    Direction = value.Normalize();
                }
            }
            protected Object(Point position): base()
            {
                Position = position;
                float[,] vec = new float[,] { { 1 }, { 0 }, { 0 } };
                Direction = new Vector(vec);
            }

            public Object(Point position, Vector direction) : this(position)
            {
                Position = position;
                Direction = direction.Normalize();
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
            public float Fov { get; set; }
            
            public float Vfov { get; set; }

            public float DrawDistance { get; set; }

            public Point LookAt { get; set; }

            public Camera(Point position, Vector direction, float drawDistance, float fov) : base(position, direction)
            {
                Fov = fov;
                Vfov = 3 * fov / 2;
                DrawDistance = drawDistance;
            }

            public Camera(Point position, Vector direction, float drawDistance, float fov, float vfov) : 
                this(position, direction, drawDistance, fov)  
            {
                Vfov = vfov;
            }

            public Camera(Point position, Point lookAt, float fov, float drawDistance) : base(position)
            {
                LookAt = lookAt;
                Fov = fov;
                DrawDistance = drawDistance;
            }

            public Camera(Point position, Point lookAt, float fov, float vfov, float drawDistance) : 
                this(position, lookAt, fov, drawDistance)
            {
                Vfov = vfov;
            }
        }
    }
}
