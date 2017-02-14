using UnityEngine;
using System.Collections;

namespace HF
{
    public class GameModeTrigger : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (!GetComponent<GameMode>().IsActive())
            {
                switch (other.tag)
                {
                    case "Player1":
                        if (ControllerManager.m_instance.m_useController)
                        {
                            if (Input.GetButtonDown("P1-X(PS4)"))
                            {
                                SetEvent(other.tag);
                            }
                        }
                        else
                        {
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                SetEvent(other.tag);
                            }
                        }
                        break;
                    case "Player2":
                        if (ControllerManager.m_instance.m_useController)
                        {
                            if (Input.GetButtonDown("P2-X(PS4)"))
                            {
                                SetEvent(other.tag);
                            }
                        }
                        else
                        {
                            if (Input.GetKeyDown("."))
                            {
                                SetEvent(other.tag);
                            }
                        }
                        break;
                    case "Player3":
                        if (ControllerManager.m_instance.m_useController)
                        {
                            if (Input.GetButtonDown("P3-X(PS4)"))
                            {
                                SetEvent(other.tag);
                            }
                        }
                        else
                        {
                            if (Input.GetKeyDown(KeyCode.Y))
                            {
                                SetEvent(other.tag);
                            }
                        }
                        break;
                    case "Player4":
                        if (ControllerManager.m_instance.m_useController)
                        {
                            if (Input.GetButtonDown("P4-X(PS4)"))
                            {
                                SetEvent(other.tag);
                            }
                        }
                        else
                        {
                            if (Input.GetKeyDown(KeyCode.O))
                            {
                                SetEvent(other.tag);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Activates the game mode attached to the same object
        /// </summary>
        public void SetEvent(string _triggerTag)
        {
            ShootBeam.sb_instance.DisplayLine();
            GetComponent<GameMode>().SetActive(true);
            GameModeManager.m_instance.m_currentGameMode = GetComponent<GameMode>();
            GameModeManager.m_instance.m_currentMode = GetComponent<GameMode>().m_mode;
            GameModeManager.m_instance.m_triggerTag = _triggerTag;
        }
    }
}

