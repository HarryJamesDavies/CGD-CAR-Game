using UnityEngine;
using System.Collections;

public class SeekerScript : MonoBehaviour
{
    int m_playerNumber;

    public string m_hiderTag;

    void Start()
    {
        m_playerNumber = transform.parent.GetComponent<Car>().m_playerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Seeker: " + m_playerNumber);
    }

    void OnTriggerStay(Collider other)
    {
        //if damage is required to be taken
        if (ControllerManager.m_instance.m_useController)
        {
            if (Input.GetButtonDown("P" + m_playerNumber + ("-X(PS4)")))
            {
                if (m_hiderTag == other.gameObject.tag)
                {
                    //take damage for the hider car
                    Debug.Log("Hider Take Damage");
                    other.gameObject.GetComponent<PlayerHealth>().decreasehealth();
                }
                else
                {
                    //seeker take damage
                    Debug.Log("Seeker Take Damage");
                    transform.GetComponent<PlayerHealth>().decreasehealth();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (m_hiderTag == other.gameObject.tag)
                {
                    //take damage for the hider car
                    Debug.Log("Hider Take Damage");
                    other.gameObject.GetComponent<PlayerHealth>().decreasehealth();
                    //m_damageApplied++;
                }
                else
                {
                    //seeker take damage
                    Debug.Log("Seeker Take Damage");
                    transform.parent.GetComponent<PlayerHealth>().decreasehealth();
                }
            }
        }

    }
}
