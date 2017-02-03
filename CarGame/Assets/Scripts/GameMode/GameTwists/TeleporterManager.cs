using UnityEngine;
using System.Collections;

namespace HF
{
    public class TeleporterManager : MonoBehaviour
    {

        GameObject[] teleporters;

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

            teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
        }


        void Update()
        {

        }

        public void Teleport(GameObject player)
        {
            int location = Random.Range(0, teleporters.Length);

            player.transform.position = teleporters[location].transform.position;
        }
    }
}