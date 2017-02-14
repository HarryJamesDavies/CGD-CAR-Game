using UnityEngine;
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

        public struct BufferPhase
        {
            public DriveAndSeekPhases m_nextPhase;
            public float m_lenght;
            public string m_message;
        }

        public DriveAndSeekPhases m_currentPhase;
        private BufferPhase m_bufferPhase;

        [HideInInspector]
        public List<int> m_playerScores;
        public int m_winningScore = 3;
        private int m_hiderNumber = -1;
        private int m_gameWinner = -1;
        private bool m_hiderWon = false;

        private GameObject m_infoText;

        public Transform m_hiderSpawn;
        public List<Transform> m_seekerSpawns;

        new
        void Start()
        {
            base.Start();

            for (int iter = 1; iter <= 4; iter++)
            {
                m_playerScores.Add(0);
            }

            m_infoText = GameObject.FindGameObjectWithTag("DaSText");

            m_mode = GameModeManager.GameModeState.DRIVEANDSEEK;
            m_currentPhase = DriveAndSeekPhases.INACTIVE;
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

                        EndGame();

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
            ResetAllTimers();

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

        /// <summary>
        /// Handles game logic when the game ends
        /// </summary>
        void EndGame()
        {
            //Reset game to initial state
            ResetMode();

            //Sets game to inactive
            m_active = false;

            //Sets the game manager back to freeroam to initalise the game mode's deactivation globaly
            GameModeManager.m_instance.m_currentMode = GameModeManager.GameModeState.FREEROAM;
        }

        /// <summary>
        /// Resets game mode upon completion of the game
        /// </summary>
        void ResetMode()
        {
            ResetAllTimers();

            for (int iter = 0; iter <= m_playerScores.Count - 1; iter++)
            {
                m_playerScores[iter] = 0;
            }

            m_hiderNumber = -1;
            m_gameWinner = -1;
            m_hiderWon = false;
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
