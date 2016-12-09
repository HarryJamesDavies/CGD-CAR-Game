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

    public Events m_currentState = Events.FREEROAM;
    public Events m_prevState = Events.FREEROAM;
    public bool m_stateChanged = false;

    public Vector3 m_eventLocation;

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

        if (m_currentState != m_prevState)
        {
            m_stateChanged = true;
            m_prevState = m_currentState;
        }
	}
}
