using UnityEngine;
using System.Collections;

//===================== Kojima Drive - Half-Full Games 2017 ====================//
//
// Author(s): MATTHEW WYNTER
// Purpose: Set the hide and seek gamemode to be triggered. 
// Namespace: HF
//
//===============================================================================//

namespace HF
{
    public class EventCollider : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (!GetComponent<GameMode>().m_active)
            {
                switch (other.tag)
                {
                    case "Player1":
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
                        break;
                    case "Player2":
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
                        break;
                    case "Player3":
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
                        break;
                    case "Player4":
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
            ShootBeam.sb_instance.DisplayLine();
            GetComponent<GameMode>().m_active = true;
            GameModeManager.m_instance.m_currentGameMode = GetComponent<GameMode>();
            GameModeManager.m_instance.m_currentEvent = GetComponent<GameMode>().m_mode;
            GameModeManager.m_instance.m_triggerTag = _other.tag;
        }
    }
}
