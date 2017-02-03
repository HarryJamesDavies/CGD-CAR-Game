using UnityEngine;
using System.Collections;

//===================== Kojima Drive - Half-Full Games 2017 ====================//
//
// Author(s): ADAM MOOREY, MATTHEW WYNTER
// Purpose: Script used for runner functionalty, hide cam, toggle controls etc.
// Namespace: HF
//
//===============================================================================//

namespace HF
{
    public class Hider : MonoBehaviour
    {

        Movement m_carMovement;
        Car m_car;

        private Camera m_hiderCam;
        //private bool m_hidingState = false;

        int m_playerNumber;

        public GameObject chasebreaker;

        void Start()
        {
            //EventManager.m_instance.SubscribeToEvent(Events.Event.DS_RUNNING, EvFunc_RunningPhase);
            //EventManager.m_instance.SubscribeToEvent(Events.Event.DS_BUFFER, EvFunc_BufferPhase);

            m_carMovement = gameObject.GetComponent<Movement>();
            m_car = gameObject.GetComponent<Car>();
            m_playerNumber = m_car.m_playerNumber;
        }

        //void EvFunc_RunningPhase()
        //{
        //    m_hidingState = true;
        //}

        //void EvFunc_BufferPhase()
        //{
        //    m_hidingState = false;
        //}

        // Update is called once per frame
        void Update()
        {
            SetLocation();
        }

        void SetLocation()
        {
            if (ControllerManager.m_instance.m_useController)
            {
                switch (gameObject.tag)
                {
                    case "Player1":
                        if (Input.GetButtonDown("P1-X(PS4)"))
                        {
                            ToggleHide();
                        }
                        break;
                    case "Player2":
                        if (Input.GetButtonDown("P2-X(PS4)"))
                        {
                            ToggleHide();
                        }
                        break;
                    case "Player3":
                        if (Input.GetButtonDown("P3-X(PS4)"))
                        {
                            ToggleHide();
                        }
                        break;
                    case "Player4":
                        if (Input.GetButtonDown("P4-X(PS4)"))
                        {
                            ToggleHide();
                        }
                        break;
                    default:
                        Debug.Log("Controller Hider Action Default");
                        break;
                }
            }
            else
            {
                switch (m_playerNumber)
                {
                    case 1:
                        if ((Input.GetKeyDown("z")))
                        {
                            ToggleHide();
                        }
                        break;
                    case 2:
                        if ((Input.GetKeyDown(KeyCode.RightShift)))
                        {
                            ToggleHide();
                        }
                        break;
                    case 3:
                        if ((Input.GetKeyDown("v")))
                        {
                            ToggleHide();
                        }
                        break;
                    case 4:
                        if ((Input.GetKeyDown("m")))
                        {
                            ToggleHide();
                        }
                        break;
                    default:
                        Debug.Log("keyboard Hider Action Default");
                        break;
                }
            }
        }

        void ToggleHide()
        {
            if (!m_carMovement.m_controls)
            {
                m_carMovement.m_controls = true;
                m_car.ToggleCamera(true);
                m_carMovement.ToggleLights(true);
            }
            else
            {
                m_carMovement.m_controls = false;
                m_car.ToggleCamera(false);
                m_carMovement.ToggleLights(false);
            }
        }
    }
}