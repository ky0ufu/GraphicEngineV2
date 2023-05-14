using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class Vector : Matrix
    {
        public override float[,] Data { get; protected set; }

        public override void SetData(float[,] data)
        {
            if (data.GetLength(0) != 1 && data.GetLength(1) != 1)
                VectorException.FormException();

            Data = RoundedFloat.RoundedMatrix(data);
        }
        public float this[int index]
        {
            get
            {
                return IsTranspose ? Data[0, index] : Data[index, 0];
            }
            set
            {
                float data = value;
                if (IsTranspose)
                    Data[0, index] = RoundedFloat.RoundFloat(data);
                else
                    Data[index, 0] = RoundedFloat.RoundFloat(data);
            }
        }
        public bool IsTranspose { get; private set; }

        public Vector(float[,] vector) : base(vector)
        {
            if ((vector.GetLength(1) != 1 && vector.GetLength(0) != 1))
                VectorException.InitException();

            IsTranspose = (vector.GetLength(0) == 1);

            Data = RoundedFloat.RoundedMatrix(vector);
        }
        public Vector(int n) : base(n)
        {
            if (n <= 0)
                VectorException.InitException();

            float[,] result = new float[n, 1];

            for (int i = 0; i < n; i++)
                result[i, 0] = 0.0f;

            Data = result;
            IsTranspose = false;
        }

        public int VectorSize()
        {
            return Data.GetLength(0) + Data.GetLength(1) - 1;
        }

        public static Vector Transpose(Vector vec)
        {
            vec.IsTranspose = !vec.IsTranspose;
            return ToVector((ToMatrix(vec).Transpose()));
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
                VecResult[i] = RoundedFloat.RoundFloat(this[i] / VectorLength());
            }
            return VecResult;
        }
        public static float ScalarProduct(Vector vector1, Vector vector2)
        {
            if (vector1.VectorSize() != vector2.VectorSize())
                VectorException.DiffirentSizes();
            float result = 0;
            for (int i = 0; i < vector1.VectorSize(); i++)
            {
                result += vector1[i] * vector2[i];
            }
            return RoundedFloat.RoundFloat(result);
        }

        public static Vector VectorProduct(Vector vector1, Vector vector2)
        {
            if (vector1.VectorSize() != 3 || vector2.VectorSize() != 3)
                VectorException.BadDimmensional();

            if (vector1.IsTranspose != vector2.IsTranspose)
                VectorException.FormException();

            Vector result = new Vector(new float[3, 1]);

            result[0] = RoundedFloat.RoundFloat(vector1[1] * vector2[2] - vector1[2] * vector2[1]);
            result[1] = RoundedFloat.RoundFloat(-(vector1[0] * vector2[2] - vector1[2] * vector2[0]));
            result[2] = RoundedFloat.RoundFloat(vector1[0] * vector2[1] - vector1[1] * vector2[0]);
            return result;
        }

        public float VectorLength()
        {
            return RoundedFloat.RoundFloat((float)Math.Sqrt(ScalarProduct(this, this)));
        }

        public static Matrix ToMatrix(Vector vector)
        {
            return new Matrix(vector.Data);
        }
        public static Vector ToVector(Matrix matrix)
        {
            return new Vector(matrix.Data);
        }
        public static Vector Add(Vector vec1, Vector vec2)
        {
            if (vec1.VectorSize() != vec2.VectorSize())
                VectorException.DiffirentSizes();
            if (vec1.IsTranspose != vec2.IsTranspose)
                VectorException.FormException();

            return ToVector(ToMatrix(vec1) + ToMatrix(vec2));
        }


        public static Vector VectorMultiplication(Vector vec, float scalar)
        {
            return ToVector(MatrixScalarMuliplication(ToMatrix(vec), scalar));
        }

        public static Vector VectorMultiplication(float scalar, Vector vec)
        {
            return VectorMultiplication(vec, scalar);
        }

        public static Vector Sub(Vector vec1, Vector vec2)
        {
            return Add(vec1, VectorMultiplication(vec2, -1));
        }
        public static Vector operator +(Vector vec1, Vector vec2)
        {
            return Add(vec1, vec2);
        }

        public static Vector operator -(Vector vec)
        {
            return VectorMultiplication(vec, -1);
        }

        public static Vector operator -(Vector vec1, Vector vec2)
        {
            return Sub(vec1, vec2);
        }

        public static Vector operator *(Vector vec, float scalar)
        {
           return VectorMultiplication(vec, scalar);
        }
        public static Vector operator *(float scalar, Vector vec)
        {
            return VectorMultiplication(vec, scalar);
        }
        public static Matrix operator *(Vector vec1, Vector vec2)
        {
            if (vec1.IsTranspose == vec2.IsTranspose)
                VectorException.FormException();
            return ToMatrix(vec1) * ToMatrix(vec2);
        }

        public static Vector VectorByMatrix(Vector vec, Matrix mat)
        {
            if (vec.IsTranspose)
                VectorException.FormException();

            return ToVector(MatrixMultiplication(mat,ToMatrix(vec)));
        }

        public static Vector operator * (Matrix mat, Vector vec)
        {
            return VectorByMatrix(vec, mat);
        }
        public static Vector operator ^(Vector vec1, Vector vec2)
        {
            return VectorProduct(vec1, vec2);
        }
        public static float operator %(Vector vec1, Vector vec2)
        {
            return ScalarProduct(vec1, vec2);
        }
        public override void Print()
        {
            for (int i = 0; i <Data.GetLength(0); i++)
            {
                for (int j =0; j < Data.GetLength(1); j++)
                {
                    Console.Write(Data[i, j]);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

        public static bool operator ==(Vector vec1, Vector vec2)
        {
            return AreEqual(vec1, vec2);
        }

        public static bool operator !=(Vector vec1, Vector vec2)
        {
            return !AreEqual(vec1, vec2);
        }

        public static bool AreEqual(Vector vec1, Vector vec2)
        {
            if (vec1.IsTranspose != vec2.IsTranspose || vec1.VectorSize() != vec2.VectorSize())
                return false;

            for (int i = 0; i < vec1.VectorSize(); i++)
            {
                
                if (vec1[i] != vec2[i])              
                    return false;

            }
            return true;
        }

    }
}
