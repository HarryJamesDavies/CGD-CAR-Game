using UnityEngine;
using System.Collections;

namespace HF
{

    public class TwistManager : MonoBehaviour
    {
        [SerializeField]
        GameObject m_teleporterManagerPrefab;
        GameObject m_teleporterManager;

        [SerializeField]
        GameObject m_teleporterPositionsPrefab;
        public GameObject m_teleporterPositions;

        public GameObject m_teleporterPrefab;

        int previousTwist;

        bool m_timerStart;

        public enum Twists
        {
            NULL,
            speedUp,
            dissapear,
            tsunami,
            teleporters,
            flipControls
        }

        public Twists m_currentTwist;

        public static TwistManager m_instance;

        // Use this for initialization
        void Start()
        {
            if (m_instance)
            {
                Destroy(this);
            }
            else
            {
                m_instance = this;
            }

            m_timerStart = true;

            m_currentTwist = Twists.NULL;
            m_teleporterPositions = Instantiate(m_teleporterPositionsPrefab);
        }

        // Update is called once per frame
        void Update()
        {
            EventManager.m_instance.SubscribeToEvent(Events.Event.DS_CHASE, StartTwists);

            if (m_timerStart == false)
            {
                //EventManager.m_instance.SubscribeToEvent(Events.Event.DS_CHASE, SetUpCountdown);
                SetUpCountdown();
            }
        }

        void StartTwists()
        {
            m_timerStart = false;
        }

        void ChooseTwist()
        {
            int twist = previousTwist;

            while (twist == previousTwist)
            {
                twist = Random.Range(1, 6);
            }

            previousTwist = twist;

            BroadcastTwist(twist);
            m_timerStart = false;
        }

        void BroadcastTwist(int twistNumber)
        {
            m_currentTwist = (Twists)twistNumber;

            if(m_currentTwist == Twists.teleporters && m_teleporterManager == null)
            {
                m_teleporterManager = Instantiate(m_teleporterManagerPrefab);
            }

            if(m_currentTwist != Twists.teleporters && m_teleporterManager != null)
            {
                Destroy(m_teleporterManager);
            }
        }

        void SetUpCountdown()
        {
            m_timerStart = true;
            StartCoroutine(CountdownToTwist());
        }

        IEnumerator CountdownToTwist()
        {
            yield return new WaitForSeconds(10); //time that the thing will start
            ChooseTwist();
        }

        public GameObject GetTeleporterPrefab()
        {
            return m_teleporterPrefab;
        }
    }
}