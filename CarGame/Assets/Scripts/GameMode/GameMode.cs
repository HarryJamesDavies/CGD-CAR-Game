using UnityEngine;
using System.Collections;

namespace HF
{
    public class GameMode : MonoBehaviour
    {
        public bool m_active = false;

        public Vector3 m_eventLocation;
        public float m_eventWidth;
        public float m_eventHeight;

        public Rect m_eventRect;

        public GameModeManager.GameModeState m_mode;

        // Use this for initialization
        protected void Start()
        {
            ResetEvent();
        }

        // Update is called once per frame
        protected void Update()
        {

        }

        public void ResetEvent()
        {
            m_eventLocation = transform.position;
            Vector2 eventMinimum = new Vector2(m_eventLocation.x - m_eventWidth / 2, m_eventLocation.z - m_eventHeight / 2);
            m_eventRect = new Rect(eventMinimum, new Vector2(m_eventWidth, m_eventHeight));
        }
    }
}