using System;
using GraphicEngineV2;

namespace Engine
{
    public class Object : Entity
    {

        protected Object(Point position, Game game) : base()
        {
            CoordSystem = game.CS;
            SetProperty("position", position);
        }

        public Object(Point position, Vector direction, Game game) : this(position, game)
        {
            direction = direction.Normalize();

            SetProperty("direction", direction);
        }

        public void Move(Vector moveDirection)
        {
            Point currentPos = GetProperty("position");

            SetProperty("position", Point.Add(currentPos, moveDirection));
        }

        public virtual void PlanarRotate(int[] inds, float angle)
        {
            if (inds.Length != 2)
                throw new Exception();

            Vector direction = GetProperty("direction");

            Matrix rotateMatrix = Matrix.GetRotateMatrix(inds, angle, direction.VectorSize());

            direction = Vector.RotateVector(rotateMatrix, direction);

            SetProperty("direction", direction);
        }

        public virtual void Rotate3D(float alpha, float betta, float gamma)
        {
            Vector direction = GetProperty("direction");

            direction = Matrix.GetTeitBryanMatrix(alpha, betta, gamma) * direction;
            SetProperty("direction", direction);
        }

        public void SetDirection(Vector direction)
        {
            SetProperty("direction", direction.Normalize());
        }
        public override float IntersectionDistance(Ray ray)
        {
            return float.PositiveInfinity;
        }
    }
}
