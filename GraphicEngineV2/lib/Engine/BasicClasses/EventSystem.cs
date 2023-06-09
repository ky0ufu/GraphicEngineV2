﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using GraphicEngineV2;


namespace Engine
{
    public class EventSystem
    {
        public delegate void EventDelegate(params dynamic[] args);
        public delegate void MoveDelegate(ref Camera camera, ref Game.Canvas canvas);


        public Dictionary<string, List<dynamic>> events = new Dictionary<string, List<dynamic>>();

        public EventSystem()
        {

        }

        public void Add(string eventName)
        {
            if(!events.ContainsKey(eventName))
            {
                events[eventName] = new List<dynamic>();
            }
        }

        public void Remove(string eventName) 
        {
            if(events.ContainsKey(eventName))
            {
                events.Remove(eventName);
            }
        }
        public void Handle(string eventName, dynamic function) 
        {
            Add(eventName);

            events[eventName].Add(function);
        }
        public dynamic Trigger(string eventName)
        {
            return events[eventName];
        }

        public List<dynamic> GetHandled(string eventName) 
        {
            if (events.ContainsKey(eventName))
            {
                return events[eventName];
            }
            throw new Exception();
        }

        public List<dynamic> this[string eventName]
        {
            get { return GetHandled(eventName); }
        }

    }
}
