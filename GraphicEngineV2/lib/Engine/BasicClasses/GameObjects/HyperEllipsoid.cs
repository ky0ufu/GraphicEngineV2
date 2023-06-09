using System;
using GraphicEngineV2;

namespace Engine
{
    public class HyperEllipsoid : Object
    {

        public HyperEllipsoid(Point position,Game game, Vector direction, float[] semiAxes) : base(position, game)
        {
            foreach (var axis in semiAxes) 
            {
                if (axis == 0.0f)
                    throw new ArgumentException("bad axis");
            }
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

            float ans1 = (-b + (float)Math.Sqrt(Dis)) / (2 * a);
            float ans2 = (-b - (float)Math.Sqrt(Dis)) / (2 * a);

            result[0] = ans1;
            result[1] = ans2;
            return result;

        }
    }
}
