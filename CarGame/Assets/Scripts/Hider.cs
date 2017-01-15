using UnityEngine;
using System.Collections;

public class Hider : MonoBehaviour {

    Movement m_carMovement;

    bool m_controlsEnabled;
    private Camera m_hiderCam;

    void Start()
    {
        m_carMovement = gameObject.GetComponent<Movement>();
        m_controlsEnabled = true;
    }

	// Update is called once per frame
	void Update ()
    {
	    switch (gameObject.tag)
        {
            case "Player1":
                SetLocation(1);
                break;
            case "Player2":
                SetLocation(2);
                break;
            case "Player3":
                SetLocation(3);
                break;
            case "Player4":
                SetLocation(4);
                break;
            default:
                break;
        }
	}

    void SetLocation(int _playerNumber)
    {
        if (ControllerManager.m_instance.m_useController)
        {
            if (Input.GetButtonDown("P" + _playerNumber + ("-X(PS4)")))
            {
                if (!m_controlsEnabled)
                {
                    m_controlsEnabled = true;
                    SetCamera(true);
                }
                else
                {
                    m_controlsEnabled = false;
                    SetCamera(false);
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
                        if (!m_controlsEnabled)
                        {
                            m_controlsEnabled = true;
                            SetCamera(true);
                        }
                        else
                        {
                            m_controlsEnabled = false;
                            SetCamera(false);
                        }
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown("v"))
                    {
                        if (!m_controlsEnabled)
                        {
                            m_controlsEnabled = true;
                            SetCamera(true);
                        }
                        else
                        {
                            m_controlsEnabled = false;
                            SetCamera(false);
                        }
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown("m"))
                    {
                        if (!m_controlsEnabled)
                        {
                            m_controlsEnabled = true;
                            SetCamera(true);
                        }
                        else
                        {
                            m_controlsEnabled = false;
                            SetCamera(false);
                        }
                    }
                    break;
                case 4:
                    if (Input.GetKeyDown("/"))
                    {
                        if (!m_controlsEnabled)
                        {
                            m_controlsEnabled = true;
                            SetCamera(true);
                        }
                        else
                        {
                            m_controlsEnabled = false;
                            SetCamera(false);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        m_carMovement.m_controls = m_controlsEnabled;
    }

    void SetCamera(bool _active)
    {
        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            string tempTag = transform.GetChild(i).gameObject.tag;

            if (tempTag == "CarCamera")
            {
                Debug.Log("Found hider cam.");
                m_hiderCam = transform.GetChild(i).GetComponent<Camera>();
                if (m_hiderCam.enabled)
                {
                    m_hiderCam.enabled = false;
                }
                else
                {
                    m_hiderCam.enabled = true;
                }
            }
        }
    }
}
