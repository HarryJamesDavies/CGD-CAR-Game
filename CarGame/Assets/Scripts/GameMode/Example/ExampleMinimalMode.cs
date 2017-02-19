using UnityEngine;
using System.Collections;

namespace HF
{
    public class ExampleMinimalMode : GameMode
    {
        new
        void Start()
        {
            base.Start();
        }

        new
       void Update()
        {
            base.Update();

            //Game Mode Loop
            if (m_active)
            {
                Debug.Log("Example Active");

                //Game Modes are required to have an exit point
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    EndGame();
                }
            }
        }

        /// <summary>
        /// Handles game logic when the game ends
        /// </summary>
        void EndGame()
        {
            //Sets game to inactive
            m_active = false;

            //Sets the game manager back to freeroam to initalise the game mode's deactivation globaly
            GameModeManager.m_instance.m_currentMode = GameModeManager.GameModeState.FREEROAM;
        }
    }
}
