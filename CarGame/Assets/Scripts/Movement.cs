using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

    float xspeep = 0f;
    public float power = 0.01f;
    float friction = 0.95f;
    bool forward = false;
    bool backward = false;
    bool right = false;
    bool left = false;

    public float fuel = 2;
    public bool m_controls;

    public Vector3 m_direction;
    Quaternion startingRotation;
    public List<AudioClip> m_hornSounds;
    AudioSource m_audioSource;

    public List<GameObject> m_lights;

    void Start()
    {
        m_direction = Vector3.forward;
        startingRotation = transform.rotation;
        m_audioSource = GetComponent<AudioSource>();
        m_controls = true;
    }


    // Use this for initialization
    void FixedUpdate()
    {
        if (forward)
        {
            xspeep += power;
            fuel -= power;
        }
        if (backward)
        {
            xspeep -= power;
            fuel -= power;
        }

        if(right)
        {
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
        }

        if(left)
        {
            transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_controls);

        if (m_controls)
        {
            if (ControllerManager.m_instance.m_useController)
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
        }
        else
        {
            forward = false;
            backward = false;
            left = false;
            right = false;
            xspeep = 0;
        }
    }

    void PlayerMovement(int _controller)
    {
        if (Input.GetAxis("P" + _controller + "-R2(PS4)") > -1.0f)
        {
            forward = true;
        }
        if (Input.GetAxis("P" + _controller + "-R2(PS4)") == -1.0f)
        {
            forward = false;
        }
        if (Input.GetAxis("P" + _controller + "-L2(PS4)") > -1.0f)
        {
            backward = true;
        }
        if (Input.GetAxis("P" + _controller + "-L2(PS4)") == -1.0f)
        {
            backward = false;
        }

        if (Input.GetAxis("P" + _controller + ("-LeftJoystickX(PS4)")) > 0.5f)
        {
            right = true;
        }
        if (Input.GetAxis("P" + _controller + ("-LeftJoystickX(PS4)")) < 0.5f)
        {
            right = false;
        }
        if (Input.GetAxis("P" + _controller + ("-LeftJoystickX(PS4)")) < -0.5f)
        {
            left = true;
        }
        if (Input.GetAxis("P" + _controller + ("-LeftJoystickX(PS4)")) > -0.5f)
        {
            left = false;
        }

        if (Input.GetButtonDown("P" + _controller + ("-Triangle(PS4)")))
        {
            gameObject.transform.position += Vector3.up * 2.5f;
            gameObject.transform.rotation = startingRotation;
        }

        if (Input.GetButtonDown("P" + _controller + ("-Square(PS4)")))
        {
            foreach (GameObject lights in m_lights)
            {
                lights.SetActive(!lights.activeInHierarchy);
            }
        }

        if (Input.GetButtonDown("P" + _controller + ("-Circle(PS4)")))
        {
            int sound = Random.Range(0, m_hornSounds.Count);
            m_audioSource.clip = m_hornSounds[sound];
            m_audioSource.Play();
        }

        if (fuel < 0)
        {
            xspeep = 0;
        }

        xspeep *= friction;
        transform.Translate(Vector3.forward * -xspeep);
    }

    void KeyboardMovement(int _playerNumber)
    {
        if (_playerNumber == 1)
        {
            if (Input.GetKeyDown("w"))
            {
                forward = true;
            }
            if (Input.GetKeyUp("w"))
            {
                forward = false;
            }
            if (Input.GetKeyDown("s"))
            {
                backward = true;
            }
            if (Input.GetKeyUp("s"))
            {
                backward = false;
            }

            if (Input.GetKeyDown("d"))
            {
                right = true;
            }
            if (Input.GetKeyUp("d"))
            {
                right = false;
            }
            if (Input.GetKeyDown("a"))
            {
                left = true;
            }
            if (Input.GetKeyUp("a"))
            {
                left = false;
            }

            if (Input.GetKeyDown("q"))
            {
                transform.position += Vector3.up * 2.5f;
                transform.rotation = startingRotation;
            }
        }
        else if (_playerNumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                forward = true;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                forward = false;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                backward = true;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                backward = false;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                right = true;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                right = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                left = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                left = false;
            }

            if (Input.GetKeyDown("/"))
            {
                transform.position += Vector3.up * 2.5f;
                transform.rotation = startingRotation;
            }
        }
        else if (_playerNumber == 3)
        {
            if (Input.GetKeyDown("t"))
            {
                forward = true;
            }
            if (Input.GetKeyUp("t"))
            {
                forward = false;
            }
            if (Input.GetKeyDown("g"))
            {
                backward = true;
            }
            if (Input.GetKeyUp("g"))
            {
                backward = false;
            }

            if (Input.GetKeyDown("h"))
            {
                right = true;
            }
            if (Input.GetKeyUp("h"))
            {
                right = false;
            }
            if (Input.GetKeyDown("f"))
            {
                left = true;
            }
            if (Input.GetKeyUp("f"))
            {
                left = false;
            }

            if (Input.GetKeyDown("r"))
            {
                transform.position += Vector3.up * 2.5f;
                transform.rotation = startingRotation;
            }
        }
        else if (_playerNumber == 4)
        {
            if (Input.GetKeyDown("i"))
            {
                forward = true;
            }
            if (Input.GetKeyUp("i"))
            {
                forward = false;
            }
            if (Input.GetKeyDown("k"))
            {
                backward = true;
            }
            if (Input.GetKeyUp("k"))
            {
                backward = false;
            }

            if (Input.GetKeyDown("l"))
            {
                right = true;
            }
            if (Input.GetKeyUp("l"))
            {
                right = false;
            }
            if (Input.GetKeyDown("j"))
            {
                left = true;
            }
            if (Input.GetKeyUp("j"))
            {
                left = false;
            }

            if (Input.GetKeyDown("u"))
            {
                transform.position += Vector3.up * 2.5f;
                transform.rotation = startingRotation;
            }
        }

        //if (Input.GetKeyDown("l"))
        //{
        //    foreach (GameObject lights in m_lights)
        //    {
        //        lights.SetActive(!lights.activeInHierarchy);
        //    }
        //}

        //if (Input.GetKeyDown("h"))
        //{
        //    int sound = Random.Range(0, m_hornSounds.Count);
        //    m_audioSource.clip = m_hornSounds[sound];
        //    m_audioSource.Play();
        //}

        if (fuel < 0)
        {
            xspeep = 0;
        }

        xspeep *= friction;
        transform.Translate(Vector3.forward * -xspeep);
    }

    public void ToggleLights(bool _active)
    {
        foreach (GameObject lights in m_lights)
        {
            lights.SetActive(_active);
        }
    }
}
