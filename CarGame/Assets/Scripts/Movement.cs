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
        if (Input.GetKeyDown("q"))
        {
            transform.position += Vector3.up * 2.5f;
            transform.rotation = startingRotation;
        }

        if (Input.GetKeyDown("l"))
        {
            foreach(GameObject lights in m_lights)
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

        if (fuel < 0)
        {

            xspeep = 0;

        }

        xspeep *= friction;
        transform.Translate(Vector3.forward * -xspeep);

    }
}
