using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HF
{
    /// <summary>
    /// Directions of use:
    /// 
    /// 1: First, add your new twist to the twist enum, this acts as a store for all
    /// the twists in Kojima.
    /// 2: Next, go to the PopulateLists function and add in an else if onto the if statements
    /// to check what game mode is currently in play. This should use the game mode structure provided
    /// by the event system that Half Full has created
    /// 3: Go to the SpawnScripts function, and add an else if on the end of the if statements for your twist
    /// if the current twist is equal to your twist, then you should add the script components of your twists to the manager
    /// the scripts should then handle themselves, this function is called when a timer expires
    /// 4: Go to the RemoveScripts function and add a check in the form of an if statement to check to see
    /// if your script component is still on the manager. If it is, it needs to be destroyed. This check is done
    /// each time the timer expires, so only one twist is active at a time.
    /// 5: That's it, the twist has been added. However, you need to call the public function StartTwists to start off
    /// the process, the manager will then take care of itself.
    /// 6: If you want the twists to start after a certain time. Use the SetTimer function, to set a customised time for the twists
    /// before you call StartTwists (DEFAULT IS SET TO TEN SECONDS)
    /// 7: If you wish to stop the twists at any point in your logic, call StopTwists and the twist manager will return to it's 
    /// otherwise passive state
    /// </summary>
   
    public class TwistsManager : MonoBehaviour
    {

        int m_previousTwist;
        int m_timer = 10;
        bool m_timerStart;

        public List<Twist> m_eventTwists;

        public enum Twist
        {
            NULL,
            tsunami,
            eruption
        }

        public Twist m_currentTwist;

        public static TwistsManager m_instance;

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

            m_currentTwist = Twist.NULL;
            //m_teleporterPositions = Instantiate(m_teleporterPositionsPrefab);
        }

        // Update is called once per frame
        void Update()
        {

            if (m_timerStart == false)
            {
                SetUpCountdown();
            }
        }

        //trigger to start the twist process
        public void StartTwists()
        {
            m_timerStart = false;
            PopulateTwistList();
        }

        public void StopTwists()
        {
            m_timerStart = true;
            StopAllCoroutines();
            RemoveScripts();
        }

        //add each of the twists to the list dependent on game mode
        void PopulateTwistList()
        {
            m_eventTwists.Clear();
            if (GameModeManager.m_instance.m_currentMode == GameModeManager.GameModeState.DRIVEANDSEEK)
            {
                m_eventTwists.Add(Twist.tsunami);
                m_eventTwists.Add(Twist.eruption);
            }
        }

        //selects a twist from the list
        void ChooseTwist()
        {
            int twist = m_previousTwist;

            while (twist == m_previousTwist)
            {
                twist = Random.Range(0, m_eventTwists.Count);
            }

            m_previousTwist = twist;

            BroadcastTwist(twist);
            RemoveScripts();
            SpawnTwistScripts();
            m_timerStart = false;
        }

        //removes all the current twist scripts
        void RemoveScripts()
        {
            if(gameObject.GetComponent<Tsunami>())
            {
                Destroy(gameObject.GetComponent<Tsunami>());
            }
        }

        //spawns the seperate twist scripts for implementation
        void SpawnTwistScripts()
        {
            if(m_currentTwist == Twist.tsunami)
            {
                gameObject.AddComponent<Tsunami>();
            }
        }

        void BroadcastTwist(int twistNumber)
        {
            m_currentTwist = m_eventTwists[twistNumber];
        }

        void SetUpCountdown()
        {
            m_timerStart = true;
            StartCoroutine(CountdownToTwist());
        }

        IEnumerator CountdownToTwist()
        {
            yield return new WaitForSeconds(m_timer); //time that the thing will start
            ChooseTwist();
        }

        public void SetTimer(int toSet)
        {
            m_timer = toSet;
        }
    }
}