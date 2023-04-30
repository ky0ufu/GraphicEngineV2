using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class Vector
    {
        public float[,] data { get; set; }
        public float this[int index]
        {
            get
            {
                return IsTranspose ? data[0, index] : data[index, 0];
            }
            set
            {
                if (IsTranspose)
                    data[0, index] = value;
                else
                    data[index, 0] = value;
            }
        }
        public bool IsTranspose { get; private set; }

        public Vector(float[,] vector)
        {
            if (vector == null || (vector.GetLength(1) != 1 && vector.GetLength(0) != 1))
                VectorException.InitException();

            IsTranspose = (vector.GetLength(0) == 1);

            data = vector;
        }
        public Vector(int n)
        {
            if (n <= 0)
                VectorException.InitException();

            float[,] result = new float[n, 1];

            for (int i = 0; i < n; i++)
                result[i, 0] = 0.0f;

            data = result;
            IsTranspose = false;
        }

        public int VectorSize()
        {
            return IsTranspose ? data.GetLength(1) : data.GetLength(0);
        }

        public static Vector Transpose(Vector vec)
        {
            vec.IsTranspose = !vec.IsTranspose;
            return MatrixToVector((VectorToMatrix(vec).Transpose()));
        }

        public static Vector ToNotTransposeVector(Vector vec)
        {
            return vec.IsTranspose ? Transpose(vec) : vec;
        }
        public Vector Normalize()
        {
            Vector VecResult = new Vector(VectorSize());
            if (IsTranspose)
                VecResult = Transpose(VecResult);

            for (int i = 0; i < VectorSize(); i++)
            {
                VecResult[i] = this[i] / VectorLength();
            }
            return VecResult;
        }
        public static float ScalarProduct(Vector vector1, Vector vector2)
        {
            if (vector1.VectorSize() != vector2.VectorSize())
                VectorException.DiffirentSizes();
            vector1 = ToNotTransposeVector(vector1);
            vector2 = ToNotTransposeVector(vector2);
            float result = 0;
            for (int i = 0; i < vector1.VectorSize(); i++)
            {
                result += vector1[i] * vector2[i];
            }
            return result;
        }

        public static Vector VectorProduct(Vector vector1, Vector vector2)
        {
            if (vector1.VectorSize() != 3 || vector2.VectorSize() != 3)
                VectorException.BadDimmensional();
            Vector result = new Vector(new float[3, 1]);
            vector1 = ToNotTransposeVector(vector1);
            vector2 = ToNotTransposeVector(vector2);

            result[0] = vector1[1] * vector2[2] - vector1[2] * vector2[1];
            result[1] = -(vector1[0] * vector2[2] - vector1[2] * vector2[0]);
            result[2] = vector1[0] * vector2[1] - vector1[1] * vector2[0];
            return result;
        }

        public float VectorLength()
        {
            return (float)Math.Sqrt(ScalarProduct(this, this));
        }

        public static Matrix VectorToMatrix(Vector vector)
        {
            return new Matrix(vector.data);
        }
        public static Vector MatrixToVector(Matrix matrix)
        {
            return new Vector(matrix.data);
        }
        public static Vector VectorAdd(Vector vec1, Vector vec2)
        {
            if (vec1.VectorSize() != vec2.VectorSize())
                VectorException.DiffirentSizes();

            if (vec1.IsTranspose != vec2.IsTranspose)
                VectorException.FormException();

            bool resultTranspose = vec1.IsTranspose;
            vec1 = ToNotTransposeVector(vec1);
            vec2 = ToNotTransposeVector(vec2);

            Vector result = new Vector(new float[vec1.VectorSize(), 1]);
            for (int i = 0; i < vec1.VectorSize(); i++)
                result.data[i, 0] = vec1.data[i, 0] + vec2.data[i, 0];

            return resultTranspose ? Transpose(result) : result;
        }
        public static Vector operator +(Vector vec1, Vector vec2)
        {
            return VectorAdd(vec1, vec2);
        }

        public static Vector operator -(Vector vec)
        {
            return vec * (-1);
        }

        public static Vector operator -(Vector vec1, Vector vec2)
        {
            return vec1 + (-vec2);
        }

        public static Vector operator *(Vector vec, float scalar)
        {
            bool resultTranspose = vec.IsTranspose;
            vec = ToNotTransposeVector(vec);
            for (int i = 0; i < vec.VectorSize(); i++)
            {
                vec.data[i, 0] *= scalar;
            }
            return resultTranspose ? Transpose(vec) : vec;
        }
        public static Vector operator *(float scalar, Vector vec)
        {
            return vec * scalar;
        }
        public static Matrix operator *(Vector vec1, Vector vec2)
        {
            if (vec1.IsTranspose == vec2.IsTranspose)
                VectorException.FormException();
            return VectorToMatrix(vec1) * VectorToMatrix(vec2);
        }

        public static Vector operator ^(Vector vec1, Vector vec2)
        {
            return VectorProduct(vec1, vec2);
        }
        public static float operator %(Vector vec1, Vector vec2)
        {
            return ScalarProduct(vec1, vec2);
        }
        public void Print()
        {
            for (int i = 0; i <data.GetLength(0); i++)
            {
                for (int j =0; j < data.GetLength(1); j++)
                {
                    Console.Write(data[i, j]);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

    }
}
