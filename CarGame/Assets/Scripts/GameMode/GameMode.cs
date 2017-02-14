using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace HF
{
    public class GameMode : MonoBehaviour
    {
        [Serializable]
        public struct PhaseLenght
        {
            public string m_name;
            public float m_length;
        }

        protected bool m_active = false;

        [HideInInspector]
        public List<Timer> m_timers;
        public GameObject m_gameModeTimer;
        protected Transform m_timerHolder;

        public float m_modeWidth;
        public float m_modeHeight;
        private Vector3 m_modeLocation;
        private Rect m_modeRect;

        public List<PhaseLenght> m_phases;

        public GameModeManager.GameModeState m_mode;

        protected void Start()
        {
            ResetEvent();
            InitialisePhases();
        }
        
        protected void Update()
        {

        }

        /// <summary>
        /// Returns whether the game mode is active
        /// </summary>
        /// <returns></returns>
        public bool IsActive()
        {
            return m_active;
        }

        /// <summary>
        /// Sets game mode active state
        /// </summary>
        public void SetActive(bool _active)
        {
            m_active = _active;
        }

        /// <summary>
        /// Returns the world position of the game mode
        /// </summary>
        public Vector3 GetEventPosition()
        {
            return m_modeLocation;
        }

        /// <summary>
        /// Returns the Rect the game mode encompasses
        /// </summary>
        /// <returns></returns>
        public Rect GetEventRect()
        {
            return m_modeRect;
        }

        /// <summary>
        /// Sets game modes positon and game mode Rect
        /// </summary>
        public void ResetEvent()
        {
            m_modeLocation = transform.position;
            Vector2 modeMinimum = new Vector2(m_modeLocation.x - m_modeWidth / 2, m_modeLocation.z - m_modeHeight / 2);
            m_modeRect = new Rect(modeMinimum, new Vector2(m_modeWidth, m_modeHeight));
        }

        /// <summary>
        /// Initalise phases based on Phase list
        /// </summary>
        void InitialisePhases()
        {
            //Creates timer holder
            m_timerHolder = new GameObject("TimerHolder").transform;
            m_timerHolder.transform.SetParent(transform);

            //Creates predetermined timers for specific phase
            for (int iter = 0; iter <= m_phases.Count - 1; iter++)
            {
                GameObject tempTimer = (GameObject)Instantiate(m_gameModeTimer);
                tempTimer.transform.SetParent(m_timerHolder);
                tempTimer.GetComponent<Timer>().SetTimer(m_phases[iter].m_name, m_phases[iter].m_length);
                m_timers.Add(tempTimer.GetComponent<Timer>());
            }
        }

        /// <summary>
        /// Resets all pre-made timers
        /// </summary>
        protected void ResetAllTimers()
        {
            foreach (Timer timer in m_timers)
            {
                timer.ResetTimer();
            }
        }

        /// <summary>
        /// Get timer index based on timer name
        /// </summary>
        protected int GetTimer(string _name)
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
    }
}