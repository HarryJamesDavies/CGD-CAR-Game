using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

    public List<EventList> m_eventList = new List<EventList>();
    public List<Events.Event> m_eventBuffer = new List<Events.Event>();
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
        if(m_tempBuffer != null)
        {
            foreach (Events.Event events in m_tempBuffer)
            {
                m_eventBuffer.Add(events);
            }

            m_tempBuffer.Clear();
        }
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        BufferHandler();
    }

    void BufferHandler()
    {
        foreach(Events.Event iterEvent in m_eventBuffer)
        {
            TriggerEvent((int)iterEvent);
        }

        if (m_eventBuffer != null)
        {
            m_eventBuffer.Clear();
        }
    }

    void TriggerEvent(int _index)
    {
        if (m_eventList[_index].m_event != null)
        {
            m_eventList[_index].m_event();
        }
    }

    public void AddEvent(Events.Event _event)
    {
        m_tempBuffer.Add(_event);
    }

    public void SubscribeToEvent(Events.Event _event, EventTrigger _trigger)
    {
        EventList tempList = m_eventList[(int)_event];
        tempList.m_event += _trigger;
        m_eventList[(int)_event] = tempList;
    }

    public void UnsubscribeToEvent(Events.Event _event, EventTrigger _trigger)
    {
        EventList tempList = m_eventList[(int)_event];
        tempList.m_event -= _trigger;
        m_eventList[(int)_event] = tempList;
    }
}
