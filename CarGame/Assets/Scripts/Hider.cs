using UnityEngine;
using System.Collections;

public class Hider : MonoBehaviour {

    Movement m_carMovement;
    Car m_car;

    bool m_controlsEnabled;
    private Camera m_hiderCam;

    void Start()
    {
        m_carMovement = gameObject.GetComponent<Movement>();
        m_car = gameObject.GetComponent<Car>();
        m_controlsEnabled = true;
    }

	// Update is called once per frame
	void Update ()
    {
        SetLocation(m_car.m_playerNumber);
	}

    void SetLocation(int _playerNumber)
    {
        if (ControllerManager.m_instance.m_useController)
        {
            if (Input.GetButtonDown("P" + _playerNumber + ("-X(PS4)")))
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
        else
        {
            switch (_playerNumber)
            {
                case 1:
                    if (Input.GetKeyDown("z"))
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
                    break;
                case 2:
                    if (Input.GetKeyDown("v"))
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
                    break;
                case 3:
                    if (Input.GetKeyDown("m"))
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
                    break;
                case 4:
                    if (Input.GetKeyDown("/"))
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
                    break;
                default:
                    break;
            }
        }
    }
}
