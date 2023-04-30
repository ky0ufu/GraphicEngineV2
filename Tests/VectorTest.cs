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
        public bool AreEqual(Vector vec1, Vector vec2)
        {
            if (vec1.IsTranspose != vec2.IsTranspose)
                return false;
            if (vec1.VectorSize() != vec2.VectorSize())
                return false;
            for (int i = 0; i < vec1.VectorSize(); i++)
            {
                if (vec1.IsTranspose)
                {
                    if (vec1.data[0, i] - vec2.data[0, i] > 0.00001)
                        return false;
                }
                else
                    if (vec1.data[i, 0] - vec2.data[i, 0] > 0.00001)
                    return false;
            }
            return true;
        }
        public bool AreEqual(float a, float b)
        {
            return (Math.Abs(a - b) < 0.000001);
        }

        public float[,] GetVectorFromString(string matrix, int rows, int cols)
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
    }
}
