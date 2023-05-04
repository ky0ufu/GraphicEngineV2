using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class EntitiesList
    {
        public List<Entity> Entities { get; }

        public EntitiesList(List<Entity> entities) 
        {
            Entities = entities;
        }

        public EntitiesList() 
        { 
            Entities = new List<Entity>();
        }
        
        public void AppendEntity(Entity entity)
        {
            Entities.Add(entity);
        }

        public void RemoveEntity(Entity entity) 
        {
            Entities.Remove(entity);
        }

        public Entity GetEntity(Identifier id)
        { 
            foreach(Entity entity in Entities) 
            {
                if (entity.Id == id)
                    return entity;
            }
            throw new Exception();
        }
        public Entity this[Identifier id]
        {
            get 
            { 
                return GetEntity(id);
            }
        }
        public delegate void EntityDelegate(Entity entity);
        public void exec(EntityDelegate operation, Entity entity)
        {
            operation(entity);
        }
    }
}
