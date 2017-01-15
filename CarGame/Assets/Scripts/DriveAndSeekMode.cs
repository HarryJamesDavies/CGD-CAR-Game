using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DriveAndSeekMode : GameMode
{
    public enum DriveAndSeekPhases
    {
        SETUP = 0,
        HIDING = 1,
        SEEKING = 2,
        CHASE = 3,  
        RESET = 4,
        FINISH = 5,
        Count
    }

    [Serializable]
    public struct PhaseLenght
    {
        public string m_name;
        public float m_length;
    }

    public DriveAndSeekPhases m_currentPhase;
    public int m_winningScore = 3;
    public int m_hiderNumber = 0;
    private int m_roundWinner = -1;
    private int m_gameWinner = -1;

    public List<PhaseLenght> m_phaseLenghts;
    public List<Timer> m_timers;
    public List<int> m_playerScores;

    public Transform m_hiderSpawn;
    public List<Transform> m_seekerSpawns;

    private Transform m_timerHolder;
    public GameObject m_timerPrefab;

    new
	void Start ()
    {
        base.Start();

        m_timerHolder = new GameObject("TimerHolder").transform;
        m_timerHolder.transform.SetParent(transform);

        for(int iter = 0; iter <= m_phaseLenghts.Count - 1; iter++)
        {
            GameObject tempTimer = (GameObject)Instantiate(m_timerPrefab);
            tempTimer.transform.SetParent(m_timerHolder);
            tempTimer.GetComponent<Timer>().SetTimer(m_phaseLenghts[iter].m_name, m_phaseLenghts[iter].m_length);
            m_timers.Add(tempTimer.GetComponent<Timer>());
        }

        for(int iter = 1; iter <= PlayerManager.m_instance.m_numberOfCars; iter++)
        {
            m_playerScores.Add(0);
        }

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
                    m_currentPhase = DriveAndSeekPhases.HIDING;
                    InitializePhase();
                    break;
                }
            case DriveAndSeekPhases.HIDING:
                {
                    EventManager.m_instance.AddEvent(Events.Event.DS_HIDING);
                    m_timers[GetTimer("Hide")].StartTimer();
                    break;
                }
            case DriveAndSeekPhases.SEEKING:
                {
                    EventManager.m_instance.AddEvent(Events.Event.DS_SEEKING);
                    m_timers[GetTimer("Seek")].StartTimer();
                    break;
                }
            case DriveAndSeekPhases.CHASE:
                {
                    EventManager.m_instance.AddEvent(Events.Event.DS_CHASE);
                    PlayerManager.m_instance.m_playerCars[m_hiderNumber].GetComponent<Car>().ToggleCamera(true);
                    m_timers[GetTimer("Chase")].StartTimer();
                    break;
                }
            case DriveAndSeekPhases.RESET:
                {
                    EventManager.m_instance.AddEvent(Events.Event.DS_RESET);
                    m_playerScores[m_roundWinner]++;
                    if (CheckFinished())
                    {
                        m_currentPhase = DriveAndSeekPhases.FINISH;
                        InitializePhase();
                    }
                    else
                    {
                        ResetRound();
                        m_currentPhase = DriveAndSeekPhases.SETUP;
                        InitializePhase();
                    }
                    break;
                }
            case DriveAndSeekPhases.FINISH:
                {
                    EventManager.m_instance.AddEvent(Events.Event.DS_FINISH);
                    Debug.Log("Player " + m_gameWinner + " Wins!");
                    DestroyObject(gameObject);
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
                    if(m_timers[GetTimer("Hide")].CheckFinished())
                    {
                        PlayerManager.m_instance.m_playerCars[m_hiderNumber].GetComponent<Car>().ToggleCamera(false);
                        m_currentPhase = DriveAndSeekPhases.SEEKING;
                        InitializePhase();
                    }
                    break;
                }
            case DriveAndSeekPhases.SEEKING:
                {
                    if(CheckHidersCaught())
                    {
                        m_currentPhase = DriveAndSeekPhases.RESET;
                        InitializePhase();
                    }

                    if(m_timers[GetTimer("Seek")].CheckFinished())
                    {
                        m_currentPhase = DriveAndSeekPhases.CHASE;
                        InitializePhase();
                    }
                    break;
                }
            case DriveAndSeekPhases.CHASE:
                {
                    if (CheckHidersCaught())
                    {
                        m_currentPhase = DriveAndSeekPhases.RESET;
                        InitializePhase();
                    }

                    if (m_timers[GetTimer("Chase")].CheckFinished())
                    {
                        m_roundWinner = m_hiderNumber;
                        m_currentPhase = DriveAndSeekPhases.RESET;
                        InitializePhase();
                    }
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

    int GetTimer(string _name)
    {
        for(int iter = 0; iter <= m_timers.Count - 1; iter++)
        {
            if(m_timers[iter].m_name == _name)
            {
                return iter;
            }
        }
        return -1;
    }

    void SetupHiderAndSeekers()
    {
        m_hiderNumber = UnityEngine.Random.Range(1, PlayerManager.m_instance.m_numberOfCars + 1) - 1;
        PlayerManager.m_instance.m_playerCars[m_hiderNumber].GetComponent<Car>().SetHider();
        string HiderTag = PlayerManager.m_instance.m_playerCars[m_hiderNumber].transform.tag;
        PlayerManager.m_instance.m_playerCars[m_hiderNumber].transform.position = m_hiderSpawn.position;
        PlayerManager.m_instance.m_playerCars[m_hiderNumber].transform.rotation = m_hiderSpawn.rotation;

        for (int iter = 1; iter <= PlayerManager.m_instance.m_numberOfCars; iter++)
        {
            if(iter != m_hiderNumber + 1)
            {
                PlayerManager.m_instance.m_playerCars[iter - 1].GetComponent<Car>().SetSeeker(HiderTag);
                PlayerManager.m_instance.m_playerCars[iter - 1].transform.position = m_seekerSpawns[iter - 1].position;
                PlayerManager.m_instance.m_playerCars[iter - 1].transform.rotation = m_seekerSpawns[iter - 1].rotation;
            }
        }
    }

    bool CheckHidersCaught()
    {
        return false;
    }

    bool CheckFinished()
    {
        for(int iter = 0; iter <= m_playerScores.Count - 1; iter++)
        {
            if(m_playerScores[iter] == m_winningScore)
            {
                m_gameWinner = iter + 1;
                return true;
            }
        }
        return false;
    }

    void ResetRound()
    {
        foreach(Timer timer in m_timers)
        {
            timer.ResetTimer();
        }

        foreach(GameObject player in PlayerManager.m_instance.m_playerCars)
        {
            player.GetComponent<Car>().ResetMode();
        }
    }
}
