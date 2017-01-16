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

        if (Input.GetKeyDown(KeyCode.V))
        {
            Collider[] m_itemsInRange = Physics.OverlapSphere(transform.parent.position, 2);

            for(int i = 0; i< m_itemsInRange.Length; i++)
            { 
                if(m_itemsInRange[i].tag == m_hiderTag)
                {
                    m_hiderHit = true;
                    DamageHider(m_itemsInRange[i]);
                 
                }
            }
           
            if(!m_hiderHit)
            {
                DamageSeeker();
        
            }
            else
            {
                m_hiderHit = false;
            }


        }
        else
        {
            //check for the controller input
            if (ControllerManager.m_instance.m_useController)
            {
                if (Input.GetButtonDown("P" + m_playerNumber + ("-X(PS4)")))
                {
                    Collider[] m_itemsInRange = Physics.OverlapSphere(transform.parent.position, 2);

                    for (int i = 0; i < m_itemsInRange.Length; i++)
                    {
                        if (m_itemsInRange[i].tag == m_hiderTag)
                        {
                            m_hiderHit = true;
                            DamageHider(m_itemsInRange[i]);

                        }
                    }

                    if (!m_hiderHit)
                    {
                        DamageSeeker();

                    }
                    else
                    {
                        m_hiderHit = false;
                    }

                }

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

