using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeadCarManager : MonoBehaviour
{
    public enum SectioningMode
    {
        SQUARE = 0,
        SPECIFIC = 1,
        Length
    };

    public static DeadCarManager m_instance = null;
    public List<GameObject> m_sections;
    public List<Rect> m_sectionBoundaries;
    public List<int> m_sectionsToActivate;
    public SectioningMode m_mode;


    // Use this for initialization
    void Start()
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

        foreach (GameObject sections in m_sections)
        {
            Vector2 sectionPosition = new Vector2(sections.transform.position.x - 50.0f, sections.transform.position.z - 50.0f);
            Rect tempRect = new Rect(sectionPosition, new Vector2(100.0f, 100.0f));
            m_sectionBoundaries.Add(tempRect);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EventManager.m_instance.m_stateChanged)
        {
            OnEventEnd();

            OnEventBegin();
        }

        OnEventUpdate();
    }

    public void OnEventBegin()
    {
        switch (EventManager.m_instance.m_currentEvent)
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
        switch (EventManager.m_instance.m_currentEvent)
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
        switch (EventManager.m_instance.m_prevEvent)
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
        switch (m_mode)
        {
            case SectioningMode.SQUARE:
                {
                    Vector2 eventMinimum = new Vector2(EventManager.m_instance.m_eventLocation.x - EventManager.m_instance.m_eventWidth / 2,
                        EventManager.m_instance.m_eventLocation.z - EventManager.m_instance.m_eventHeight / 2);
                    Rect eventRect = new Rect(eventMinimum, new Vector2(EventManager.m_instance.m_eventWidth, EventManager.m_instance.m_eventHeight));

                    int index = 0;
                    foreach (Rect sections in m_sectionBoundaries)
                    {
                        if (eventRect.Overlaps(sections))
                        {
                            m_sections[index].SetActive(true);
                        }
                        index++;
                    }
                    break;
                }
            case SectioningMode.SPECIFIC:
                {
                    foreach (int index in m_sectionsToActivate)
                    {
                        m_sections[index - 1].SetActive(true);
                    }

                    m_sectionsToActivate.Clear();
                    break;
                }
            default:
                {
                    break;
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

    public void AddSectionIndex(int _index)
    {
        m_sectionsToActivate.Add(_index);
    }

    public void AddSectionIndex(List<int> _indicies)
    {
        foreach (int index in _indicies)
        {
            m_sectionsToActivate.Add(index);
        }
    }

}
