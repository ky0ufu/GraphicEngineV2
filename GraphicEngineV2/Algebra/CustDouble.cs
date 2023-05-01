using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class CustDouble
    {
        private static int n = 3;

        private double data;

        public CustDouble(double data)
        {

            this.data = Math.Round(data, n);
        }

        public override string ToString()
        {
            return data.ToString();
        }
        public void SetFloat(float value)
        {
            data = Math.Round(value, n);
        }
        public double GetFloat(float value) { return data; }

    }
}
