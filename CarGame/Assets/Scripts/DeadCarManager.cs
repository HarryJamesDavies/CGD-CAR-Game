﻿using UnityEngine;
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
    public List<GameObject> m_prefabCars;
    public List<GameObject> m_sections;
    public List<Rect> m_sectionBoundaries;
    public List<int> m_sectionsToActivate;
    public SectioningMode m_mode;

    public List<GameObject> m_cars = new List<GameObject>();


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

        EventManager.m_instance.SubscribeToEvent(Events.Event.DS_SETUP, EvFunc_OnDSSetup);
        EventManager.m_instance.SubscribeToEvent(Events.Event.DS_HIDING, EvFunc_OnDSHiding);
        EventManager.m_instance.SubscribeToEvent(Events.Event.DS_SEEKING, EvFunc_OnDSSeeking);
        EventManager.m_instance.SubscribeToEvent(Events.Event.DS_CHASE, EvFunc_OnDSChase);
        EventManager.m_instance.SubscribeToEvent(Events.Event.DS_RESET, EvFunc_OnDSReset);
        EventManager.m_instance.SubscribeToEvent(Events.Event.DS_FINISH, EvFunc_OnDSFinish);

        foreach (GameObject sections in m_sections)
        {
            Vector2 sectionPosition = new Vector2(sections.transform.position.x - 50.0f, sections.transform.position.z - 50.0f);
            Rect tempRect = new Rect(sectionPosition, new Vector2(100.0f, 100.0f));
            m_sectionBoundaries.Add(tempRect);
        }
    }

    void EvFunc_OnDSSetup()
    {
        SetSections();
        return;
    }

    void EvFunc_OnDSHiding()
    {
        
        return;
    }

    void EvFunc_OnDSSeeking()
    {
        
        return;
    }

    void EvFunc_OnDSChase()
    {

        return;
    }


    void EvFunc_OnDSReset()
    {
        ResetSections();
        return;
    }

    void EvFunc_OnDSFinish()
    {
        
        return;
    }

    void SetSections()
    {
        switch (m_mode)
        {
            case SectioningMode.SQUARE:
                {
                    int index = 0;
                    foreach (Rect sections in m_sectionBoundaries)
                    {
                        if (GameModeManager.m_instance.m_currentGameMode.m_eventRect.Overlaps(sections))
                        {
                            SpawnCars(index);
                        }
                        index++;
                    }
                    break;
                }
            case SectioningMode.SPECIFIC:
                {
                    foreach (int index in m_sectionsToActivate)
                    {
                        SpawnCars(index - 1);
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
        if (m_cars.Count != 0)
        {
            for (int iter = 0; iter <= m_cars.Count - 1; iter++)
            {
                Destroy(m_cars[iter].gameObject);
            }
            m_cars.Clear();
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

    void SpawnCars(int _section)
    {
        for (int iter = 0; iter <= m_sections[_section].transform.childCount - 1; iter++)
        {
            Transform spawnLocation = m_sections[_section].transform.GetChild(iter).transform;
            m_cars.Add((GameObject)Instantiate(m_prefabCars[GetRandomCar()], spawnLocation.position, spawnLocation.rotation));
        }
    }

    int GetRandomCar()
    {
        return Random.Range(0, m_prefabCars.Count);
    }
}
