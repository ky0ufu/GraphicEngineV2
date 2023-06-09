using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using GraphicEngineV2;


namespace Engine
{
    public class EventSystem
    {
        public delegate dynamic EventDelegate(params dynamic[] args);


        public Dictionary<string, List<EventDelegate>> events = new Dictionary<string, List<EventDelegate>>();

        public EventSystem()
        {

        }

        public void Add(string eventName)
        {
            if(!events.ContainsKey(eventName))
            {
                events[eventName] = new List<EventDelegate>();
            }
        }

        public void Remove(string eventName) 
        {
            if(events.ContainsKey(eventName))
            {
                events.Remove(eventName);
            }
        }
        public void Handle(string eventName, EventDelegate function) 
        {
            Add(eventName);

            events[eventName].Add(function);
        }
        public void Trigger(string eventName, params dynamic[] args)
        {
            if(events.ContainsKey(eventName)) 
            {
                foreach(var evt in events[eventName])
                {
                    evt(args);
                }
            }
        }

        public List<EventDelegate> GetHandled(string eventName) 
        {
            if (events.ContainsKey(eventName))
            {
                return events[eventName];
            }
            throw new Exception();
        }

        public List<EventDelegate> this[string eventName]
        {
            get { return GetHandled(eventName); }
        }

    }
}
