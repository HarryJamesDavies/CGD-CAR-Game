using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HF
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager m_instance = null;

        public List<GameObject> m_playerCars;

        private int m_spawned;
        public int m_numberOfCars;

        public GameObject m_p1Prefab;
        public GameObject m_p2Prefab;
        public GameObject m_p3Prefab;
        public GameObject m_p4Prefab;

        public Transform m_p1Start;
        public Transform m_p2Start;
        public Transform m_p3Start;
        public Transform m_p4Start;

        void Start()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            m_playerCars = new List<GameObject>();
            m_spawned = 0;
            m_numberOfCars = 0;
        }

        void LateUpdate()
        {
            if (m_spawned < 1)
            {
                switch (ControllerManager.m_instance.m_joystickNumber)
                {
                    case 0:
                        m_numberOfCars = 4;
                        m_playerCars.Add((GameObject)Instantiate(m_p1Prefab, m_p1Start.position, m_p1Start.rotation));
                        m_playerCars.Add((GameObject)Instantiate(m_p2Prefab, m_p2Start.position, m_p2Start.rotation));
                        m_playerCars.Add((GameObject)Instantiate(m_p3Prefab, m_p3Start.position, m_p3Start.rotation));
                        m_playerCars.Add((GameObject)Instantiate(m_p4Prefab, m_p4Start.position, m_p4Start.rotation));
                        break;
                    case 1:
                        m_numberOfCars = 1;
                        m_playerCars.Add((GameObject)Instantiate(m_p1Prefab, m_p1Start.position, m_p1Start.rotation));
                        break;
                    case 2:
                        m_numberOfCars = 2;
                        m_playerCars.Add((GameObject)Instantiate(m_p1Prefab, m_p1Start.position, m_p1Start.rotation));
                        m_playerCars.Add((GameObject)Instantiate(m_p2Prefab, m_p2Start.position, m_p2Start.rotation));
                        break;
                    case 3:
                        m_numberOfCars = 3;
                        m_playerCars.Add((GameObject)Instantiate(m_p1Prefab, m_p1Start.position, m_p1Start.rotation));
                        m_playerCars.Add((GameObject)Instantiate(m_p2Prefab, m_p2Start.position, m_p2Start.rotation));
                        m_playerCars.Add((GameObject)Instantiate(m_p3Prefab, m_p3Start.position, m_p3Start.rotation));
                        break;
                    case 4:
                        m_numberOfCars = 4;
                        m_playerCars.Add((GameObject)Instantiate(m_p1Prefab, m_p1Start.position, m_p1Start.rotation));
                        m_playerCars.Add((GameObject)Instantiate(m_p2Prefab, m_p2Start.position, m_p2Start.rotation));
                        m_playerCars.Add((GameObject)Instantiate(m_p3Prefab, m_p3Start.position, m_p3Start.rotation));
                        m_playerCars.Add((GameObject)Instantiate(m_p4Prefab, m_p4Start.position, m_p4Start.rotation));
                        break;
                    default:
                        break;
                }
                m_spawned++;
            }
        }
    }
}