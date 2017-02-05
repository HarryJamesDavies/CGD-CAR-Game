using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HF
{
    public class TeleporterManager : MonoBehaviour
    {

        bool m_teleportersActive;

        List<GameObject> m_teleporters = new List<GameObject>();

        [SerializeField]
        GameObject m_teleporterPrefab;

        public static TeleporterManager m_instance;

        void Start()
        {
            
            if (m_instance)
            {
                Destroy(this);
            }
            else if (m_instance == null)
            {
                m_instance = this;
            }

            //GameObject[] teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
            //for (int iter = 0; teleporters.Length > iter; iter++)
            // {
            //     m_teleporters.Add(teleporters[iter]);
            //}
            m_teleportersActive = false;
            m_teleporterPrefab = TwistManager.m_instance.GetTeleporterPrefab();
        }


        void Update()
        {
            if(TwistManager.m_instance.m_currentTwist == TwistManager.Twists.teleporters)
            {
                if(m_teleportersActive == false)
                {
                    SpawnTeleporters();
                }
            }
            else if(TwistManager.m_instance.m_currentTwist != TwistManager.Twists.teleporters)
            {
                if (m_teleportersActive == true)
                {
                    DestroyTeleporters();
                }
            }
        }

        void SpawnTeleporters()
        {
            m_teleportersActive = true;
            
            for (int iter = 0; iter < TwistManager.m_instance.m_teleporterPositions.transform.childCount; iter++)
            {
                GameObject temp = Instantiate(m_teleporterPrefab);
                temp.transform.position = TwistManager.m_instance.m_teleporterPositions.transform.GetChild(iter).transform.position;
                m_teleporters.Add(temp);
            }
            
        }

        void DestroyTeleporters()
        {
            m_teleportersActive = false;
            for (int iter = 0; iter < m_teleporters.Count; iter++)
            {
                Destroy(m_teleporters[iter]);
            }
            m_teleporters.Clear();
        }

        public void Teleport(GameObject player)
        {
            int location = Random.Range(0, m_teleporters.Count);

            player.transform.position = m_teleporters[location].transform.position;
        }
    }
}