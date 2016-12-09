using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeadCarManager : MonoBehaviour
{
    public enum SectioningMode
    {
        GRID = 0,
        SQUARE = 1,
        SPECIFIC = 2,
        Length
    };

    public static DeadCarManager m_instance = null;
    public List<GameObject> m_sections;
    public List<Rect> m_sectionBoundaries;
    public List<int> m_sectionsToActivate;
    public SectioningMode m_mode;

    public int m_gridWidth;
    public int m_gridHeight;

    public int m_gridBorderWidth;
    public int m_gridBorderHeight;


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
            case SectioningMode.GRID:
                {
                    int centerIndex = 0;
                    Vector2 eventLocation = new Vector2(EventManager.m_instance.m_eventLocation.x, EventManager.m_instance.m_eventLocation.z);

                    int index = 0;
                    foreach (Rect sections in m_sectionBoundaries)
                    {
                        if (sections.Contains(eventLocation))
                        {
                            centerIndex = index;
                        }
                        index++;
                    }

                    Vector2 centerPos = GetSectionPos(centerIndex);

                    for (int y = ((int)centerPos.y - m_gridBorderHeight / 2); y >= ((int)centerPos.y + m_gridBorderHeight / 2); y++)
                    {
                        for (int x = ((int)centerPos.x - m_gridBorderWidth / 2); x >= ((int)centerPos.x + m_gridBorderWidth / 2); x++)
                        {
                            m_sections[GetSectionIndex(new Vector2(x, y)) - 1].SetActive(true);
                        }
                    }

                    break;
                }
            case SectioningMode.SQUARE:
                {
                    Vector2 eventMinimum = new Vector2(EventManager.m_instance.m_eventLocation.x - EventManager.m_instance.m_eventWidth / 2,
                        EventManager.m_instance.m_eventLocation.z - EventManager.m_instance.m_eventHeight / 2);
                    Rect eventRect = new Rect(eventMinimum, new Vector2(EventManager.m_instance.m_eventWidth, EventManager.m_instance.m_eventHeight));

                    int index = 1;
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

    Vector2 GetSectionPos(int index)
    {
        Vector2 centerPos;

        centerPos.x = index % m_gridHeight;
        centerPos.y = index / m_gridHeight;

        return centerPos;
    }

    int GetSectionIndex(Vector2 _pos)
    {
        int index;

        index = ((int)_pos.y * m_gridBorderWidth) + (int)_pos.x;

        return index;
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
