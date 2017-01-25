using UnityEngine;
using UnityEngine.UI;
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

    public RawImage m_image;
    public Texture m_chaserTitle;
    public Texture m_runnerTitle;
    public Texture m_outOfFuel;
    public Text m_fuel;

    public Canvas chasebreakerUI;

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

        m_fuel.enabled = false;
        EventManager.m_instance.SubscribeToEvent(Events.Event.DS_SETUP, SetupText);
        EventManager.m_instance.SubscribeToEvent(Events.Event.DS_HIDING, HideText);
    }

    void Update()
    {
        //updates the fuel text for the runner, and sets others to false
        if (m_hider)
        {
            m_fuel.enabled = true;
            m_fuel.text = "Fuel: " + (int)GetComponent<Movement>().fuel;
        }
        else
        {
            m_fuel.enabled = false;
        }

        //checks when the out of fuel image is showing or not
        if (GetComponent<Movement>().fuel == 0.0f)
        {
            ShowFuel();
        }
        else if (GetComponent<Movement>().m_controls == true)
        {
            m_image.enabled = false;
        }
    }

    public void SetSeeker(string _hiderTag)
    {
        m_seeker = true;        
        m_seekerCone = Instantiate(m_seekerParam, transform.position, Quaternion.Euler(0.0f, 90.0f, 90.0f)) as GameObject;
        m_seekerCone.transform.parent = gameObject.transform;

        //Instantiate(chasebreakerUI, transform.position, transform.rotation);

        m_seekerCone.GetComponent<SeekerScript>().m_hiderTag = _hiderTag;
        Destroy(GetComponent<ChaseBreaker>());
    }

    public void SetHider()
    {
        m_hider = true;
        GetComponent<Movement>().m_power = 0.006f;
        //gameObject.AddComponent<Hider>();
        gameObject.AddComponent<HiderAbilities>();
        gameObject.GetComponent<ChaseBreaker>().enabled = true;
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
        GetComponent<Movement>().m_power = 0.005f;
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

    void SetupText()
    {
        if (m_hider)
        {
            m_image.texture = m_runnerTitle;
            m_image.enabled = true;
        }
        else
        {
            m_image.texture = m_chaserTitle;
            m_image.enabled = true;
        }
    }

    void HideText()
    {
        m_image.enabled = false;
    }

    void ShowFuel()
    {
        if (GetComponent<Movement>().fuel <= 0.0f)
        {
            m_image.enabled = true;
            m_image.texture = m_outOfFuel;
        }
    }

    void OnDestroy()
    {
        EventManager.m_instance.UnsubscribeToEvent(Events.Event.DS_SETUP, SetupText);
        EventManager.m_instance.UnsubscribeToEvent(Events.Event.DS_HIDING, HideText);
        EventManager.m_instance.UnsubscribeToEvent(Events.Event.DS_CHASE, ShowFuel);
    }

    //public void ChangeMesh(int _damageCounter)
    //{
    //    switch(m_playerNumber)
    //    {
    //        case 1:
    //            switch (_damageCounter)
    //            {
    //                case 4:
    //                    break;
    //                case 3:
    //                    break;

    //            }
    //            break;
    //        case 2:
    //            break;
    //        case 3:
    //            break;
    //        case 4:
    //            break;
    //        default:
    //            break;
    //    }
    //}

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
