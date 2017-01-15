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
        int HiderNumber = Random.Range(0, PlayerManager.m_instance.m_numberOfCars);
        /* Link Stuff */
        /* Turn off Cams */
    }
}
