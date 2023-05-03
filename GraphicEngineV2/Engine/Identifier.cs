using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Engine
{
    public class Identifier
    {
        static private HashSet<string> identifiers = new HashSet<string>();
        public string Value { get; }
        public Identifier()
        {
            do
                Value = GenerateIdentifier();
            while (identifiers.Contains(Value));
            identifiers.Add(Value);
        }

        private static string GenerateIdentifier()
        {
            return Convert.ToString(Guid.NewGuid().ToString());      
        }

        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
