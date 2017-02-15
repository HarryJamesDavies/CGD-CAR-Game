using UnityEngine;
using System.Collections;

namespace HF
{

    public class Chaser : MonoBehaviour
    {
        public int m_playerNumber;
        int m_damageApplied = 0;
        //bool m_lights = true;

        float timer = 2.0f;

        public string m_hiderTag;
        public string m_chaserTag;

        void Start()
        {
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
            }



        }

        void OnTriggerEnter(Collider _collider)
        {
            CheckItemsInRange(_collider);
        }
        void DamageSeeker()
        {
            if (m_damageApplied == 0)
            {
                GetComponent<PlayerHealth>().decreasehealth();
                m_damageApplied++;
            }
        }

        void DamageHider(Collider other)
        {
            if (m_damageApplied == 0)
            {
                other.gameObject.GetComponent<PlayerHealth>().decreasehealth();
                m_damageApplied++;
            }
        }

        void CheckItemsInRange(Collider _collider)
        {
            //if hider hasn't been hit, damage the seeker, unless it's the ground or something random
            if (_collider.tag == m_hiderTag)
            {
                DamageHider(_collider);
            }
            else if (_collider.tag == m_chaserTag)
            {
                DamageSeeker();
            }
            else
            {

            }

        }
    }
}