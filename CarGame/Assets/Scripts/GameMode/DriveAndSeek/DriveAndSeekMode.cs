﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace HF
{
    public class DriveAndSeekMode : GameMode
    {
        public enum DriveAndSeekPhases
        {
            SETUP = 0,
            RUNNING = 1,
            CHASING = 3,
            RESET = 4,
            FINISH = 5,
            BUFFER = 6,
            INACTIVE = 7,
            Count
        }

        [Serializable]
        public struct PhaseLenght
        {
            public string m_name;
            public float m_length;
        }

        public struct BufferStruct
        {
            public DriveAndSeekPhases m_nextPhase;
            public float m_lenght;
            public string m_message;
        }

        public DriveAndSeekPhases m_currentPhase;

        [HideInInspector]
        public List<int> m_playerScores;
        public int m_winningScore = 3;
        private int m_hiderNumber = 0;
        private int m_gameWinner = -1;
        private bool m_hiderWon = false;

        public List<PhaseLenght> m_phaseLenghts;
        private BufferStruct m_bufferPhase;   

        [HideInInspector]
        public List<Timer> m_timers;
        public GameObject m_timerPrefab;
        private Transform m_timerHolder;

        private GameObject m_infoText;

        public Transform m_hiderSpawn;
        public List<Transform> m_seekerSpawns;

        new
        void Start()
        {
            base.Start();

            m_timerHolder = new GameObject("TimerHolder").transform;
            m_timerHolder.transform.SetParent(transform);

            for (int iter = 0; iter <= m_phaseLenghts.Count - 1; iter++)
            {
                GameObject tempTimer = (GameObject)Instantiate(m_timerPrefab);
                tempTimer.transform.SetParent(m_timerHolder);
                tempTimer.GetComponent<Timer>().SetTimer(m_phaseLenghts[iter].m_name, m_phaseLenghts[iter].m_length);
                m_timers.Add(tempTimer.GetComponent<Timer>());
            }

            for (int iter = 1; iter <= 4; iter++)
            {
                m_playerScores.Add(0);
            }

            m_infoText = GameObject.FindGameObjectWithTag("DaSText");

            m_currentPhase = DriveAndSeekPhases.INACTIVE;
            m_mode = GameModeManager.GameModeState.DRIVEANDSEEK;
        }

        new
        void Update()
        {
            base.Update();
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

                        //Setup buffer phase
                        m_currentPhase = DriveAndSeekPhases.BUFFER;
                        m_bufferPhase.m_lenght = 5.0f;
                        m_bufferPhase.m_nextPhase = DriveAndSeekPhases.RUNNING;
                        m_bufferPhase.m_message = "Runner Get Ready!";
                        InitializePhase();
                        break;
                    }
                case DriveAndSeekPhases.RUNNING:
                    {
                        EventManager.m_instance.AddEvent(Events.Event.DS_RUNNING);
                        m_infoText.GetComponent<Text>().text = "Start Running!";
                        m_timers[GetTimer("Running")].StartTimer();

                        ChangeAllPlayerMovement(false);
                        ChangePlayerMovement(m_hiderNumber, true);
                        break;
                    }
                case DriveAndSeekPhases.CHASING:
                    {
                        EventManager.m_instance.AddEvent(Events.Event.DS_CHASING);

                        m_infoText.GetComponent<Text>().text = "Catch the Runner!";

                        ChangeAllPlayerMovement(true);

                        m_timers[GetTimer("Chasing")].StartTimer();
                        break;
                    }
                case DriveAndSeekPhases.RESET:
                    {
                        EventManager.m_instance.AddEvent(Events.Event.DS_RESET);

                        if (m_hiderWon)
                        {
                            m_playerScores[m_hiderNumber]++;
                        }
                        else
                        {
                            for (int iter = 0; iter <= PlayerManager.m_instance.m_playerCars.Count - 1; iter++)
                            {
                                if (iter != m_hiderNumber)
                                {
                                    m_playerScores[iter]++;
                                }
                            }
                        }

                        for (int iter = 0; iter <= PlayerManager.m_instance.m_playerCars.Count - 1; iter++)
                        {
                            PlayerManager.m_instance.m_playerCars[iter].GetComponent<Car>().ToggleCamera(true);
                        }

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

                        for (int iter = 0; iter <= m_playerScores.Count - 1; iter++)
                        {
                            m_playerScores[iter] = 0;
                        }

                        ResetRound();

                        m_active = false;
                        GameModeManager.m_instance.m_currentEvent = GameModeManager.GameModeState.FREEROAM;

                        //Setup buffer phase
                        m_currentPhase = DriveAndSeekPhases.BUFFER;
                        m_bufferPhase.m_lenght = 5.0f;
                        m_bufferPhase.m_nextPhase = DriveAndSeekPhases.INACTIVE;
                        m_bufferPhase.m_message = "Player " + m_gameWinner + " Wins!";
                        InitializePhase();

                        break;
                    }
                case DriveAndSeekPhases.BUFFER:
                    {
                        EventManager.m_instance.AddEvent(Events.Event.DS_BUFFER);

                        ChangeAllPlayerMovement(false);

                        m_timers[GetTimer("Buffer")].m_timerLength = m_bufferPhase.m_lenght;
                        m_infoText.GetComponent<Text>().text = m_bufferPhase.m_message;
                        m_timers[GetTimer("Buffer")].StartTimer();
                        break;
                    }
                case DriveAndSeekPhases.INACTIVE:
                    {
                        m_infoText.GetComponent<Text>().text = "";
                        ChangeAllPlayerMovement(true);
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
                case DriveAndSeekPhases.RUNNING:
                    {
                        if (m_timers[GetTimer("Running")].CheckFinished())
                        {
                            m_currentPhase = DriveAndSeekPhases.CHASING;
                            InitializePhase();
                        }
                        break;
                    }
                case DriveAndSeekPhases.CHASING:
                    {
                        if (CheckHidersCaught())
                        {
                            m_currentPhase = DriveAndSeekPhases.RESET;
                            InitializePhase();
                        }

                        if (CheckSeekersDead())
                        {
                            m_hiderWon = true;
                            m_currentPhase = DriveAndSeekPhases.RESET;
                            InitializePhase();
                        }

                        if (m_timers[GetTimer("Chasing")].CheckFinished())
                        {
                            m_hiderWon = true;
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
                case DriveAndSeekPhases.BUFFER:
                    {
                        if (m_timers[GetTimer("Buffer")].CheckFinished())
                        {
                            m_currentPhase = m_bufferPhase.m_nextPhase;
                            InitializePhase();
                        }
                        break;
                    }
                case DriveAndSeekPhases.INACTIVE:
                    {
                        if (m_active)
                        {
                            m_currentPhase = DriveAndSeekPhases.SETUP;
                            InitializePhase();
                        }

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
            for (int iter = 0; iter <= m_timers.Count - 1; iter++)
            {
                if (m_timers[iter].m_name == _name)
                {
                    return iter;
                }
            }
            return -1;
        }

        void SetupHiderAndSeekers()
        {
            if (!m_hiderWon)
            {
                int prevHiderNum = m_hiderNumber;
                do
                {
                    m_hiderNumber = UnityEngine.Random.Range(1, PlayerManager.m_instance.m_numberOfCars + 1) - 1;
                } while (m_hiderNumber == prevHiderNum);
            }

            //DeadCarManager.m_instance.SetHiderNumber(m_hiderNumber);

            PlayerManager.m_instance.m_playerCars[m_hiderNumber].GetComponent<Car>().SetRunner();
            string HiderTag = PlayerManager.m_instance.m_playerCars[m_hiderNumber].transform.tag;
            PlayerManager.m_instance.m_playerCars[m_hiderNumber].transform.position = m_hiderSpawn.position;
            PlayerManager.m_instance.m_playerCars[m_hiderNumber].transform.rotation = m_hiderSpawn.rotation;

            int SpawnsUsed = 0;
            for (int iter = 0; iter <= PlayerManager.m_instance.m_numberOfCars - 1; iter++)
            {
                if (iter != m_hiderNumber)
                {
                    PlayerManager.m_instance.m_playerCars[iter].GetComponent<Car>().SetChaser(HiderTag);
                    PlayerManager.m_instance.m_playerCars[iter].transform.position = m_seekerSpawns[SpawnsUsed].position;
                    PlayerManager.m_instance.m_playerCars[iter].transform.rotation = m_seekerSpawns[SpawnsUsed].rotation;
                    SpawnsUsed++;
                }
            }

            m_hiderWon = false;
        }

        bool CheckHidersCaught()
        {
            if (PlayerManager.m_instance.m_playerCars[m_hiderNumber].GetComponent<Car>().m_isDead)
            {
                return true;
            }
            return false;
        }

        bool CheckSeekersDead()
        {
            foreach (GameObject player in PlayerManager.m_instance.m_playerCars)
            {
                if (player.GetComponent<Car>().m_playerNumber != m_hiderNumber)
                {
                    if (!player.GetComponent<Car>().m_isDead)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        bool CheckFinished()
        {
            for (int iter = 0; iter <= m_playerScores.Count - 1; iter++)
            {
                if (m_playerScores[iter] == m_winningScore)
                {
                    m_gameWinner = iter + 1;
                    return true;
                }
            }
            return false;
        }

        void ResetRound()
        {
            foreach (Timer timer in m_timers)
            {
                timer.ResetTimer();
            }

            for (int iter = 0; iter <= PlayerManager.m_instance.m_numberOfCars - 1; iter++)
            {
                if (iter == m_hiderNumber)
                {
                    PlayerManager.m_instance.m_playerCars[iter].GetComponent<Car>().ResetRunner();
                }
                else
                {
                    PlayerManager.m_instance.m_playerCars[iter].GetComponent<Car>().ResetChaser();
                }
            }
        }

        void ChangePlayerMovement(int _index, bool _state)
        {
            PlayerManager.m_instance.m_playerCars[_index].GetComponent<Movement>().m_controls = _state;
        }

        void ChangeAllPlayerMovement(bool _state)
        {
            for (int iter = 0; iter <= PlayerManager.m_instance.m_numberOfCars - 1; iter++)
            {
                PlayerManager.m_instance.m_playerCars[iter].GetComponent<Movement>().m_controls = _state;
            }
        }
    }
}