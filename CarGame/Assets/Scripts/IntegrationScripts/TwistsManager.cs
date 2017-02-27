﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HF
{
    /// <summary>
    /// *Old* Directions of use:
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
        int m_numberOfTwistsAtOnce = 1;
        float m_timer = 10.0f;
        bool m_timerStart;

        GameObject tsunamiPrefab;

        Twists m_twistContainer = new Twists();

        public List<Twists.Twist> m_eventTwists = new List<Twists.Twist>();

        public List<Twists.Twist> m_currentTwists = new List<Twists.Twist>();

        public List<GameObject> m_TwistManagers = new List<GameObject>();

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

            m_timerStart = false;

            m_twistContainer.allOff = false;
            m_twistContainer.tsunamiOff = false;
            m_twistContainer.eruptionOff = false;

        }

        // Update is called once per frame to check to see if the timer is ready to be started
        void Update()
        {
            if (m_timerStart == true)
            {
                SetUpCountdown();
            }
        }

        //trigger to start the twist process
        public void StartTwists()
        {
            if (m_twistContainer.allOff == true)
            {
                m_timerStart = true;
                PopulateTwistList();
            }
        }

        //trigger to stop the twist process
        public void StopTwists()
        {
            m_timerStart = false;
            StopAllCoroutines();
            RemoveScripts();
        }

        //add each of the twists to the list dependent on game mode
        void PopulateTwistList()
        {
            m_eventTwists.Clear();
            if (GameModeManager.m_instance.m_currentMode == GameModeManager.GameModeState.DRIVEANDSEEK)
            {
                if (m_twistContainer.tsunamiOff == true)
                {
                    m_eventTwists.Add(Twists.Twist.tsunami);
                }
                if (m_twistContainer.eruptionOff == true)
                {
                    m_eventTwists.Add(Twists.Twist.eruption);
                }
            }
            else if(GameModeManager.m_instance.m_currentMode == GameModeManager.GameModeState.FREEROAM)
            {
                StopTwists();
                Debug.Log("Twist tried to start in freeroam. Twist manager was disabled");
            }

        }

        //selects a twist from the list
        void ChooseTwist()
        {
            int twist = m_previousTwist;
            m_currentTwists.Clear();
            for (int iter = 0; iter < m_numberOfTwistsAtOnce; iter++)
            {

                while (twist == m_previousTwist)
                {
                    twist = Random.Range(0, m_eventTwists.Count);
                }

                m_currentTwists.Add(m_eventTwists[iter]);
                m_previousTwist = twist;
            }
            

            RemoveScripts();
            SpawnTwistScripts();
            m_timerStart = true;
        }

        //removes all the current twist scripts
        void RemoveScripts()
        {
            for (int iter = 0; m_TwistManagers.Count > iter; iter++)
            {
                Destroy(m_TwistManagers[iter]);
            }
            m_TwistManagers.Clear();
        }

        //spawns the seperate twist scripts for implementation
        void SpawnTwistScripts()
        {
            for (int iter = 0; m_currentTwists.Count > iter; iter++)
            {
                if (m_currentTwists[iter] == Twists.Twist.tsunami)
                {
                    m_TwistManagers.Add(Instantiate(tsunamiPrefab));
                }
                else if (m_currentTwists[iter] == Twists.Twist.NULL)
                {
                    //do nothing
                }
            }
        }

        //sets the countdown to be started
        void SetUpCountdown()
        {
            m_timerStart = false;
            StartCoroutine(CountdownToTwist());
        }

        //countdown before twist kicks in
        IEnumerator CountdownToTwist()
        {
            yield return new WaitForSeconds(m_timer); //time that the thing will start
            ChooseTwist();
        }

        //helper function allowing you to set the time in seconds when the twist kicks in
        public void SetTimer(float toSet)
        {
            m_timer = toSet;
        }

        //helper function allowing you to set the time in seconds when the twist kicks in
        public void SetTimer(int toSet)
        {
            m_timer = toSet;
        }

        public void SetAllOff(bool toSet)
        {
            m_twistContainer.allOff = toSet;
        }
        public void SetTsunamiOff(bool toSet)
        {
            m_twistContainer.tsunamiOff = toSet;
        }
        public void SetEruptionOff(bool toSet)
        {
            m_twistContainer.eruptionOff = toSet;
        }

        public void SetNumberOfTwistsAtOnce(int toSet)
        {
            m_numberOfTwistsAtOnce = toSet;
        }
    }
}