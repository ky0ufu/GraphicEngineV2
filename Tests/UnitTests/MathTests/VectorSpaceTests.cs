using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEngineV2;
using System.Globalization;

namespace Tests
{
    [TestClass]
    public class VectorSpaceTests
    {

        public TestContext TestContext { get; set; }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\VectorSpaceData\ScalarProductData.xml", "dat", DataAccessMethod.Sequential)]
        [TestMethod]
        public void ScalarProduct()
        {
            float[,] vector1 = { { 1 }, { 1 }, { 2 } };
            float[,] vector2 = { { 2 }, { 1 }, { 1 } };
            float[,] vector3 = { { 1 }, { 2 }, { 1 } };
            Vector first = new Vector(vector1);
            Vector second = new Vector(vector2);
            Vector third = new Vector(vector3);
            Vector[] basis = { first, second, third };
            VectorSpace space = new VectorSpace(basis);

            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string vec1 = Convert.ToString(TestContext.DataRow["vector1"]);
            string vec2 = Convert.ToString(TestContext.DataRow["vector2"]);

            Vector A = new Vector(VectorTest.GetVectorFromString(vec1, row, col));
            Vector B = new Vector(VectorTest.GetVectorFromString(vec2, row, col));

            float result = float.Parse(Convert.ToString(TestContext.DataRow["result"]), CultureInfo.InvariantCulture.NumberFormat);
            float scalarProduct = space.ScalarProduct(A, B);

            Assert.AreEqual(result, scalarProduct);
        }



    }
}
