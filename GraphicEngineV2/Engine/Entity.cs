using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEngineV2;


namespace Engine
{   
    public class Entity
    {
        public CoordinateSystem CS { get; set; }
        public Identifier Id { get; }
        public Dictionary<string, dynamic> properties = new Dictionary<string, dynamic>();

        public Entity(CoordinateSystem cS)
        {
           CS = cS;
            Id = new Identifier();
            properties.Add("properties", new HashSet<dynamic>());
        }
        public void SetProperty(string prop, dynamic value)
        {
            if (prop == "properties")
                throw new Exception();

            if (!properties.ContainsKey(prop))
                properties.Add(prop, value);
            else
                properties[prop] = value;

            properties["properties"].Add(prop);
        }

        public dynamic GetProperty(string prop = null) 
        {
            return properties["properties"].ContainsKey(prop) ? properties[prop] : null;
        }

        public void RemoveProperty(string prop) 
        {
            if (prop == "properties")
                throw new Exception();

            properties.Remove(prop);
            properties["properties"].Remove(prop);
        }

        public float this[string prop]
        {
            get 
            {
                if (prop == "properties")
                    throw new Exception();
                return properties[prop]; 
            }
            set
            {
                properties[prop] = value;
                properties["properties"].Add(value);
            }
        }
    }
}
