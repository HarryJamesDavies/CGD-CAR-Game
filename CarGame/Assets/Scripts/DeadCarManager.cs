using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeadCarManager : MonoBehaviour {

    public static DeadCarManager m_instance = null;
    public List<GameObject> m_sections;
    public float m_minimumDistance;

    // Use this for initialization
    void Start ()
    {
        if (m_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }

        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            m_sections.Add(transform.GetChild(i).gameObject);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(EventManager.m_instance.m_stateChanged)
        {
            OnEventEnd();

            OnEventBegin();
        }

        OnEventUpdate();
	}

    public void OnEventBegin()
    {
        switch (EventManager.m_instance.m_currentState)
        {
            case EventManager.Events.FREEROAM:
                {
                    break;
                }
            case EventManager.Events.DRIVEANDSEEK:
                {
                    SetSections();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void OnEventUpdate()
    {
        switch (EventManager.m_instance.m_currentState)
        {
            case EventManager.Events.FREEROAM:
                {
                    break;
                }
            case EventManager.Events.DRIVEANDSEEK:
                {

                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void OnEventEnd()
    {
        switch (EventManager.m_instance.m_prevState)
        {
            case EventManager.Events.FREEROAM:
                {
                    break;
                }
            case EventManager.Events.DRIVEANDSEEK:
                {
                    ResetSections();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void SetSections()
    {
        foreach(GameObject sections in m_sections)
        {
            if(m_minimumDistance >= Vector3.Distance(EventManager.m_instance.m_eventLocation, sections.transform.position))
            {
                sections.gameObject.SetActive(true);
            }
        }
    }

    void ResetSections()
    {
        foreach (GameObject sections in m_sections)
        {
            sections.gameObject.SetActive(false);
        }
    }

    /*
        if (Input.GetKeyDown("e") || Input.GetButtonDown("P" + _controller + ("-Circle(PS4)")))
        {
            EventManager.m_instance.m_eventLocation = this.gameObject.transform.position;
            EventManager.m_instance.m_currentState = EventManager.Events.DRIVEANDSEEK;
        }

        if (Input.GetKeyDown("r") || Input.GetButtonDown("P" + _controller + ("-Circle(PS4)")))
        {
            EventManager.m_instance.m_currentState = EventManager.Events.FREEROAM;
        }
    */
}
