using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    public string m_tag;

    private Camera m_playerCam;
    public GameObject m_seekerParam;

    public bool m_hider;
    public bool m_seeker;

    void Awake()
    {
        m_tag = gameObject.tag;
        SetCamera();

        if (m_tag == "Player1")
        {
            m_hider = true;
            m_seeker = false;
            gameObject.AddComponent<Hider>();
        }
        else if (m_tag == "Player2")
        {
            m_hider = false;
            m_seeker = true;
            SetSeekerParameters();
        }
        else
        {
            m_hider = false;
            m_seeker = false;
        }
    }
	
	void Update ()
    {
	    
	}

    void SetSeekerParameters()
    {
        //add the seeker cone and set its parent
        GameObject m_seekerCone = Instantiate(m_seekerParam, transform.position, Quaternion.Euler(0.0f,90.0f,90.0f)) as GameObject;
        m_seekerCone.transform.parent = gameObject.transform;
    }

    void SetCamera()
    {
        Debug.Log("Number of Cars: " + PlayerManager.m_instance.m_numberOfCars);

        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            string tempTag = transform.GetChild(i).gameObject.tag;

            if (tempTag == "CarCamera")
            {
                Debug.Log("Found car cam.");
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

    void OnCollisionEnter(Collider _collider)
    {
        if(_collider.gameObject.GetComponent<Car>().m_hider == true 
            || _collider.gameObject.GetComponent<Car>().m_seeker == true)
        {
            if(_collider.gameObject.GetComponent<Car>().m_seeker == true && m_hider == true)
            {
                if (StateManager.m_instance.gameManager.GetComponent<GameManager>().m_phaseDriveAndSeek 
                    == GameManager.DriveAndSeekPhases.seek)
                {
                    StateManager.m_instance.gameManager.GetComponent<GameManager>().hiderCaught();
                }

                //some sort of logic to add points to a score
            }
        }
    }
}
