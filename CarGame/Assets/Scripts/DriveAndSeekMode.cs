using UnityEngine;
using System.Collections;

public class DriveAndSeekMode : GameMode
{
    public enum DriveAndSeekPhases
    {
        SETUP = 0,
        HIDING = 1,
        SEEKING = 2,
        RESET = 3,
        FINISH = 4,
        Count
    }

    public DriveAndSeekPhases m_currentPhase;

    new
	void Start ()
    {
        base.Start();
        m_currentPhase = DriveAndSeekPhases.SETUP;
        InitializePhase();
	}
	
	new
	void Update ()
    {
        base.Start();
        UpdatePhase();
	}

    void InitializePhase()
    {
        switch (m_currentPhase)
        {
            case DriveAndSeekPhases.SETUP:
                {
                    EventManager.m_instance.AddEvent(Events.Event.DS_SETUP);
                    SetupHiderAndSeekers();

                    break;
                }
            case DriveAndSeekPhases.HIDING:
                {
                    EventManager.m_instance.AddEvent(Events.Event.DS_HIDING);
                    break;
                }
            case DriveAndSeekPhases.SEEKING:
                {
                    EventManager.m_instance.AddEvent(Events.Event.DS_SEEKING);
                    break;
                }
            case DriveAndSeekPhases.RESET:
                {
                    EventManager.m_instance.AddEvent(Events.Event.DS_RESET);
                    break;
                }
            case DriveAndSeekPhases.FINISH:
                {
                    EventManager.m_instance.AddEvent(Events.Event.DS_FINISH);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void UpdatePhase()
    {
        switch (m_currentPhase)
        {
            case DriveAndSeekPhases.SETUP:
                {
                    break;
                }
            case DriveAndSeekPhases.HIDING:
                {
                    break;
                }
            case DriveAndSeekPhases.SEEKING:
                {
                    break;
                }
            case DriveAndSeekPhases.RESET:
                {
                    break;
                }
            case DriveAndSeekPhases.FINISH:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void SetupHiderAndSeekers()
    {
        int HiderNumber = Random.Range(1, PlayerManager.m_instance.m_numberOfCars);
        PlayerManager.m_instance.m_playerCars[HiderNumber - 1].GetComponent<Car>().SetHider();
        string HiderTag = PlayerManager.m_instance.m_playerCars[HiderNumber - 1].transform.tag;
        for(int iter = 1; iter <= PlayerManager.m_instance.m_numberOfCars; iter++)
        {
            if(iter != HiderNumber)
            {
                PlayerManager.m_instance.m_playerCars[iter - 1].GetComponent<Car>().SetSeeker(HiderTag);
            }
        }
        /* Turn off Cams */
    }
}
