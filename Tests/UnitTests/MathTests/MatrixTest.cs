using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GraphicEngineV2;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;

namespace Tests
{
    [TestClass]
    public class MatrixTests
    {
        public TestContext TestContext { get; set; }

        public bool AreEqual(Matrix mat1, Matrix mat2)
        {
            return mat1 == mat2;
        }
        public bool AreEqual(float a, float b)
        {
            return (Math.Abs(a - b) < 0.000001);
        }
        public bool AreEqualSizes(Matrix mat1, Matrix mat2)
        {
            return (mat1.Rows() == mat2.Rows() && mat1.Cols() == mat2.Cols());
        }
        public static float[,] GetMatrixFromString(string matrix, int rows, int cols)
        {
            string[] matrixValues = matrix.Split(' ');
            float[,] res = new float[rows, cols];
            int index = 0;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    res[i, j] = float.Parse(matrixValues[index], CultureInfo.InvariantCulture.NumberFormat);
                    index++;
                }
            return res;
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\MatrixData\DataTranspose.xml", "TransposeData", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestTranspose()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string matrix = Convert.ToString(TestContext.DataRow["matrix"]);
            Matrix mat = new Matrix(GetMatrixFromString(matrix, row, col)).Transpose();

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Matrix res = new Matrix(GetMatrixFromString(result, row, col));

            Assert.IsTrue(AreEqual(mat, res));
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\MatrixData\DataDeterminant.xml", "DeterminantData", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestDeterminant()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string matrix = Convert.ToString(TestContext.DataRow["matrix"]);
            int res = Convert.ToInt32(TestContext.DataRow["result"]);

            float det = (new Matrix(GetMatrixFromString(matrix, row, col))).Determinant();

            Assert.IsTrue(AreEqual(det, res));
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\MatrixData\DataIdentity.xml", "IdentityData", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestIdentity()
        {
            int n = Convert.ToInt32(TestContext.DataRow["size"]);
            string res = Convert.ToString(TestContext.DataRow["result"]);

            Matrix identity = Matrix.Identity(n);
            Matrix result = new Matrix(GetMatrixFromString(res, n, n));

            Assert.IsTrue(AreEqual(identity, result));
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\MatrixData\DataAdd.xml", "AddData", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestAdd()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string matrix1 = Convert.ToString(TestContext.DataRow["matrix1"]);
            string matrix2 = Convert.ToString(TestContext.DataRow["matrix2"]);

            Matrix A = new Matrix(GetMatrixFromString(matrix1, row, col));
            Matrix B = new Matrix(GetMatrixFromString(matrix2, row, col));

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Matrix res = new Matrix(GetMatrixFromString(result, row, col));

            Assert.IsTrue(AreEqual(A + B, res));
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\MatrixData\DataMinus.xml", "MinusData", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestMinus()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string matrix1 = Convert.ToString(TestContext.DataRow["matrix1"]);
            string matrix2 = Convert.ToString(TestContext.DataRow["matrix2"]);

            Matrix A = new Matrix(GetMatrixFromString(matrix1, row, col));
            Matrix B = new Matrix(GetMatrixFromString(matrix2, row, col));

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Matrix res = new Matrix(GetMatrixFromString(result, row, col));

            Assert.IsTrue(AreEqual(A - B, res));
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\MatrixData\DataMatrixMultiply.xml", "MatrixMultiplyData", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestMatrixMultiply()
        {
            string size1 = Convert.ToString(TestContext.DataRow["size1"]);
            int row1 = int.Parse(size1.Split()[0]);
            int col1 = int.Parse(size1.Split()[1]);

            string size2 = Convert.ToString(TestContext.DataRow["size2"]);
            int row2 = int.Parse(size2.Split()[0]);
            int col2 = int.Parse(size2.Split()[1]);

            string sizeRes = Convert.ToString(TestContext.DataRow["sizeRes"]);
            int rowRes = int.Parse(sizeRes.Split()[0]);
            int colRes = int.Parse(sizeRes.Split()[1]);

            string matrix1 = Convert.ToString(TestContext.DataRow["matrix1"]);
            string matrix2 = Convert.ToString(TestContext.DataRow["matrix2"]);

            Matrix A = new Matrix(GetMatrixFromString(matrix1, row1, col1));
            Matrix B = new Matrix(GetMatrixFromString(matrix2, row2, col2));

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Matrix res = new Matrix(GetMatrixFromString(result, rowRes, colRes));

            Assert.IsTrue(AreEqual(A * B, res));
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\MatrixData\DataScalarMultiply.xml", "MultiplyScalarData", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestScalarMultiply()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string matrix = Convert.ToString(TestContext.DataRow["matrix1"]);
            Matrix A = new Matrix(GetMatrixFromString(matrix, row, col));

            float scalar = float.Parse(Convert.ToString(TestContext.DataRow["scalar"]), CultureInfo.InvariantCulture.NumberFormat);

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Matrix res = new Matrix(GetMatrixFromString(result, row, col));

            Assert.IsTrue(AreEqual(A * scalar, res));
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\MatrixData\RotationMatrixData.xml", "Vec", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestGetRotationMatrix()
        {
            string ax = Convert.ToString(TestContext.DataRow["axis"]);
            int firstAx = int.Parse(ax.Split()[0]);
            int secondAx = int.Parse(ax.Split()[1]);
            int[] axis = { firstAx, secondAx };

            int size = int.Parse(Convert.ToString(TestContext.DataRow["size"]).Split()[0]);

            float angle = float.Parse(Convert.ToString(TestContext.DataRow["angle"]), CultureInfo.InvariantCulture.NumberFormat);

            Matrix A = Matrix.GetRotateMatrix(axis, angle, size);

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Matrix res = new Matrix(GetMatrixFromString(result, size, size));

            Assert.IsTrue(AreEqual(A, res));


        }
    }
}
