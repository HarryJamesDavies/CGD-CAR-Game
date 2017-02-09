using UnityEngine;
using System.Collections;

namespace HF
{
    public class FoxAndHound : MonoBehaviour
    {
        //key player identifiers
        public int m_iPlayerNumber;

        //detection variables
        public bool m_bFox;
        public bool m_bHound;
        public bool m_bDead;

        void Start()
        {
            m_iPlayerNumber = 0;

            m_bFox = false;
            m_bHound = false;
        }

        void Update()
        {

        }

        public void SetFox()
        {
            m_bFox = true;
            gameObject.AddComponent<Health>();
        }

        public void SetHound()
        {
            m_bHound = true;
            gameObject.AddComponent<Health>();
        }

        public void ResetFox()
        {
            m_bFox = false;
            Destroy(GetComponent<Health>());
        }

        public void ResetHound()
        {
            m_bHound = false;
            Destroy(GetComponent<Health>());
        }
    }
}
