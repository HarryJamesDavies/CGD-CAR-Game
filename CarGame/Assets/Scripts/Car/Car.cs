using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace HF
{
    public class Car : MonoBehaviour
    {

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

        public RawImage chasebreaker1;
        public RawImage chasebreaker2;
        public RawImage chasebreaker3;

        public GameObject m_100HealthModel;
        public GameObject m_80HealthModel;
        public GameObject m_60HealthModel;
        public GameObject m_40HealthModel;
        public GameObject m_20HealthModel;
        public GameObject m_0HealthModel;
        public GameObject m_currentModel;

        ChaseBreaker chaseBreakerInstance;

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

            //so we can access the cb counter
            chaseBreakerInstance = GetComponent<ChaseBreaker>();
        }

        void Update()
        {
            //updates the fuel text for the runner, and sets others to false
            if (m_hider)
            {
                m_fuel.enabled = true;
                m_fuel.text = "Fuel: " + (int)GetComponent<Movement>().fuel;
                chasebreaker1.enabled = true;
                chasebreaker2.enabled = true;
                chasebreaker3.enabled = true;
            }
            else
            {
                m_fuel.enabled = false;
                chasebreaker1.enabled = false;
                chasebreaker2.enabled = false;
                chasebreaker3.enabled = false;
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

            checkChaseBreakerNumber();
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
            gameObject.AddComponent<ChaseBreaker>();
            Destroy(m_seekerCone);

            for (int i = 0; i <= transform.childCount - 1; i++)
            {
                string tempTag = transform.GetChild(i).gameObject.tag;

                if (tempTag == "CarModel")
                {
                    m_currentModel = transform.GetChild(i).gameObject;
                }
            }

            m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_100HealthModel.GetComponent<MeshFilter>().sharedMesh;
        }

        public void ResetHider()
        {
            m_hider = false;
            GetComponent<Movement>().m_power = 0.005f;
            GetComponent<PlayerHealth>().ResetHealth();
            Destroy(GetComponent<Hider>());

            for (int i = 0; i <= transform.childCount - 1; i++)
            {
                string tempTag = transform.GetChild(i).gameObject.tag;

                if (tempTag == "CarModel")
                {
                    m_currentModel = transform.GetChild(i).gameObject;
                }
            }

            m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_100HealthModel.GetComponent<MeshFilter>().sharedMesh;
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

        public void ChangeMesh(int _damageCounter)
        {
            for (int i = 0; i <= transform.childCount - 1; i++)
            {
                string tempTag = transform.GetChild(i).gameObject.tag;

                if (tempTag == "CarModel")
                {
                    m_currentModel = transform.GetChild(i).gameObject;
                }
            }

            switch (_damageCounter)
            {
                case 1:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_80HealthModel.GetComponent<MeshFilter>().sharedMesh;
                    break;
                case 2:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_60HealthModel.GetComponent<MeshFilter>().sharedMesh;
                    break;
                case 3:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_40HealthModel.GetComponent<MeshFilter>().sharedMesh;
                    break;
                case 4:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_20HealthModel.GetComponent<MeshFilter>().sharedMesh;
                    break;
                case 5:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_0HealthModel.GetComponent<MeshFilter>().sharedMesh;
                    break;
                default:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_100HealthModel.GetComponent<MeshFilter>().sharedMesh;
                    break;
            }
        }

        //update the chasebreaker UI
        public void checkChaseBreakerNumber()
        {
            if (chaseBreakerInstance.m_chaseBreakerCounter == 2)
            {
                chasebreaker1.enabled = false;
            }

            if (chaseBreakerInstance.m_chaseBreakerCounter == 3)
            {
                chasebreaker1.enabled = false;
                chasebreaker2.enabled = false;
            }

            if (chaseBreakerInstance.m_chaseBreakerCounter >= 4)
            {
                chasebreaker1.enabled = false;
                chasebreaker2.enabled = false;
                chasebreaker3.enabled = false;
            }
        }
    }
}
