using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    public string m_tag;
    public int m_playerNumber;

    private Camera m_playerCam;
    public GameObject m_seekerParam;

    public bool m_hider;
    public bool m_seeker;
    public bool m_isDead;
    private GameObject m_seekerCone;

    void Awake()
    {
        m_tag = gameObject.tag;
        SetCamera();

        if (m_tag == "Player1")
        {
            m_playerNumber = 1;
            m_hider = false;
            m_seeker = false;
        }
        else if (m_tag == "Player2")
        {
            m_playerNumber = 2;
            m_hider = false;
            m_seeker = false;
        }
        else if (m_tag == "Player3")
        {
            m_playerNumber = 3;
            m_hider = false;
            m_seeker = false;
        }
        else if (m_tag == "Player4")
        {
            m_playerNumber = 4;
            m_hider = false;
            m_seeker = false;
        }
    }

    public void SetSeeker(string _hiderTag)
    {
        m_seeker = true;
        m_seekerCone = Instantiate(m_seekerParam, transform.position, Quaternion.Euler(0.0f, 90.0f, 90.0f)) as GameObject;
        m_seekerCone.transform.parent = gameObject.transform;
        m_seekerCone.GetComponent<SeekerScript>().m_hiderTag = _hiderTag;
    }

    public void SetHider()
    {
        m_hider = true;
        gameObject.AddComponent<Hider>();
    }

    public void ResetSeeker()
    {
        m_seeker = false;
        GetComponent<PlayerHealth>().ResetHealth();
        Destroy(m_seekerCone);
    }

    public void ResetHider()
    {
        m_hider = false;
        GetComponent<PlayerHealth>().ResetHealth();
        Destroy(GetComponent<Hider>());
    }

    public void ToggleCamera(bool _active)
    {
        m_playerCam.enabled = _active;
    }

    void SetCamera()
    {
        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            string tempTag = transform.GetChild(i).gameObject.tag;

            if (tempTag == "CarCamera")
            {
                m_playerCam = transform.GetChild(i).GetComponent<Camera>();
            }
        }

        switch (PlayerManager.m_instance.m_numberOfCars)
        {
            case 1:
                m_playerCam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                break;
            case 2:
                if (gameObject.tag == "Player1")
                {
                    m_playerCam.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                }
                else if (gameObject.tag == "Player2")
                {
                    m_playerCam.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                }
                break;
            case 3:
                if (gameObject.tag == "Player1")
                {
                    m_playerCam.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                }
                else if (gameObject.tag == "Player2")
                {
                    m_playerCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                }
                else if (gameObject.tag == "Player3")
                {
                    m_playerCam.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                }
                break;
            case 4:
                if (gameObject.tag == "Player1")
                {
                    m_playerCam.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                }
                else if (gameObject.tag == "Player2")
                {
                    m_playerCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                }
                else if (gameObject.tag == "Player3")
                {
                    m_playerCam.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                }
                else if (gameObject.tag == "Player4")
                {
                    m_playerCam.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
                }
                break;
            default:
                break;
        }
    }

    //void OnCollisionEnter(Collider _collider)
    //{
    //    if(_collider.gameObject.GetComponent<Car>().m_hider == true 
    //        || _collider.gameObject.GetComponent<Car>().m_seeker == true)
    //    {
    //        if(_collider.gameObject.GetComponent<Car>().m_seeker == true && m_hider == true)
    //        {
    //            if (StateManager.m_instance.gameManager.GetComponent<GameManager>().m_phaseDriveAndSeek 
    //                == GameManager.DriveAndSeekPhases.seek)
    //            {
    //                StateManager.m_instance.gameManager.GetComponent<GameManager>().hiderCaught();
    //            }

    //            //some sort of logic to add points to a score
    //        }
    //    }
    //}
}
