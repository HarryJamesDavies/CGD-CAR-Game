﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//===================== Kojima Drive - Half-Full Games 2017 ====================//
//
// Author(s): ADAM MOOREY, MATTHEW WYNTER, HARRY DAVIES
// Purpose: Main car script for the key player car requirements
// Namespace: HF
//
//===============================================================================//

namespace HF
{
    public class Car : MonoBehaviour
    {
        //key player identifiers
        public string m_tag;
        public int m_playerNumber;
        public float fade = 1.0f;
        public float speed = 0.1f;

        private Camera m_playerCam;
        //public GameObject m_seekerParam;

        //detection variables
        public bool m_runner;
        public bool m_chaser;
        public bool m_isDead;
        public bool m_demoText;
        //private GameObject m_seekerCone;

        //UI image variables
        public RawImage m_image;
        public Texture m_chaserTitle;
        public Texture m_runnerTitle;
        public Text demoText;
        public RectTransform rect;

        //chase breaker images
        public RawImage chasebreaker1;
        public RawImage chasebreaker2;
        public RawImage chasebreaker3;

        public bool m_teleportCooldown;

        ChaseBreaker chaseBreakerInstance;

        void Awake()
        {
            m_teleportCooldown = false;
            m_tag = gameObject.tag;

            SetCamera();

            m_runner = false;
            m_chaser = false;

            //assigns the player numbers based on gameobjects tag
            switch (m_tag)
            {
                case "Player1":
                    m_playerNumber = 1;
                    break;
                case "Player2":
                    m_playerNumber = 2;
                    break;
                case "Player3":
                    m_playerNumber = 3;
                    break;
                case "Player4":
                    m_playerNumber = 4;
                    break;
                default:
                    Debug.Log("Error: Player tag doesn't match.");
                    break;
            }				

            //subscribe the text setup to the necessary events
            EventManager.m_instance.SubscribeToEvent(Events.Event.DS_SETUP, SetupText);
            EventManager.m_instance.SubscribeToEvent(Events.Event.DS_RUNNING, RunningText);
            //so we can access the cb counter
            chaseBreakerInstance = GetComponent<ChaseBreaker>();

            //add a player exp to the scene

            //if(gameObject.tag == "Player1")
            //{
            //   GetComponent<PlayerExp>().addExptoCurrentLevel(500);
            //}
        }

        void Update()
        {
            //updates the fuel text for the runner, and sets others to false
            if (m_runner)
            {
                chasebreaker1.enabled = true;
                chasebreaker2.enabled = true;
                chasebreaker3.enabled = true;
            }
            else
            {
                //m_fuel.enabled = false;
                chasebreaker1.enabled = false;
                chasebreaker2.enabled = false;
                chasebreaker3.enabled = false;
            }

            //have we entered an xp event
            if(m_demoText == true)
            {
                //add fancy effects to the text componenet
                demoText.color -= new Color(0, 0, 0, fade * Time.deltaTime);
                rect.position += Vector3.up * Time.deltaTime * speed;

                Color demoTextColour = demoText.color;
                Color fadeColour = new Color(0, 0, 0, 0);

                if(demoTextColour.a < fadeColour.a)
                {
                    //reset the xp text 
                    rect.localPosition = new Vector3(8, -6, -3);
                    demoText.text = "";
                    demoText.color = new Color(0, 0, 0, 1);
                    m_demoText = false;
                }
            }

            //checks when the out of fuel image is showing or not
            if (GetComponent<Movement>().m_controls == true)
            {
                m_image.enabled = false;
            }

            //if the twist is speedup increase the vehicle power
            if (TwistManager.m_instance.m_currentTwist == TwistManager.Twists.speedUp)
            {
                GetComponent<Movement>().m_power = 0.01f;
            }
            else
            {
                if (m_runner)
                {
                    GetComponent<Movement>().m_power = 0.006f;
                }
                else
                {
                    GetComponent<Movement>().m_power = 0.005f;
                }
            }

            checkChaseBreakerNumber();
        }

        public void SetChaser(string _runnerTag)
        {
            m_chaser = true;
            gameObject.AddComponent<SeekerScript>();
            //m_seekerCone = Instantiate(m_seekerParam, transform.position, Quaternion.Euler(0.0f, 90.0f, 90.0f)) as GameObject;
            //m_seekerCone.transform.parent = gameObject.transform;

            //Instantiate(chasebreakerUI, transform.position, transform.rotation);

            GetComponent<SeekerScript>().m_hiderTag = _runnerTag;
            Destroy(GetComponent<ChaseBreaker>());
        }

        public void SetRunner()
        {
            //set the runners base variables
            m_runner = true;
            GetComponent<Movement>().m_power = 0.006f;
            gameObject.AddComponent<HiderAbilities>();
            gameObject.GetComponent<ChaseBreaker>().enabled = true;
        }

        public void ResetChaser()
        {
            //reset the seekers current variables
            m_chaser = false;
            Destroy(gameObject.GetComponent<SeekerScript>());
            GetComponent<PlayerHealth>().ResetHealth();
            gameObject.AddComponent<ChaseBreaker>();
            //Destroy(m_seekerCone);
        }

        public void ResetRunner()
        {
            //reset the runner variables
            m_runner = false;
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
            //find the camera component tagged carcamera
            for (int i = 0; i <= transform.childCount - 1; i++)
            {
                string tempTag = transform.GetChild(i).gameObject.tag;

                if (tempTag == "CarCamera")
                {
                    m_playerCam = transform.GetChild(i).GetComponent<Camera>();
                }
            }

            //change the camera render dependent on the number of players spawned
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
            //set the intital role text based on players role
            if (m_runner)
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

        void RunningText()
        {
            m_image.enabled = false;
        }

        void OnDestroy()
        {
            //unsubscribe functions that aren't needed anymore
            EventManager.m_instance.UnsubscribeToEvent(Events.Event.DS_SETUP, SetupText);
            EventManager.m_instance.UnsubscribeToEvent(Events.Event.DS_RUNNING, RunningText);
            //EventManager.m_instance.UnsubscribeToEvent(Events.Event.DS_CHASING, ShowFuel);
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

        public void StartCooldown()
        {
            StartCoroutine(TeleportCooldown());
        }

        IEnumerator TeleportCooldown()
        {
            m_teleportCooldown = true;
            yield return new WaitForSeconds(10);
            m_teleportCooldown = false;
        }
    }
}
