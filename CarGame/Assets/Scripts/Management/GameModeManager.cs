using UnityEngine;
using System.Collections;

namespace HF
{
    public class GameModeManager : MonoBehaviour
    {

        public static GameModeManager m_instance = null;

        public enum GameModeState
        {
            FREEROAM = 0,
            DRIVEANDSEEK = 1,
            TEST = 2,
            Count
        };

        public GameModeState m_currentEvent = GameModeState.FREEROAM;
        public GameModeState m_prevEvent = GameModeState.FREEROAM;
        public GameModeState m_floatingEvent = GameModeState.FREEROAM;

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

        // Update is called once per frame
        void LateUpdate()
        {
            if (m_currentEvent != m_floatingEvent)
            {
                m_prevEvent = m_floatingEvent;
                m_floatingEvent = m_currentEvent;
                UpdateEvent();
            }
        }

        void UpdateEvent()
        {
            switch (m_currentEvent)
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
                case GameModeState.TEST:
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