/*
 * Acts as a middle man for handling event calls.
 * Functions can be subscribed to event call list 
 * to be triggered when the event is called.
 * 
 * Created by: Harry Davies, Half-Full Games, 31/01/2017
 */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HF
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager m_instance;
        public delegate void EventTrigger();

        [Serializable]
        public struct EventList
        {
            public EventTrigger m_event;
            public Events.Event m_eventType;
        }

        //Holds a list of tracked events
        private List<EventList> m_eventList = new List<EventList>();
        //Holds any events pushed to the EventManager this frame
        private List<Events.Event> m_eventBuffer = new List<Events.Event>();
        //Acts as an intermediate when resolving event calls
        private List<Events.Event> m_tempBuffer = new List<Events.Event>();

        void Awake()
        {
            if (m_instance)
            {
                Destroy(this.gameObject);
            }
            else
            {
                m_instance = this;
            }

            //Adds a call list per Event in Events enum
            for (int i = 0; i <= (int)Events.Event.Count - 1; i++)
            {
                EventList tempList;
                tempList.m_eventType = (Events.Event)i;
                tempList.m_event = null;
                m_eventList.Add(tempList);
            }
        }

        void Update()
        {
            //Moves all events from tempBuffer to be resolved
            if (m_tempBuffer != null)
            {
                foreach (Events.Event events in m_tempBuffer)
                {
                    m_eventBuffer.Add(events);
                }

                m_tempBuffer.Clear();
            }
        }

        void LateUpdate()
        {
            BufferHandler();
        }

        /// <summary>Triggers all events pushed to EventBuffer this frame </summary>
        void BufferHandler()
        {
            //Triggers events pushed to EventBuffer
            foreach (Events.Event iterEvent in m_eventBuffer)
            {
                TriggerEvent((int)iterEvent);
            }

            //Clears the buffer of triggered evennts
            if (m_eventBuffer != null)
            {
                m_eventBuffer.Clear();
            }
        }

        /// <summary>Calls all functions subscribe to this event </summary>
        void TriggerEvent(int _index)
        {
            if (m_eventList[_index].m_event != null)
            {
                m_eventList[_index].m_event();
            }
        }

        /// <summary>Adds event call to EventBuffer for processing in LateUpdate </summary>
        public void AddEvent(Events.Event _event)
        {
            m_tempBuffer.Add(_event);
        }

        /// <summary>Adds function to call list when this event is triggered </summary>
        public void SubscribeToEvent(Events.Event _event, EventTrigger _trigger)
        {
            EventList tempList = m_eventList[(int)_event];
            tempList.m_event += _trigger;
            m_eventList[(int)_event] = tempList;
        }

        /// <summary>Removes function from call list of this event </summary>
        public void UnsubscribeToEvent(Events.Event _event, EventTrigger _trigger)
        {
            EventList tempList = m_eventList[(int)_event];
            tempList.m_event -= _trigger;
            m_eventList[(int)_event] = tempList;
        }
    }
}
