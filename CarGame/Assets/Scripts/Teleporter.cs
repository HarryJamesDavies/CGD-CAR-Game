using UnityEngine;
using System.Collections;

namespace HF
{
    public class Teleporter : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void OnTriggerEnter(Collider _collider)
        {
            if (_collider.tag == "Player1" || _collider.tag == "Player2"
                || _collider.tag == "Player3" || _collider.tag == "Player4")
            {
                if (_collider.gameObject.GetComponent<Car>().m_teleportCooldown == false)
                {
                    TeleporterManager.m_instance.Teleport(_collider.gameObject);
                    _collider.gameObject.GetComponent<Car>().StartCooldown();
                }
        }
        }
    }
}