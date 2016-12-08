using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour
{
    public GameObject m_spawnPoint;
    public Quaternion m_spawnRotation;

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Car"))
        {
            collision.transform.position = m_spawnPoint.transform.position;
            collision.transform.rotation = m_spawnRotation;
        }
    }
}
