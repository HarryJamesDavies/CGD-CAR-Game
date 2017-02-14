using UnityEngine;
using System.Collections;

namespace HF
{
    public class GameModeManager : MonoBehaviour
    {

        public static GameModeManager m_instance = null;

        public enum GameModeState
        {
            FREEROAM,
            EXAMPLE,
            DRIVEANDSEEK,
            Count
        };

        public GameModeState m_currentMode = GameModeState.FREEROAM;
        public GameModeState m_prevMode = GameModeState.FREEROAM;
        public GameModeState m_floatingMode = GameModeState.FREEROAM;

        public GameMode m_currentGameMode;
        private GameObject m_modeHolder;

        public string m_triggerTag;

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

            m_modeHolder = new GameObject("Mode Holder");
            m_modeHolder.transform.SetParent(transform);
        }

        void LateUpdate()
        {
            //Checks whether game mode has changed
            if (m_currentMode != m_floatingMode)
            {
                m_prevMode = m_floatingMode;
                m_floatingMode = m_currentMode;
                UpdateEvent();
            }
        }

        /// <summary>
        /// Handles the initialising of the new Game Mode globally
        /// </summary>
        void UpdateEvent()
        {
            switch (m_currentMode)
            {
                case GameModeState.FREEROAM:
                    {
                        EventManager.m_instance.AddEvent(Events.Event.GM_FREEROAM);
                        m_currentGameMode = null;
                        m_triggerTag = null;
                        break;
                    }
                case GameModeState.DRIVEANDSEEK:
                    {
                        EventManager.m_instance.AddEvent(Events.Event.GM_DRIVEANDSEEK);
                        break;
                    }
                case GameModeState.EXAMPLE:
                    {
                        EventManager.m_instance.AddEvent(Events.Event.GM_TEST);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}