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

            Matrix rotateMatrix = Matrix.GetRotateMatrix(inds, angle, normal.VectorSize());

            normal = Vector.RotateVector(rotateMatrix, normal);

            SetProperty("normal", normal);
        }

        public override void Rotate3D(float alpha, float betta, float gamma)
        {
            Vector normal = GetProperty("normal");

            normal = Matrix.GetTeitBryanMatrix(alpha, betta, gamma) * normal;
            SetProperty("normal", normal);
        }

        public override float IntersectionDistance(Ray ray)
        {

            Vector posPlane = Point.toVector(GetProperty("position"));
            Vector normalPlane = GetProperty("normal");

            Vector rayStart = Point.toVector(ray.InitialPoint);
            Vector rayDir = ray.Direction;
;
            float scalarWithPoints = Vector.ScalarProduct(normalPlane, rayStart - posPlane);
            float scalarWithDir = Vector.ScalarProduct(normalPlane, rayDir);

            if (scalarWithPoints == 0)
                return 0f;
            if (scalarWithDir == 0)
                return float.PositiveInfinity;

            float scalarCoef = scalarWithDir / scalarWithPoints;

            Vector endPoint = rayStart + (scalarCoef * ray.Direction);

            return (endPoint - rayStart).VectorLength();            
        }
    }
}
