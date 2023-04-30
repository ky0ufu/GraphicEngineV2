using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class Matrix
    {
        public int Rows()
        {
            return data.GetLength(0);
        }
        public int Cols()
        {
            return data.GetLength(1);
        }
        public float[,] data { get; set; }

        public float this[int index, int jndex]
        {
            get { return data[index, jndex]; }
            set => data[index, jndex] = value;
        }

        public Matrix(float[,] matrix)
        {
            if (matrix == null)
                MatrixException.InitException();
            this.data = matrix;
        }
        public Matrix(int n, int m)
        {
            if (n <= 0 || m <= 0)
                MatrixException.InitException();
            data = new float[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    data[i, j] = 0;
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
                    newMatrix.data[i, j] = data[j, i];
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
                return data[0, 0];
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
                                submatrix[i - 1, j] = data[i, j];
                            }
                            else if (j > k)
                            {
                                submatrix[i - 1, j - 1] = data[i, j];
                            }
                        }
                    }
                    int sign = (k % 2 == 0) ? 1 : -1;

                    det += sign * data[0, k] * new Matrix(submatrix).Determinant();
                }
                return det;
            }
        }

        public static Matrix Inverse(Matrix matrix)
        {
            if (matrix.Cols() != matrix.Rows() || matrix.Determinant() == 0)
                MatrixException.NotSquareException();
            int n = matrix.Rows();
            Matrix identity = Matrix.Identity(n);
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (i != j)
                    {
                        float ratio = matrix.data[i, j] / matrix.data[j, j];
                        for (int k = 0; k < n; k++)
                        {
                            matrix.data[i, k] -= ratio * matrix.data[j, k];
                            identity.data[i, k] -= ratio * identity.data[j, k];
                        }
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                float div = matrix.data[i, i];
                for (int j = 0; j < n; j++)
                {
                    matrix.data[i, j] /= div;
                    identity.data[i, j] /= div;
                }
            }
            return identity;
        }

        public static Matrix Gram(Vector[] Basis)
        {
            int row, col;
            col = Basis.Length;
            row = Basis[0].data.Length;
            for (int i = 1; i < col; i++)
                if (row != Basis[i].VectorSize())
                    throw new EngineException("Basis size not equal vector size in Basis");

            if (row != col)
                MatrixException.NotSquareException();

            Matrix gram = new Matrix(new float[row, col]);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    gram.data[i, j] = Vector.ScalarProduct(Basis[i], Basis[j]);
            }
            return gram;

        }
        public static Matrix MatrixMultiply(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Cols() != matrix2.Rows())
                MatrixException.InvalidSizes();

            Matrix result = new Matrix(new float[matrix1.Rows(), matrix2.Cols()]);

            for (int i = 0; i < matrix1.Rows(); i++)
                for (int j = 0; j < matrix2.Cols(); j++)
                    for (int k = 0; k < matrix2.Rows(); k++)
                        result.data[i, j] += matrix1.data[i, k] * matrix2.data[k, j];
            return result;
        }

        public static Matrix MatrixScalarMuliplication(Matrix matrix, float scalar)
        {
            for (int i = 0; i < matrix.Rows(); i++)
            {
                for (int j = 0; j < matrix.Cols(); j++)
                    matrix.data[i, j] *= scalar;
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
                    result.data[i, j] = matrix1.data[i, j] + matrix2.data[i, j];
            return result;
        }

        public static Matrix Sub(Matrix matrix1, Matrix matrix2)
        {
            return matrix1 + ((-1) * matrix2);
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
            return matrix * scalar;
        }
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            return MatrixMultiply(matrix1, matrix2);
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
                    Console.Write(data[i, j]);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }
        private static float ConvertToRadian(float angle)
        {
            return angle * (float)Math.PI / 180.0f;
        }
        public static Matrix GetRotateMatrix(int[]axesIndecies,float angle, int size)
        {
            angle = ConvertToRadian(angle);
            Matrix rotationMatrix = Identity(size);
            int n = (axesIndecies[1] + axesIndecies[0]) % 2 == 0 ? 1 : 0;
            rotationMatrix.data[axesIndecies[0], axesIndecies[0]] = (float)Math.Cos(angle);
            rotationMatrix.data[axesIndecies[1], axesIndecies[1]] = (float)Math.Cos(angle);
            rotationMatrix.data[axesIndecies[1], axesIndecies[0]] = (float)(Math.Pow((-1), n) * Math.Sin(angle));
            rotationMatrix.data[axesIndecies[0], axesIndecies[1]] = (float)(Math.Pow((-1), n+1) * Math.Sin(angle));
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
    }
}