using UnityEngine;
using System.Collections;

namespace HF
{
    public class Health : MonoBehaviour
    {
        public float m_fMaxHealth = 100.0f;
        public float m_fCurrentHealth = 0.0f;
        public float m_fDamageAmount = 20.0f;

        public int m_iDamageCounter;

        public FoxAndHound m_foxAndHound;

        void Start()
        {
            m_fCurrentHealth = m_fMaxHealth;
            m_iDamageCounter = 0;

            m_foxAndHound = gameObject.GetComponent<FoxAndHound>();
        }

        void Update()
        {
            CheckHealth();
        }

        void CheckHealth()
        {
            if (m_fCurrentHealth <= 0.0f)
            {
                m_fCurrentHealth = 0.0f;
                m_foxAndHound.m_bDead = true;
            }
            else
            {
                m_foxAndHound.m_bDead = false;
            }
        }

        public void DecreaseHealth()
        {
            m_fCurrentHealth -= m_fDamageAmount;
        }

        public void ResetHealth()
        {
            m_fCurrentHealth = m_fMaxHealth;
        }
    }
}