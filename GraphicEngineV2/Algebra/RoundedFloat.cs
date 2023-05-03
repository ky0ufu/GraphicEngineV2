using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class RoundedFloat
    {
        private static int n = 3;

        private float value;

        public RoundedFloat(float data)
        {

            this.value = (float)Math.Round(data, n);
        }

        public override string ToString()
        {
            return value.ToString();
        }
        public void SetFloat(float value)
        {
            value = (float)Math.Round(value, n);
        }
        public double GetFloat(float value) { return value; }

        public static implicit operator float(RoundedFloat value) 
        {
            return value.value;
        }

    }
}
