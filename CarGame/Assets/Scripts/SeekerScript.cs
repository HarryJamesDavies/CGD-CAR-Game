using UnityEngine;
using System.Collections;

public class SeekerScript : MonoBehaviour {

    Car m_car;
    Car m_hidercar;

    void Start ()
    {
        m_car = gameObject.GetComponent<Car>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "EmptyCar")
        {
            //if the car is a hider car
            m_hidercar = other.gameObject.GetComponent<Car>();

            //if ( get the hidertag variable m_hidertag == true)
            //{
            //    //if damage is required to be taken
            //    if (Input.GetKey(KeyCode.Z))
            //    {
            //        //take damage for the hider car 
            //        Debug.Log("Take Damage");
            //    }
            //}
        }
    }

    
}
