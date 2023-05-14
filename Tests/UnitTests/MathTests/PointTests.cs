using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEngineV2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class PointTests
    {
        public TestContext TestContext { get; set; }

        public static float[] GetArrayFromString(string ptr, int size)
        {
            float[] res = new float[size];
            for (int i = 0; i < size; i++)
                res[i] = Convert.ToSingle(ptr.Split(' ')[i], CultureInfo.InvariantCulture.NumberFormat);
            return res;
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"D:\vs\GraphicEngineV2\Tests\Data\PointData\AddData.xml", "Point", DataAccessMethod.Sequential)]
        [TestMethod]
        public void AddTest()
        {
            string size = Convert.ToString(TestContext.DataRow["size"]);
            int row = int.Parse(size.Split()[0]);
            int col = int.Parse(size.Split()[1]);

            string vec = Convert.ToString(TestContext.DataRow["vector"]);
            Vector A = new Vector(VectorTest.GetVectorFromString(vec, row, col));

            string ptr = Convert.ToString(TestContext.DataRow["point"]);
            Point B = new Point(GetArrayFromString(ptr, row));

            string res = Convert.ToString(TestContext.DataRow["result"]);
            Point result = new Point(GetArrayFromString(res, row));

            Assert.IsTrue((B + A) == result);
        }

    }
}
