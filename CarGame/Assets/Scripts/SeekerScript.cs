using UnityEngine;
using System.Collections;

public class SeekerScript : MonoBehaviour
{
    int m_playerNumber;
    int m_damageApplied = 0;
    //bool m_lights = true;

    float timer = 2.0f;
    bool m_hiderHit = false;

    public string m_hiderTag;

    void Start()
    {
        TurnLightsOn();
        m_playerNumber = transform.parent.GetComponent<Car>().m_playerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_damageApplied > 0 && timer > 0.0f)
        {
            timer -= Time.deltaTime;
            //Debug.Log(timer);
  
        }
        else
        {
            m_damageApplied = 0;
            timer = 2.0f;
            TurnLightsOn();
        }

        //if using controllers do controller switch
        //else do keyboard switch
        if (ControllerManager.m_instance.m_useController)
        {
            //switch for checking player number and controller action press
            switch (m_playerNumber)
            {
                case 1:
                    if (Input.GetButtonDown("P1-X(PS4)"))
                    {
                        CheckItemsInRange();
                    }
                    break;
                case 2:
                    if (Input.GetButtonDown("P2-X(PS4)"))
                    {
                        CheckItemsInRange();
                    }
                    break;
                case 3:
                    if (Input.GetButtonDown("P3-X(PS4)"))
                    {
                        CheckItemsInRange();
                    }
                    break;
                case 4:
                    if (Input.GetButtonDown("P4-X(PS4)"))
                    {
                        CheckItemsInRange();
                    }
                    break;
                default:
                    Debug.Log("Controller Seeker Action Default");
                    break;
            }
        }
        else
        {
            //switch for checking player number and action button press
            switch (m_playerNumber)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        CheckItemsInRange();
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.RightAlt))
                    {
                        CheckItemsInRange();
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown("b"))
                    {
                        CheckItemsInRange();
                    }
                    break;
                case 4:
                    if (Input.GetKeyDown(","))
                    {
                        CheckItemsInRange();
                    }
                    break;
                default:
                    Debug.Log("Keyboard Seeker Action Default");
                    break;
            }
        }

    }

    void TurnLightsOn()
    {
        //if (!m_lights)
        //{
            foreach (GameObject lights in transform.parent.GetComponent<Movement>().m_lights)
            {
                lights.SetActive(true);
            }
        //    m_lights = true;
        //}
    }

    void TurnLightsOff()
    {
        //if (m_lights)
        //{
            foreach (GameObject lights in transform.parent.GetComponent<Movement>().m_lights)
            {
                lights.SetActive(false);
            }
        //    m_lights = false;
        //}
    }

    void DamageSeeker()
    {
        if (m_damageApplied == 0)
        {
            transform.parent.GetComponent<PlayerHealth>().decreasehealth();
            m_damageApplied++;
            TurnLightsOff();
        }
    }

    void DamageHider(Collider other)
    {
        if (m_damageApplied == 0)
        {
            other.gameObject.GetComponent<PlayerHealth>().decreasehealth();
            m_damageApplied++;
            TurnLightsOff();
        }
    }

    void CheckItemsInRange()
    {
        //check collider items within range of this object
        Collider[] m_itemsInRange = Physics.OverlapSphere(transform.parent.position, 2);

        //iterate through items and check for hider
        for (int i = 0; i < m_itemsInRange.Length; i++)
        {
            if (m_itemsInRange[i].tag == m_hiderTag)
            {
                m_hiderHit = true;
                DamageHider(m_itemsInRange[i]);
            }
        }

        //if hider hasn't been hit, damage the seeker
        if (!m_hiderHit)
        {
            DamageSeeker();
        }
        else
        {
            m_hiderHit = false;
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    //if damage is required to be taken
    //    if (ControllerManager.m_instance.m_useController)
    //    {
    //        if (Input.GetButtonDown("P" + m_playerNumber + ("-X(PS4)")))
    //        {
    //            if (m_hiderTag == other.gameObject.tag)
    //            {
    //                //take damage for the hider car
    //                Debug.Log("Hider Take Damage");
    //                other.gameObject.GetComponent<PlayerHealth>().decreasehealth();
    //            }
    //            else
    //            {
    //                //seeker take damage
    //                Debug.Log("Seeker Take Damage");
    //                transform.GetComponent<PlayerHealth>().decreasehealth();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (Input.GetKeyDown(KeyCode.V))
    //        {
                
    //                if (m_hiderTag == other.gameObject.tag)
    //                {                     
    //                    //take damage for the hider car
    //                    Debug.Log("Hider Take Damage");
    //                    DamageHider(other);
                    
    //                }
    //                else
    //                {
    //                    //seeker take damage                       
    //                    Debug.Log("Seeker Take Damage");
    //                    DamageSeeker();
                       
    //                }
                
            
    //        }
    //    }

    //}
}

