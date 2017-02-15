using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HF
{

    public class TwistManager : MonoBehaviour
    {
        //teleporter stuff
        [SerializeField]
        GameObject m_teleporterManagerPrefab;
        GameObject m_teleporterManager;

        [SerializeField]
        GameObject m_teleporterPositionsPrefab;
        public GameObject m_teleporterPositions;

        public GameObject m_teleporterPrefab;

        //teleporter stuff end

        int previousTwist;

        bool m_timerStart;

        public List<Twists> m_eventTwists;

        public enum Twists
        {
            NULL,
            speedUp,
            dissapear,
            teleporters,
            tsunami,
            flipControls,
			eruption
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
            //m_teleporterPositions = Instantiate(m_teleporterPositionsPrefab);
        }

        // Update is called once per frame
        void Update()
        {
            //EventManager.m_instance.SubscribeToEvent(Events.Event.DS_CHASING, StartTwists);

            if (m_timerStart == false)
            {
                //EventManager.m_instance.SubscribeToEvent(Events.Event.DS_CHASE, SetUpCountdown);
                SetUpCountdown();
            }
        }

        //trigger to start the twist process
        public void StartTwists()
        {
            m_timerStart = false;
            PopulateTwistList();
        }

        //add each of the twists to the list dependent on game mode
        void PopulateTwistList()
        {
            m_eventTwists.Clear();
            if(GameModeManager.m_instance.m_currentMode == GameModeManager.GameModeState.DRIVEANDSEEK)
            {
                m_eventTwists.Add(Twists.tsunami);
                m_eventTwists.Add(Twists.eruption);
            }
        }

        //selects a twist from the list
        void ChooseTwist()
        {
            int twist = previousTwist;

            while (twist == previousTwist)
            {
                twist = Random.Range(0, m_eventTwists.Count);
            }

            previousTwist = twist;

            BroadcastTwist(twist);
            RemoveScripts();
            SpawnTwistScripts();
            m_timerStart = false;
        }

        //removes all the current twist scripts
        void RemoveScripts()
        {
            
        }

        //spawns the seperate twist scripts for implementation
        void SpawnTwistScripts()
        {

        }

        void BroadcastTwist(int twistNumber)
        {
            m_currentTwist = m_eventTwists[twistNumber];
            //ChangeTeleporter();
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

        void ChangeTeleporter()
        {

            if (m_currentTwist == Twists.teleporters && m_teleporterManager == null)
            {
                m_teleporterManager = Instantiate(m_teleporterManagerPrefab);
            }

            if (m_currentTwist != Twists.teleporters && m_teleporterManager != null)
            {
                Destroy(m_teleporterManager);
            }
        }

        public GameObject GetTeleporterPrefab()
        {
            return m_teleporterPrefab;
        }
    }
}