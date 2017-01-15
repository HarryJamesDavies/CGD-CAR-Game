using UnityEngine;
using System.Collections;

public class SeekerScript : MonoBehaviour {

    Car m_car;
    Car m_hidercar;

    int m_playerNumber;

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
        /*if (other.gameObject.tag != "EmptyCar")
        {
            //if the car is a hider car
            m_hidercar = other.gameObject.GetComponent<Car>();

            if (get the hidertag variable m_hidertag == true)
            {
                if (ControllerManager.m_instance.m_useController)
                {
                    int _playerNumber = 0;

                    switch (gameObject.tag)
                    {
                        case "Player1":
                            _playerNumber = 1;
                            break;
                        case "Player2":
                            _playerNumber = 2;
                            break;
                        case "Player3":
                            _playerNumber = 3;
                            break;
                        case "Player4":
                            _playerNumber = 4;
                            break;
                        default:
                            break;
                    }

                    if (Input.GetButtonDown("P" + _playerNumber + ("-X(PS4)")))
                    {
                        //take damage for the hider car 
                        Debug.Log("Take Damage");
                    }
                }
                else
                {
                    //if damage is required to be taken
                    if (Input.GetKey(KeyCode.Z))
                    {
                        //take damage for the hider car 
                        Debug.Log("Take Damage");
                    }
                }
            }
        }*/
    }
}
