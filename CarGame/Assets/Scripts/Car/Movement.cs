using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//===================== Kojima Drive - Half-Full Games 2017 ====================//
//
// Author(s): HARRY DAVIES, ADAM MOOREY
// Purpose: Movement for the car and control handling
// Namespace: HF
//
//===============================================================================//

namespace HF
{
    public class Movement : MonoBehaviour
    {
        float m_appliedForce = 0f;
        public float m_power = 0.01f;
        float m_friction = 0.95f;
        bool m_forward = false;
        bool m_backward = false;
        bool m_right = false;
        bool m_left = false;

        public bool m_controls;

        public Vector3 m_direction;
        Quaternion startingRotation;
        public List<AudioClip> m_hornSounds;

        public List<GameObject> m_lights;

        public FuelSystem m_fuelSystem;

        void Start()
        {
            m_direction = Vector3.forward;
            startingRotation = transform.rotation;
            m_controls = true;

            m_fuelSystem = gameObject.GetComponent<FuelSystem>();
        }

        // Use this for initialization
        void FixedUpdate()
        {
            //apply the direction to the car
            if (m_forward)
            {
                m_appliedForce += m_power;

                if (m_fuelSystem.m_reduceFuel)
                {
                    m_fuelSystem.m_fuel -= m_power * 20;
                }
            }

            if (m_backward)
            {
                m_appliedForce -= m_power;

                if (m_fuelSystem.m_reduceFuel)
                {
                    m_fuelSystem.m_fuel -= m_power * 20;
                }
            }

            //rotate the car dependent on direction facing
            if (m_right)
            {
                transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
            }

            if (m_left)
            {
                transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f));
            }
        }

        // Update is called once per frame
        void Update()
        {
            //if the controls are enabled then determine 
            if (m_controls)
            {
                //if a controller is being used use correct controls
                if (ControllerManager.m_instance.m_useController)
                {
                    //if flipcontrols is not currently active, use normal controls
                    if (TwistManager.m_instance.m_currentTwist != TwistManager.Twists.flipControls)
                    {
                        switch (gameObject.tag)
                        {
                            case "Player1":
                                PlayerMovement(1);
                                break;
                            case "Player2":
                                PlayerMovement(2);
                                break;
                            case "Player3":
                                PlayerMovement(3);
                                break;
                            case "Player4":
                                PlayerMovement(4);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (gameObject.tag)
                        {
                            case "Player1":
                                FlipControls(1);
                                break;
                            case "Player2":
                                FlipControls(2);
                                break;
                            case "Player3":
                                FlipControls(3);
                                break;
                            case "Player4":
                                FlipControls(4);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    //if flipcontrols is not currently active, use normal controls
                    if (TwistManager.m_instance.m_currentTwist != TwistManager.Twists.flipControls)
                    {
                        switch (gameObject.tag)
                        {
                            case "Player1":
                                KeyboardMovement(1);
                                break;
                            case "Player2":
                                KeyboardMovement(2);
                                break;
                            case "Player3":
                                KeyboardMovement(3);
                                break;
                            case "Player4":
                                KeyboardMovement(4);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (gameObject.tag)
                        {
                            case "Player1":
                                FlipControls(1);
                                break;
                            case "Player2":
                                FlipControls(2);
                                break;
                            case "Player3":
                                FlipControls(3);
                                break;
                            case "Player4":
                                FlipControls(4);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                m_forward = false;
                m_backward = false;
                m_left = false;
                m_right = false;
                m_appliedForce = 0;
            }

            //when no force is being applied refuel, or use button to refuel so long as fuel is less than 50
            switch (gameObject.tag)
            {
                case "Player1":
                    if (Input.GetKeyDown("e"))
                    {
                        m_fuelSystem.Refuel();
                    }
                    break;
                case "Player2":
                    if (Input.GetKeyDown("."))
                    {
                        m_fuelSystem.Refuel();
                    }
                    break;
                case "Player3":
                    if (Input.GetKeyDown("y"))
                    {
                        m_fuelSystem.Refuel();
                    }
                    break;
                case "Player4":
                    if (Input.GetKeyDown("o"))
                    {
                        m_fuelSystem.Refuel();
                    }
                    break;
                default:
                    break;
            }
            
        }

        void PlayerMovement(int _controller)
        {
            if (Input.GetAxis("P" + _controller + "-R2(PS4)") > -1.0f)
            {
                m_forward = true;
            }
            if (Input.GetAxis("P" + _controller + "-R2(PS4)") == -1.0f)
            {
                m_forward = false;
            }
            if (Input.GetAxis("P" + _controller + "-L2(PS4)") > -1.0f)
            {
                m_backward = true;
            }
            if (Input.GetAxis("P" + _controller + "-L2(PS4)") == -1.0f)
            {
                m_backward = false;
            }

            if (Input.GetAxis("P" + _controller + ("-LeftJoystickX(PS4)")) > 0.5f)
            {
                m_right = true;
            }
            if (Input.GetAxis("P" + _controller + ("-LeftJoystickX(PS4)")) < 0.5f)
            {
                m_right = false;
            }
            if (Input.GetAxis("P" + _controller + ("-LeftJoystickX(PS4)")) < -0.5f)
            {
                m_left = true;
            }
            if (Input.GetAxis("P" + _controller + ("-LeftJoystickX(PS4)")) > -0.5f)
            {
                m_left = false;
            }

            if (Input.GetButtonDown("P" + _controller + ("-Triangle(PS4)")))
            {
                gameObject.transform.position += Vector3.up * 1.5f;
                gameObject.transform.rotation = startingRotation;
            }

            if (m_fuelSystem.m_fuel < 0)
            {
                m_appliedForce = 0;
            }

            m_appliedForce *= m_friction;
            transform.Translate(Vector3.forward * -m_appliedForce);
        }

        void KeyboardMovement(int _playerNumber)
        {
            if (_playerNumber == 1)
            {
                if (Input.GetKeyDown("w"))
                {
                    m_forward = true;
                }
                if (Input.GetKeyUp("w"))
                {
                    m_forward = false;
                }
                if (Input.GetKeyDown("s"))
                {
                    m_backward = true;
                }
                if (Input.GetKeyUp("s"))
                {
                    m_backward = false;
                }

                if (Input.GetKeyDown("d"))
                {
                    m_right = true;
                }
                if (Input.GetKeyUp("d"))
                {
                    m_right = false;
                }
                if (Input.GetKeyDown("a"))
                {
                    m_left = true;
                }
                if (Input.GetKeyUp("a"))
                {
                    m_left = false;
                }

                if (Input.GetKeyDown("q"))
                {
                    transform.position += Vector3.up * 1.5f;
                    transform.rotation = startingRotation;
                }
            }
            else if (_playerNumber == 2)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    m_forward = true;
                }
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    m_forward = false;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    m_backward = true;
                }
                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    m_backward = false;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    m_right = true;
                }
                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    m_right = false;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    m_left = true;
                }
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    m_left = false;
                }

                if (Input.GetKeyDown("/"))
                {
                    transform.position += Vector3.up * 1.5f;
                    transform.rotation = startingRotation;
                }
            }
            else if (_playerNumber == 3)
            {
                if (Input.GetKeyDown("t"))
                {
                    m_forward = true;
                }
                if (Input.GetKeyUp("t"))
                {
                    m_forward = false;
                }
                if (Input.GetKeyDown("g"))
                {
                    m_backward = true;
                }
                if (Input.GetKeyUp("g"))
                {
                    m_backward = false;
                }

                if (Input.GetKeyDown("h"))
                {
                    m_right = true;
                }
                if (Input.GetKeyUp("h"))
                {
                    m_right = false;
                }
                if (Input.GetKeyDown("f"))
                {
                    m_left = true;
                }
                if (Input.GetKeyUp("f"))
                {
                    m_left = false;
                }

                if (Input.GetKeyDown("r"))
                {
                    transform.position += Vector3.up * 1.5f;
                    transform.rotation = startingRotation;
                }
            }
            else if (_playerNumber == 4)
            {
                if (Input.GetKeyDown("i"))
                {
                    m_forward = true;
                }
                if (Input.GetKeyUp("i"))
                {
                    m_forward = false;
                }
                if (Input.GetKeyDown("k"))
                {
                    m_backward = true;
                }
                if (Input.GetKeyUp("k"))
                {
                    m_backward = false;
                }

                if (Input.GetKeyDown("l"))
                {
                    m_right = true;
                }
                if (Input.GetKeyUp("l"))
                {
                    m_right = false;
                }
                if (Input.GetKeyDown("j"))
                {
                    m_left = true;
                }
                if (Input.GetKeyUp("j"))
                {
                    m_left = false;
                }

                if (Input.GetKeyDown("u"))
                {
                    transform.position += Vector3.up * 1.5f;
                    transform.rotation = startingRotation;
                }
            }

            if (m_fuelSystem.m_fuel <= 0.0f)
            {
                m_appliedForce = 0;
            }

            m_appliedForce *= m_friction;
            transform.Translate(Vector3.forward * -m_appliedForce);
        }

        public void ToggleLights(bool _active)
        {
            foreach (GameObject lights in m_lights)
            {
                lights.SetActive(_active);
            }
        }

        void FlipControls(int _playerNumber)
        {
            if (ControllerManager.m_instance.m_useController)
            {
                if (Input.GetAxis("P" + _playerNumber + "-R2(PS4)") > -1.0f)
                {
                    m_backward = true;
                }
                if (Input.GetAxis("P" + _playerNumber + "-R2(PS4)") == -1.0f)
                {
                    m_backward = false;
                }
                if (Input.GetAxis("P" + _playerNumber + "-L2(PS4)") > -1.0f)
                {
                    m_forward = true;
                }
                if (Input.GetAxis("P" + _playerNumber + "-L2(PS4)") == -1.0f)
                {
                    m_forward = false;
                }

                if (Input.GetAxis("P" + _playerNumber + ("-LeftJoystickX(PS4)")) > 0.5f)
                {
                    m_left = true;
                }
                if (Input.GetAxis("P" + _playerNumber + ("-LeftJoystickX(PS4)")) < 0.5f)
                {
                    m_left = false;
                }
                if (Input.GetAxis("P" + _playerNumber + ("-LeftJoystickX(PS4)")) < -0.5f)
                {
                    m_right = true;
                }
                if (Input.GetAxis("P" + _playerNumber + ("-LeftJoystickX(PS4)")) > -0.5f)
                {
                    m_right = false;
                }

                if (Input.GetButtonDown("P" + _playerNumber + ("-Triangle(PS4)")))
                {
                    gameObject.transform.position += Vector3.up * 2.5f;
                    gameObject.transform.rotation = startingRotation;
                }

                if (m_fuelSystem.m_fuel < 0)
                {
                    m_appliedForce = 0;
                }

                m_appliedForce *= m_friction;
                transform.Translate(Vector3.forward * -m_appliedForce);
            }
            else
            {
                switch (_playerNumber)
                {
                    case 1:
                        if (Input.GetKeyDown("w"))
                        {
                            m_backward = true;
                        }
                        if (Input.GetKeyUp("w"))
                        {
                            m_backward = false;
                        }
                        if (Input.GetKeyDown("s"))
                        {
                            m_forward = true;
                        }
                        if (Input.GetKeyUp("s"))
                        {
                            m_forward = false;
                        }

                        if (Input.GetKeyDown("d"))
                        {
                            m_left = true;
                        }
                        if (Input.GetKeyUp("d"))
                        {
                            m_left = false;
                        }
                        if (Input.GetKeyDown("a"))
                        {
                            m_right = true;
                        }
                        if (Input.GetKeyUp("a"))
                        {
                            m_right = false;
                        }

                        if (Input.GetKeyDown("q"))
                        {
                            transform.position += Vector3.up * 2.5f;
                            transform.rotation = startingRotation;
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            m_backward = true;
                        }
                        if (Input.GetKeyUp(KeyCode.UpArrow))
                        {
                            m_backward = false;
                        }
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            m_forward = true;
                        }
                        if (Input.GetKeyUp(KeyCode.DownArrow))
                        {
                            m_forward = false;
                        }

                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            m_left = true;
                        }
                        if (Input.GetKeyUp(KeyCode.RightArrow))
                        {
                            m_left = false;
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            m_right = true;
                        }
                        if (Input.GetKeyUp(KeyCode.LeftArrow))
                        {
                            m_right = false;
                        }

                        if (Input.GetKeyDown("/"))
                        {
                            transform.position += Vector3.up * 2.5f;
                            transform.rotation = startingRotation;
                        }
                        break;
                    case 3:
                        if (Input.GetKeyDown("t"))
                        {
                            m_backward = true;
                        }
                        if (Input.GetKeyUp("t"))
                        {
                            m_backward = false;
                        }
                        if (Input.GetKeyDown("g"))
                        {
                            m_forward = true;
                        }
                        if (Input.GetKeyUp("g"))
                        {
                            m_forward = false;
                        }

                        if (Input.GetKeyDown("h"))
                        {
                            m_left = true;
                        }
                        if (Input.GetKeyUp("h"))
                        {
                            m_left = false;
                        }
                        if (Input.GetKeyDown("f"))
                        {
                            m_right = true;
                        }
                        if (Input.GetKeyUp("f"))
                        {
                            m_right = false;
                        }

                        if (Input.GetKeyDown("r"))
                        {
                            transform.position += Vector3.up * 2.5f;
                            transform.rotation = startingRotation;
                        }
                        break;
                    case 4:
                        if (Input.GetKeyDown("i"))
                        {
                            m_backward = true;
                        }
                        if (Input.GetKeyUp("i"))
                        {
                            m_backward = false;
                        }
                        if (Input.GetKeyDown("k"))
                        {
                            m_forward = true;
                        }
                        if (Input.GetKeyUp("k"))
                        {
                            m_forward = false;
                        }

                        if (Input.GetKeyDown("l"))
                        {
                            m_left = true;
                        }
                        if (Input.GetKeyUp("l"))
                        {
                            m_left = false;
                        }
                        if (Input.GetKeyDown("j"))
                        {
                            m_right = true;
                        }
                        if (Input.GetKeyUp("j"))
                        {
                            m_right = false;
                        }

                        if (Input.GetKeyDown("u"))
                        {
                            transform.position += Vector3.up * 2.5f;
                            transform.rotation = startingRotation;
                        }
                        break;
                    default:
                        break;
                }

                if (m_fuelSystem.m_fuel < 0)
                {
                    m_appliedForce = 0;
                }

                m_appliedForce *= m_friction;
                transform.Translate(Vector3.forward * -m_appliedForce);
            }
        }
    }
}