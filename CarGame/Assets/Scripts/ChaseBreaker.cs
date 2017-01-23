using UnityEngine;
using System.Collections;

public class ChaseBreaker : MonoBehaviour {

    Car m_car;

    public GameObject m_chaseBreaker;
    float m_spawnDist = -5;

    Quaternion m_rotation1 = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    Quaternion m_newRot;

    Vector3 m_playerPos;
    Vector3 m_playerDir;
    Vector3 m_spawnPos;


    // Use this for initialization
    void Start ()
    {
        m_car = gameObject.GetComponent<Car>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    //if(Input.GetKeyDown(KeyCode.P))
     //   {
     //       ActivateChaseBreaker();
     //   }
	}

    void ActivateChaseBreaker()
    {
        //find the players forward and direction
        m_playerPos = gameObject.transform.position;
        m_playerDir = gameObject.transform.forward * -1;

        //then calculate the offset for the chase breaker
        m_spawnPos = m_playerPos + m_playerDir * m_spawnDist;

        //calculate the new rotation and add a euler to offset any wrong changes
        m_newRot = gameObject.transform.rotation * m_rotation1;

        //spawn the damn thing
        GameObject cb = (GameObject)Instantiate(m_chaseBreaker, m_spawnPos, m_newRot) as GameObject;

    }
}
