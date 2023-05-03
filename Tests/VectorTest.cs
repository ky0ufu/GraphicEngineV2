using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEngineV2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class VectorTest
    {
        public TestContext TestContext { get; set; }
        public static bool AreEqual(Vector vec1, Vector vec2)
        {
            return vec1 == vec2;
        }
        public bool AreEqual(float a, float b)
        {
            return (Math.Abs(a - b) < 0.0001);
        }

        public static float[,] GetVectorFromString(string matrix, int rows, int cols)
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
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\VectorData\VectorAdd.xml", "AddVec", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestAdd()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string vec1 = Convert.ToString(TestContext.DataRow["vector1"]);
            string vec2 = Convert.ToString(TestContext.DataRow["vector2"]);

            Vector A = new Vector(GetVectorFromString(vec1, row, col));
            Vector B = new Vector(GetVectorFromString(vec2, row, col));

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Vector res = new Vector(GetVectorFromString(result, row, col));

            Assert.IsTrue(AreEqual(A + B, res));
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\VectorData\MinusData.xml", "Vec", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestMinus()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string vec1 = Convert.ToString(TestContext.DataRow["vector1"]);
            string vec2 = Convert.ToString(TestContext.DataRow["vector2"]);

            Vector A = new Vector(GetVectorFromString(vec1, row, col));
            Vector B = new Vector(GetVectorFromString(vec2, row, col));

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Vector res = new Vector(GetVectorFromString(result, row, col));

            Assert.IsTrue(AreEqual(A - B, res));
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\VectorData\ScalarProductData.xml", "Vec", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestScalarProduct()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string vec1 = Convert.ToString(TestContext.DataRow["vector1"]);
            string vec2 = Convert.ToString(TestContext.DataRow["vector2"]);

            Vector A = new Vector(GetVectorFromString(vec1, row, col));
            Vector B = new Vector(GetVectorFromString(vec2, row, col));

            float result = float.Parse(Convert.ToString(TestContext.DataRow["result"]), CultureInfo.InvariantCulture.NumberFormat);
            float scalarProduct = Vector.ScalarProduct(A, B);

            Assert.IsTrue(AreEqual(scalarProduct, result));
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\VectorData\ScalarMultiplyData.xml", "Vec", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestScalarMultiply()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string vec1 = Convert.ToString(TestContext.DataRow["vector1"]);
            float scalar = float.Parse(Convert.ToString(TestContext.DataRow["scalar"]), CultureInfo.InvariantCulture.NumberFormat);

            Vector A = new Vector(GetVectorFromString(vec1, row, col));

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Vector res = new Vector(GetVectorFromString(result, row, col));

            Assert.IsTrue(AreEqual(A * scalar, res));
        }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\VectorData\VectorProductData.xml", "Vec", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestVectorProduct()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);
  
            string vec1 = Convert.ToString(TestContext.DataRow["vector1"]);
            string vec2 = Convert.ToString(TestContext.DataRow["vector2"]);
            
            Vector A = new Vector(GetVectorFromString(vec1, row, col));
            Vector B = new Vector(GetVectorFromString(vec2, row, col));
            
            Vector newVec = Vector.VectorProduct(A, B);
            
            string result = Convert.ToString(TestContext.DataRow["result"]);
            Vector res = new Vector(GetVectorFromString(result, row, col));
            
            Assert.IsTrue(AreEqual(newVec, res));
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\VectorData\NormalizeData.xml", "Vec", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestNormalize()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string vec1 = Convert.ToString(TestContext.DataRow["vector1"]);
            Vector A = new Vector(GetVectorFromString(vec1, row, col));

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Vector res = new Vector(GetVectorFromString(result, row, col));
            
            Assert.IsTrue(AreEqual(A.Normalize(), res));
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\VectorData\VectorByMatrixData.xml", "Vec", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestVectorByMatrix()
        {
            string size = Convert.ToString(TestContext.DataRow["size1"]);
            int row1 = int.Parse(size.Split()[0]);
            int col1 = int.Parse(size.Split()[1]);

            string size2 = Convert.ToString(TestContext.DataRow["size2"]);
            int row2 = int.Parse(size2.Split()[0]);
            int col2 = int.Parse(size2.Split()[1]);

            string vec1 = Convert.ToString(TestContext.DataRow["vector1"]);
            Vector A = new Vector(GetVectorFromString(vec1, row1, col1));

            string matrix2 = Convert.ToString(TestContext.DataRow["mat1"]);
            Matrix B = new Matrix(MatrixTests.GetMatrixFromString(matrix2, row2, col2));

            string result = Convert.ToString(TestContext.DataRow["result"]);
            Vector res = new Vector(GetVectorFromString(result, row1, col1));

            Assert.IsTrue(AreEqual(B * A, res));
        }
    }
}
