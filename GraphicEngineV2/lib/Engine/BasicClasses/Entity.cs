﻿using System;
using System.Collections.Generic;
using GraphicEngineV2;


namespace Engine
{   
    public class Entity
    {
        public CoordinateSystem CoordSystem { get; set; }
        public Identifier Id { get; }
        public Dictionary<string, dynamic> properties = new Dictionary<string, dynamic>();

        public Entity(CoordinateSystem cS) : this()
        {
            CoordSystem = cS;
        }
        protected Entity()
        {
            Id = new Identifier();
            properties.Add("properties", new HashSet<dynamic>());
        }

        public void SetProperty(string prop, dynamic value)
        {
            if (prop == "properties")
                throw new Exception();

            if (!properties.ContainsKey(prop))
            {
                properties.Add(prop, value);
                properties["properties"].Add(prop);
            }
            else
            {
                properties[prop] = value;
            }
        }

        public dynamic GetProperty(string prop = null) 
        {
            return properties.ContainsKey(prop) ? properties[prop] : null;
        }

        public void RemoveProperty(string prop) 
        {
            if (prop == "properties")
                throw new Exception();

            properties.Remove(prop);
            properties["properties"].Remove(prop);
        }

        public virtual float IntersectionDistance(Ray ray)
        {
            return float.PositiveInfinity;
        }

        public dynamic this[string prop]
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
