using GraphicEngineV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Camera : Object
    {
        private static float Rh = 16 / 9;

        public Camera(Point position, Vector direction, Game game, float drawDistance, float fov) : base(position, direction, game)
        {
            if (fov >= RoundedFloat.PI())
                fov = RoundedFloat.PI() - 0.02f;

            float vfov = RoundedFloat.RoundFloat((float)Math.Atan(Rh * Math.Tan(fov / 2)));

            SetProperty("fov", fov);
            SetProperty("vfov", vfov);
            SetProperty("drawDistance", drawDistance);
        }

        public Camera(Point position, Vector direction, Game game, float drawDistance, float fov, float vfov) :
            this(position, direction, game, drawDistance, fov)
        {
            if (vfov >= RoundedFloat.PI())
                vfov = RoundedFloat.PI() - 0.02f;

            SetProperty("vfov", vfov);
        }

        public Camera(Point position, Point lookAt, Game game, float drawDistance, float fov) : base(position, game)
        {
            if (fov >= RoundedFloat.PI())
                fov = RoundedFloat.PI() - 0.02f;

            float vfov = RoundedFloat.RoundFloat((float)Math.Atan(Rh * Math.Tan(fov / 2)));

            SetProperty("lookAt", lookAt);
            SetProperty("fov", fov);
            SetProperty("vfov", vfov);
            SetProperty("drawDistance", drawDistance);

            Vector direction = Vector.GetVectorFromPoints(GetProperty("position"), GetProperty("lookAt"));
            direction = direction.Normalize();
            SetProperty("direction", direction);
        }

        public Camera(Point position, Point lookAt, Game game, float drawDistance,  float fov, float vfov) :
            this(position, lookAt, game, fov, drawDistance)
        {
            if (vfov >= RoundedFloat.PI())
                vfov = RoundedFloat.PI() - 0.02f;

            SetProperty("vfov", vfov);
        }


        public Ray[,] GetRaysMatrix(int n, int m)
        {
            float deltaAlpha = RoundedFloat.RoundFloat(GetProperty("fov") / n);

            float deltaBetta = RoundedFloat.RoundFloat(GetProperty("vfov") / m);

            float zeroAngleX = RoundedFloat.RoundFloat(GetProperty("fov") / 2);

            float zeroAngleY = RoundedFloat.RoundFloat(GetProperty("vfov") / 2);

            Vector direction = GetProperty("direction");

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

                    RayMatrix[i, j] = new Ray(CoordSystem, GetProperty("position"), proectionDir);
                }
            }
            return RayMatrix;
        }

    }
}
