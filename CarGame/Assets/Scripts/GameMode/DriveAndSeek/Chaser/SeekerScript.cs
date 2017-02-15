using UnityEngine;
using System.Collections;

namespace HF
{
    public class SeekerScript : MonoBehaviour
    {
        public int m_playerNumber;
        int m_damageApplied = 0;
        //bool m_lights = true;

        float timer = 2.0f;

        public string m_hiderTag;
        public string m_chaserTag;

        void Start()
        {
            TurnLightsOn();
            m_playerNumber = GetComponent<Car>().m_playerNumber;
            m_chaserTag = gameObject.tag;
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

             

        }

        void OnTriggerEnter(Collider _collider)
        {
            CheckItemsInRange(_collider);
        }

        void TurnLightsOn()
        {
            //if (!m_lights)
            //{
            foreach (GameObject lights in gameObject.GetComponent<Movement>().m_lights)
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
            foreach (GameObject lights in gameObject.GetComponent<Movement>().m_lights)
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
                GetComponent<PlayerHealth>().decreasehealth();
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

        void CheckItemsInRange(Collider _collider)
        {
            //if hider hasn't been hit, damage the seeker, unless it's the ground or something random
            if (_collider.tag == m_hiderTag)
            {
                DamageHider(_collider);
            }
            else if(_collider.tag == m_chaserTag)
            {
                DamageSeeker();
            }
            else
            {
                
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
}