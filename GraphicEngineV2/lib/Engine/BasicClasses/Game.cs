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
                CoordSystem = Game.CoordSystem;
            }
        }

        public class Object : GameEntity
        {

            protected Object(Point position): base()
            {
                SetProperty("position", position);
            }

            public Object(Point position, Vector direction) : this(position)
            {
                direction = direction.Normalize();

                SetProperty("direction", direction);
            }

            public void Move(Vector moveDirection)
            {
                Point currentPos = GetProperty("position");
                RemoveProperty("position");

                SetProperty("position", Point.Add(currentPos, moveDirection));
            }

            public virtual void PlanarRotate(int[] inds, float angle)
            {
                if (inds.Length != 2)
                    throw new Exception();

                Vector direction = GetProperty("direction");
                RemoveProperty("direction");

                Matrix rotateMatrix = Matrix.GetRotateMatrix(inds, angle, direction.VectorSize());

                direction = Vector.RotateVector(rotateMatrix, direction);

                SetProperty("direction", direction);
            }

            public virtual void Rotate3D(float alpha, float betta, float gamma)
            {
                Vector direction = GetProperty("direction");
                RemoveProperty("direction");
                
                direction = Matrix.GetTeitBryanMatrix(alpha, betta, gamma) * direction;
                SetProperty("direction", direction);
            }

            public void SetDirection(Vector direction)
            {
                SetProperty("direction", direction.Normalize());
            }
            public virtual float IntersectionDistance(Ray ray)
            {
                return 0f;
            }
        }
        public class Camera : Object
        {
            public Camera(Point position, Vector direction, float drawDistance, float fov) : base(position, direction)
            {
                if (fov == RoundedFloat.PI())
                    fov = fov - 0.02f;

                SetProperty("fov", fov);
                SetProperty("vfov", 16f / 9 * fov);
                SetProperty("drawDistance", drawDistance);
            }

            public Camera(Point position, Vector direction, float drawDistance, float fov, float vfov) : 
                this(position, direction, drawDistance, fov)  
            {
                if (vfov == RoundedFloat.PI())
                    vfov = fov - 0.02f;

                SetProperty("vfov", vfov);
            }

            public Camera(Point position, Point lookAt, float fov, float drawDistance) : base(position)
            {
                if (fov == RoundedFloat.PI())
                    fov = fov - 0.02f;
                
                SetProperty("lookAt", lookAt);
                SetProperty("fov", fov);
                SetProperty("vfov", 16f / 9 * fov);
                SetProperty("drawDistance", drawDistance);
            }

            public Camera(Point position, Point lookAt, float fov, float vfov, float drawDistance) : 
                this(position, lookAt, fov, drawDistance)
            {
                if (vfov == RoundedFloat.PI())
                    fov = fov - 0.02f;

                SetProperty("vfov", vfov);
            }

            public Ray[,] GetRaysMatrix(int n, int m)
            {
                float deltaAlpha = RoundedFloat.RoundFloat(GetProperty("fov") / n);

                float deltaBetta = RoundedFloat.RoundFloat(GetProperty("vfov") / m);

                float zeroAngleX = RoundedFloat.RoundFloat(GetProperty("fov") / 2);

                float zeroAngleY = RoundedFloat.RoundFloat(GetProperty("vfov") / 2);

                Vector direction;

                if (GetProperty("direction") == null)
                {
                    direction = Vector.GetVectorFromPoints(GetProperty("position"), GetProperty("lookAt"));
                    direction = direction.Normalize();
                }
                else
                    direction = GetProperty("direction");

                Ray[,] RayMatrix = new Ray[n, m];

                for (int i = 0; i < n; i++)
                {
                    float currentAngleX = deltaAlpha * i - zeroAngleX;
                    for (int j = 0; j < m; j++)
                    {
                        float currentAngleY = deltaBetta * j - zeroAngleY;

                        Vector proectionDir = Vector.RotateVector(Matrix.RotateYZ(currentAngleX), direction);

                        proectionDir = Vector.RotateVector(Matrix.RotateXZ(currentAngleY), proectionDir);

                        proectionDir = (float)Math.Pow(direction.VectorLength(), 2) / Vector.ScalarProduct(direction, proectionDir) * proectionDir;

                        RayMatrix[i, j] = new Ray(Game.CoordSystem, GetProperty("position"), proectionDir);
                    }
                }
                return RayMatrix;
            }

        }
        public class Canvas
        {
            private int n;
            private int m;          
            private Matrix distance;

            public Canvas(int n, int m) 
            {
                this.n = n;
                this.m = m;
                distance = new Matrix(n, m);
            }

            public void Draw()
            {

            }
            public void Update(Game.Camera camera)
            {

            }           
        }
        public class HyperPlane : Object
        {

            public HyperPlane(Point position, Vector normal) : base(position) 
            {
                normal = normal.Normalize();
                SetProperty("normal", normal);
            }
            public override void PlanarRotate(int[] inds, float angle)
            {
                if (inds.Length != 2)
                    throw new Exception();

                Vector normal = GetProperty("normal");
                RemoveProperty("normal");

                Matrix rotateMatrix = Matrix.GetRotateMatrix(inds, angle, normal.VectorSize());

                normal = Vector.RotateVector(rotateMatrix, normal);

                SetProperty("normal", normal);
            }

            public override void Rotate3D(float alpha, float betta, float gamma)
            {
                Vector normal = GetProperty("normal");
                RemoveProperty("normal");

                normal = Matrix.GetTeitBryanMatrix(alpha, betta, gamma) * normal;
                SetProperty("normal", normal);
            }

            public override float IntersectionDistance(Ray ray)
            {

                Point pos = GetProperty("position");
                Vector normal = GetProperty("normal");

                float d = 0f;
                float result = 0f;

                for (int i = 0; i < pos.PointSize(); i++)
                {
                    d += -(pos[i] * normal[i]);
                    result += normal[i] * ray.InitialPoint[i];
                }

                result = (Math.Abs(result + d)) / normal.VectorLength();
                return RoundedFloat.RoundFloat(result);
            }
        }

        public class HyperEllipsoid : Object
        {

            public HyperEllipsoid(Point position, Vector direction, float[] semiAxes) : base(position)
            {
                direction = direction.Normalize();
                SetProperty("direction", direction);
                SetProperty("semiAxes", semiAxes);
            }
            public override void PlanarRotate(int[] inds, float angle)
            {
                base.PlanarRotate(inds, angle);
            }

            public override void Rotate3D(float alpha, float betta, float gamma)
            {
                base.Rotate3D(alpha, betta, gamma);
            }

            public override float IntersectionDistance(Ray ray)
            {
                if (FindParams(ray) == null)
                    return float.PositiveInfinity;

                Vector rayDir = ray.Direction;

                float[] paramms = FindParams(ray);
                float distance1 = 0f;
                float distance2 = 0f;

                for (int i = 0; i < rayDir.VectorSize(); i++)
                {
                    distance1 += (float)Math.Pow(paramms[0] * rayDir[i], 2);
                    distance2 += (float)Math.Pow(paramms[1] * rayDir[i], 2);
                }
                if (distance1 < distance2)
                    return RoundedFloat.RoundFloat((float)Math.Sqrt(distance1));
                else
                    return RoundedFloat.RoundFloat((float)Math.Sqrt(distance2));
            }

            private float[] FindParams(Ray ray)
            {
                float a = 0f;
                float b = 0f;
                float c = 0f;

                float freeCoef = 0f;
                float[] result = new float[2];

                Point startRay = ray.InitialPoint;
                Vector rayDir = ray.Direction;
                float[] semiAxes = GetProperty("semiAxes");

                int dimms = startRay.PointSize();

                if (rayDir.VectorSize() != dimms || semiAxes.Length != dimms)
                    throw new Exception();

                for (int i = 0; i < dimms; i++)
                {
                    float coef = 0f;
                    for (int j = 0; j < dimms; j++) 
                    {
                        if (i == j)
                            continue;
                        coef *= semiAxes[j] * semiAxes[j];
                    }
                    a += coef * rayDir[i] * rayDir[i];
                    b += coef * startRay[i];
                    c += coef * startRay[i] * startRay[i];

                    freeCoef *= semiAxes[i] * semiAxes[i];
                }

                c -= freeCoef;
                b = 2 * b;
                float Dis = b * b - 4 * a * c;

                if (Dis < 0)
                    return null;

                if (Dis == 0)
                {
                    float ans = (-b + (float)Math.Sqrt(Dis)) / (2 * a);

                    result[0] = ans;
                    result[1] = ans;
                    return result;
                }
                else
                {
                    float ans1 = (-b + (float)Math.Sqrt(Dis)) / (2 * a);
                    float ans2 = (-b - (float)Math.Sqrt(Dis)) / (2 * a);

                    result[0] = ans1;
                    result[1] = ans2;
                    return result;
                }
            }
        }
    }

}
