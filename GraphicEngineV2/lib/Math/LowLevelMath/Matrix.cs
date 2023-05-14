using System;

namespace GraphicEngineV2
{
    public class Matrix
    {
        public int Rows()
        {
            return Data.GetLength(0);
        }
        public int Cols()
        {
            return Data.GetLength(1);
        }
        public virtual float[,] Data { get; protected set; }

        public virtual void SetData(float[,] data)
        {
            Data = RoundedFloat.RoundedMatrix(data);
        }

        public float this[int index, int jndex]
        {
            get { return Data[index, jndex]; }
            set
            {
                float val = value;
                Data[index, jndex] = RoundedFloat.RoundFloat(val);
            }
        }

        public Matrix(float[,] matrix)
        {
            if (matrix == null)
                MatrixException.InitException();

            this.Data = RoundedFloat.RoundedMatrix(matrix);
        }
        public Matrix(int n, int m)
        {
            if (n <= 0 || m <= 0)
                MatrixException.InitException();

            Data = new float[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    Data[i, j] = 0;
        }
        public Matrix(int n) : this(n, n)
        {
        }
        public Matrix Transpose()
        {
            int newRows = Cols();
            int newCols = Rows();
            Matrix newMatrix = new Matrix(new float[newRows, newCols]);
            for (int i = 0; i < newRows; i++)
                for (int j = 0; j < newCols; j++)
                {
                    newMatrix.Data[i, j] = Data[j, i];
                }
            return newMatrix;
        }

        public static Matrix Identity(int n)
        {
            float[,] identityMatrix = new float[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    identityMatrix[i, j] = i == j ? 1 : 0;
                }
            return new Matrix(identityMatrix);
        }

        public float Determinant()
        {
            if (Rows() != Cols())
            {
                MatrixException.NotSquareException();
            }

            int n = Rows();
            if (n == 1)
                return RoundedFloat.RoundFloat(Data[0, 0]);
            else
            {
                float det = 0;
                for (int k = 0; k < n; k++)
                {
                    float[,] submatrix = new float[n - 1, n - 1];
                    for (int i = 1; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (j < k)
                            {
                                submatrix[i - 1, j] = Data[i, j];
                            }
                            else if (j > k)
                            {
                                submatrix[i - 1, j - 1] = Data[i, j];
                            }
                        }
                    }
                    int sign = (k % 2 == 0) ? 1 : -1;

                    det += sign * Data[0, k] * new Matrix(submatrix).Determinant();
                }
                return RoundedFloat.RoundFloat(det);
            }
        }

        public static Matrix Inverse(Matrix matrix)
        {
            if (matrix.Cols() != matrix.Rows() || matrix.Determinant() == 0)
                MatrixException.NotSquareException();

            int n = matrix.Rows();
            Matrix identity = Identity(n);

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (i != j)
                    {
                        float value = matrix.Data[i, j] / matrix.Data[j, j];
                        for (int k = 0; k < n; k++)
                        {
                            matrix.Data[i, k] -= value * matrix.Data[j, k];
                            identity.Data[i, k] -= value * identity.Data[j, k];
                        }
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                float div = matrix.Data[i, i];
                for (int j = 0; j < n; j++)
                {
                    matrix.Data[i, j] /= div;
                    identity.Data[i, j] /= div;
                }
            }
            identity.Data = RoundedFloat.RoundedMatrix(identity.Data);
            return identity;
        }

        public static Matrix Gram(Vector[] Basis)
        {
            int row, col;
            col = Basis.Length;
            row = Basis[0].Data.Length;
            for (int i = 1; i < col; i++)
                if (row != Basis[i].VectorSize())
                    VectorSpaceException.NotBasis();

            if (row != col)
                MatrixException.NotSquareException();

            Matrix gram = new Matrix(new float[row, col]);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    gram.Data[i, j] = Vector.ScalarProduct(Basis[i], Basis[j]);
            }
            return gram;

        }
        public static Matrix MatrixMultiplication(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Cols() != matrix2.Rows())
                MatrixException.InvalidSizes();

            Matrix result = new Matrix(new float[matrix1.Rows(), matrix2.Cols()]);

            for (int i = 0; i < matrix1.Rows(); i++)
                for (int j = 0; j < matrix2.Cols(); j++)
                {
                    for (int k = 0; k < matrix2.Rows(); k++)
                        result.Data[i, j] += matrix1.Data[i, k] * matrix2.Data[k, j];

                    result.Data[i, j] = RoundedFloat.RoundFloat(result.Data[i, j]);
                }

            return result;
        }

        public static Matrix MatrixScalarMuliplication(Matrix matrix, float scalar)
        {
            for (int i = 0; i < matrix.Rows(); i++)
            {
                for (int j = 0; j < matrix.Cols(); j++)
                {
                    matrix.Data[i, j] *= scalar;
                    matrix.Data[i, j] = RoundedFloat.RoundFloat(matrix.Data[i, j]);
                }
                
            }
            return matrix;
        }

        public static Matrix Add(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Cols() != matrix2.Cols() || matrix1.Rows() != matrix2.Rows())
            {
                MatrixException.DiffirentSizes();
            }
            Matrix result = new Matrix(new float[matrix1.Rows(), matrix1.Cols()]);

            for (int i = 0; i < matrix1.Rows(); i++)
                for (int j = 0; j < matrix2.Cols(); j++)
                    result.Data[i, j] = RoundedFloat.RoundFloat(matrix1.Data[i, j] + matrix2.Data[i, j]);
            return result;
        }

        public static Matrix Sub(Matrix matrix1, Matrix matrix2)
        {
            return Add(matrix1, MatrixScalarMuliplication(-1, matrix2));
        }


        public static Matrix MatrixScalarMuliplication(float scalar, Matrix matrix)
        {
            return MatrixScalarMuliplication(matrix, scalar);
        }


        public static Matrix operator *(Matrix matrix, float scalar)
        {
            return MatrixScalarMuliplication(matrix, scalar);
        }
        public static Matrix operator *(float scalar, Matrix matrix)
        {
            return MatrixScalarMuliplication(matrix, scalar);
        }
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            return MatrixMultiplication(matrix1, matrix2);
        }

        public static Matrix operator /(Matrix mat, float scalar)
        {
            if (scalar == 0)
                throw new DivideByZeroException();

            return MatrixScalarMuliplication(mat, 1 / scalar);
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            return Add(matrix1, matrix2);
        }
        
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return Sub(matrix1, matrix2);
        }
        public virtual void Print()
        {
            for (int i = 0; i < Rows(); i++)
            {
                for (int j = 0; j < Cols(); j++)
                {
                    Console.Write(Data[i, j]);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }
        private static float ConvertToRadian(float angle)
        {
            return angle * RoundedFloat.PI() / 180.0f;
        }
        public static Matrix GetRotateMatrix(int[]axesIndecies,float angle, int size)
        {
            angle = ConvertToRadian(angle);
            Matrix rotationMatrix = Identity(size);
            int n = (axesIndecies[1] + axesIndecies[0]) % 2 == 0 ? 1 : 0;

            float cos = RoundedFloat.RoundFloat((float)Math.Cos(angle));
            float sin = RoundedFloat.RoundFloat((float)Math.Sin(angle));

            if (angle == (RoundedFloat.PI() / 2) || angle == (3 * RoundedFloat.PI() / 2))
            {
                cos = 0.0f;
            }
            if (angle == RoundedFloat.PI() || angle == 0.0f)
            {
                sin = 0.0f;
            }
          
            rotationMatrix.Data[axesIndecies[0], axesIndecies[0]] = cos;
            rotationMatrix.Data[axesIndecies[1], axesIndecies[1]] = cos;

            rotationMatrix.Data[axesIndecies[1], axesIndecies[0]] = (float)(Math.Pow((-1), n) * sin);
            rotationMatrix.Data[axesIndecies[0], axesIndecies[1]] = (float)(Math.Pow((-1), n+1) * sin);

            return rotationMatrix;
        }
        private static Matrix Rx(float angle)
        {
            angle = ConvertToRadian(angle);
            int[] RxAxis = { 1, 2 };
            return GetRotateMatrix(RxAxis, angle, 3);
        }
        private static Matrix Ry(float angle)
        {
            angle = ConvertToRadian(angle);
            int[] RyAxis = { 0, 2 };
            return GetRotateMatrix(RyAxis, angle, 3);
        }
         private static Matrix Rz(float angle)
        {
            angle = ConvertToRadian(angle);
            int[] RzAxis = { 0, 1 };
            return GetRotateMatrix(RzAxis, angle, 3);
        }
        public static Matrix GetTeitBryanMatrix(float alpha, float betta, float gamma)
        {
            return Rx(alpha) * Ry(betta) * Rz(gamma);

        }

        public static bool operator ==(Matrix left, Matrix right) 
        {
            return AreEqual(left, right);
        }

        public static bool operator !=(Matrix left, Matrix right)
        {
            return !AreEqual(left, right);
        }

        public static bool AreEqual(Matrix mat1, Matrix mat2)
        {
            if (mat1.Rows() != mat2.Rows() || mat1.Cols() != mat2.Cols())
                return false;

            for (int i = 0; i < mat1.Rows(); i++)
                for (int j = 0; j < mat2.Cols(); j++)
                {
                    if (mat1.Data[i, j] != mat2.Data[i, j])
                        return false;
                }
            return true;
        }
    }
}