﻿using UnityEngine;
using System.Collections;

namespace HF
{
    public class EventCollider : MonoBehaviour
    {

        //check if player 1 has already pressed to play
        public bool m_firstPlayerPlaying = false;
        public bool m_secondPlayerPlaying = false;
        public bool m_thirdPlayerPlaying = false;
        public bool m_fourthPlayerPlaying = false;

        private void OnTriggerStay(Collider other)
        {
            if (!GetComponent<GameMode>().m_active)
            {
                switch (other.tag)
                {
                    case "Player1":
                        if (m_firstPlayerPlaying == false)
                        {
                            if (ControllerManager.m_instance.m_useController)
                            {
                                if (Input.GetButtonDown("P1-X(PS4)"))
                                {
                                    SetEvent(other);
                                }
                            }
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.E))
                                {
                                    SetEvent(other);
                                }
                            }
                        }
                        break;
                    case "Player2":
                        if (m_secondPlayerPlaying == false)
                        {
                            if (ControllerManager.m_instance.m_useController)
                            {
                                if (Input.GetButtonDown("P2-X(PS4)"))
                                {
                                    SetEvent(other);
                                }
                            }
                            else
                            {
                                if (Input.GetKeyDown("."))
                                {
                                    SetEvent(other);
                                }
                            }
                        }
                        break;
                    case "Player3":
                        if (m_thirdPlayerPlaying == false)
                        {
                            if (ControllerManager.m_instance.m_useController)
                            {
                                if (Input.GetButtonDown("P3-X(PS4)"))
                                {
                                    SetEvent(other);
                                }
                            }
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.Y))
                                {
                                    SetEvent(other);
                                }
                            }
                        }
                        break;
                    case "Player4":
                        if (m_fourthPlayerPlaying == false)
                        {
                            if (ControllerManager.m_instance.m_useController)
                            {
                                if (Input.GetButtonDown("P4-X(PS4)"))
                                {
                                    SetEvent(other);
                                }
                            }
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.O))
                                {
                                    SetEvent(other);
                                }
                            }
                        }
                        break;
                    default:
                        //Debug.Log("Default for event trigger");
                        break;
                }
            }

            if (other.tag == "Player1")
            {

            }

            //not currently set up for the 2nd player
            if (other.tag == "Player2")
            {
                //Debug.Log("Player2");
            }

        }

        void SetEvent(Collider _other)
        {
            Debug.Log(_other.tag + " wants to connect!");

            //activates the laser and active player number
            PlayersConnected.pc_instance.IncrementPC();

            //only fire the beam if there is 1 player connected
            if (PlayersConnected.pc_instance.m_playersconnected > 0)
            {
                ShootBeam.sb_instance.DisplayLine();
            }

            GetComponent<GameMode>().m_active = true;
            GameModeManager.m_instance.m_currentGameMode = GetComponent<GameMode>();
            GameModeManager.m_instance.m_currentEvent = GetComponent<GameMode>().m_mode;
        }
    }
}
