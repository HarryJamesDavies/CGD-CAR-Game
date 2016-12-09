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

            if (Input.GetKeyDown("l"))
            {
                foreach (GameObject lights in m_lights)
                {
                    lights.SetActive(!lights.activeInHierarchy);
                }
            }

            if (Input.GetKeyDown("h"))
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

        //Debug.Log("R2: " + Input.GetAxis("P" + _controller + "-R2(PS4)"));
        //Debug.Log("L2: " + Input.GetAxis("P" + _controller + "-L2(PS4)"));
        //Debug.Log("LeftJoystickX: " + Input.GetAxis("P" + _controller + "-LeftJoystickX(PS4)"));

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
            transform.position += Vector3.up * 2.5f;
            transform.rotation = startingRotation;
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
}
