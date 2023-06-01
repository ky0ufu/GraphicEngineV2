using System;
using GraphicEngineV2;

namespace Engine
{
    public class HyperPlane : Object
    {

        public HyperPlane(Point position, Game game, Vector normal) : base(position, game)
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
}
