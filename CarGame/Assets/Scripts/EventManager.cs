using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    public static EventManager m_instance = null;

    public enum Events
    {
        FREEROAM = 0,
        DRIVEANDSEEK = 1,
        Length
    };

    public Events m_currentEvent = Events.FREEROAM;
    public Events m_prevEvent = Events.FREEROAM;
    private Events m_floatingEvent = Events.FREEROAM;
    public bool m_stateChanged = false;

    public Vector3 m_eventLocation;
    public float m_eventWidth;
    public float m_eventHeight;

    // Use this for initialization
    void Start ()
    {
	    if(m_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
	}

    // Update is called once per frame
    void LateUpdate ()
    {
        m_stateChanged = false;

        if (m_currentEvent != m_floatingEvent)
        {
            m_stateChanged = true;
            m_prevEvent = m_floatingEvent;
            m_floatingEvent = m_currentEvent;
        }
	}
}
