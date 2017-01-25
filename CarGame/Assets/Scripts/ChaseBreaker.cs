using UnityEngine;
using System.Collections;

public class ChaseBreaker : MonoBehaviour {
    Car m_car;

    public GameObject m_chaseBreaker;
    float m_spawnDist = -5;

    Quaternion m_rotation1 = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    Quaternion m_newRot;

    Vector3 m_playerPos;
    Vector3 m_playerDir;
    Vector3 m_spawnPos;

    float timer = 5.0f;
    float decayTimer = 0.0f;
    public int m_chaseBreakerCounter = 1;

    // Use this for initialization
    void Start()
    {
        m_car = gameObject.GetComponent<Car>();
        EventManager.m_instance.SubscribeToEvent(Events.Event.DS_RESET, ResetBreakers);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;


        if (ControllerManager.m_instance.m_useController)
        {
            switch (m_car.m_playerNumber)
            {
                case 1:
                    if (Input.GetButtonDown("P1-Circle(PS4)"))
                    {
                        ActivateChaseBreaker();
                    }
                    break;
                case 2:
                    if (Input.GetButtonDown("P2-Circle(PS4)"))
                    {
                        ActivateChaseBreaker();
                    }
                    break;
                case 3:
                    if (Input.GetButtonDown("P3-Circle(PS4)"))
                    {
                        ActivateChaseBreaker();
                    }
                    break;
                case 4:
                    if (Input.GetButtonDown("P4-Circle(PS4)"))
                    {
                        ActivateChaseBreaker();
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (m_car.m_playerNumber)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        ActivateChaseBreaker();
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.RightAlt))
                    {
                        ActivateChaseBreaker();
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.B))
                    {
                        ActivateChaseBreaker();
                    }
                    break;
                case 4:
                    if (Input.GetKeyDown(KeyCode.Comma))
                    {
                        ActivateChaseBreaker();
                    }
                    break;
                default:
                    break;
            }
        }
    }




    void ActivateChaseBreaker()
    {
        //find the players forward and direction
        m_playerPos = gameObject.transform.position;
        m_playerDir = gameObject.transform.forward * -1;

        //then calculate the offset for the chase breaker
        m_spawnPos = m_playerPos + m_playerDir * m_spawnDist;

        //calculate the new rotation and add a euler to offset any wrong changes
        m_newRot = gameObject.transform.rotation * m_rotation1;

        //spawn the damn thing

        if(timer <= 0.0f)
        {
            GameObject cb = (GameObject)Instantiate(m_chaseBreaker, m_spawnPos, m_newRot) as GameObject;
            decayTimer += Time.deltaTime;

            timer = 5.0f;
            m_chaseBreakerCounter++;

            //destroy any barriers that are greater than the count
            if (m_chaseBreakerCounter >= 3)
            {
                Destroy(cb);
            }
        }
        

    }

    public void ResetBreakers()
    {
        foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.name == "ChaseBreaker(Clone)")
            {
                Destroy(obj);
            }
        }

        m_chaseBreakerCounter = 1;
        timer = 5.0f;
    } 
}
