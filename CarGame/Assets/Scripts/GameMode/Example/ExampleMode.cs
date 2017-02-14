using UnityEngine;
using System.Collections;

namespace HF
{
    public class ExampleMode : GameMode
    {
        /* ================= Phase Variables ================= */
        public enum ExamplePhases
        {
            INACTIVE,
            SETUP,
            PLAY,
            RESET,
            FINISH,
            BUFFER,
            Count
        }

        public struct BufferPhase
        {
            public ExamplePhases m_nextPhase;
            public float m_lenght;
            public string m_message;
        }

        private ExamplePhases m_currentPhase;
        private BufferPhase m_bufferPhase;

        /* =================================================== */


        /* ============= Game Specific Variables ============= */

        public int m_numberOfRounds = 3;
        private int m_roundsPlayed = 0;

        /* =================================================== */

        new
        void Start()
        {
            base.Start();
            m_mode = GameModeManager.GameModeState.EXAMPLE;
        }

        new
       void Update()
        {
            base.Update();

            //Link to phase specific logic
            UpdatePhase();
        }

        /* ================= Phase Functions ================= */

        /// <summary>
        /// Called when changing to a new phase to initialise the phase
        /// </summary>
        void InitializePhase()
        {
            switch (m_currentPhase)
            {
                case ExamplePhases.INACTIVE:
                    {

                        break;
                    }
                case ExamplePhases.SETUP:
                    {
                        //Informs the game of the change of phase
                        EventManager.m_instance.AddEvent(Events.Event.EX_SETUP);

                        SpawnPlayers();

                        //Setup buffer phase
                        m_currentPhase = ExamplePhases.BUFFER;
                        m_bufferPhase.m_lenght = 5.0f;
                        m_bufferPhase.m_nextPhase = ExamplePhases.PLAY;
                        m_bufferPhase.m_message = "Round " + (m_roundsPlayed + 1) + " Set!";
                        InitializePhase();
                        break;
                    }
                case ExamplePhases.PLAY:
                    {
                        //Informs the game of the change of phase
                        EventManager.m_instance.AddEvent(Events.Event.EX_PLAY);

                        //Starts pre-made timer for play phase
                        m_timers[GetTimer("Play")].StartTimer();
                        break;
                    }
                case ExamplePhases.RESET:
                    {
                        //Informs the game of the change of phase
                        EventManager.m_instance.AddEvent(Events.Event.EX_RESET);

                        if (CheckFinished())
                        {
                            m_currentPhase = ExamplePhases.FINISH;
                            InitializePhase();
                        }
                        else
                        {
                            ResetRound();
                            m_currentPhase = ExamplePhases.SETUP;
                            InitializePhase();
                        }
                        break;
                    }
                case ExamplePhases.FINISH:
                    {
                        //Informs the game of the change of phase
                        EventManager.m_instance.AddEvent(Events.Event.DS_FINISH);

                        //Call standard function for deactivating game
                        EndGame();

                        //Setup buffer phase
                        m_currentPhase = ExamplePhases.BUFFER;
                        m_bufferPhase.m_lenght = 5.0f;
                        m_bufferPhase.m_nextPhase = ExamplePhases.INACTIVE;
                        m_bufferPhase.m_message = "Game Over!";
                        InitializePhase();

                        break;
                    }
                case ExamplePhases.BUFFER:
                    {
                        //Informs the game of the change of phase
                        EventManager.m_instance.AddEvent(Events.Event.DS_BUFFER);

                        //Initialise timer for dynamic buffer phase
                        Debug.Log(m_bufferPhase.m_message);
                        m_timers[GetTimer("Buffer")].m_timerLength = m_bufferPhase.m_lenght;
                        m_timers[GetTimer("Buffer")].StartTimer();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Phase loops for each individual phase
        /// </summary>
        void UpdatePhase()
        {
            switch (m_currentPhase)
            {
                case ExamplePhases.INACTIVE:
                    {
                        //Checks whether the game has been activated for play
                        if (m_active)
                        {
                            //Initialse when the game is active
                            m_currentPhase = ExamplePhases.SETUP;
                            InitializePhase();
                        }

                        break;
                    }
                case ExamplePhases.SETUP:
                    {
                        break;
                    }
                case ExamplePhases.PLAY:
                    {
                        //Checks whether the play phase has end
                        if (m_timers[GetTimer("Play")].CheckFinished())
                        {
                            //Increases the number of rounds played
                            m_roundsPlayed++;

                            //Changes phse to reset phase to hand end of round logic
                            m_currentPhase = ExamplePhases.RESET;
                            InitializePhase();
                        }
                        break;
                    }
                case ExamplePhases.RESET:
                    {
                        break;
                    }
                case ExamplePhases.FINISH:
                    {
                        break;
                    }
                case ExamplePhases.BUFFER:
                    {
                        //Checks whether the buffer phase has end
                        if (m_timers[GetTimer("Buffer")].CheckFinished())
                        {
                            //Changes to next phase upon buffer phase completion
                            m_currentPhase = m_bufferPhase.m_nextPhase;
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

        /* =================================================== */


        /* =============== Game Mode Functions =============== */

        /// <summary>
        /// Determines whether the game has ended
        /// </summary>
        /// <returns></returns>
        bool CheckFinished()
        {
            //Ends game when pre-allotted rounds played 
            if (m_roundsPlayed == m_numberOfRounds)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Moves Players to spawn position
        /// </summary>
        void SpawnPlayers()
        {
            for (int iter = 0; iter <= PlayerManager.m_instance.m_numberOfCars - 1; iter++)
            {
                Transform spawn = GetSpawn(iter);
                PlayerManager.m_instance.m_playerCars[iter].transform.position = spawn.position;
                PlayerManager.m_instance.m_playerCars[iter].transform.rotation = spawn.rotation;
            }
        }

        /// <summary>
        /// Resets variables and timers between rounds
        /// </summary>
        void ResetRound()
        {
            ResetAllTimers();
        }

        /// <summary>
        /// Resets game mode upon completion of the game
        /// </summary>
        void ResetMode()
        {
            ResetAllTimers();
            m_roundsPlayed = 0;
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
    }

    /* =================================================== */
}
