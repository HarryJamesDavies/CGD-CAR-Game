using UnityEngine;
using System.Collections;

public class OilSlickScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider _collider)
    {
        if(_collider.tag == "Player1" || _collider.tag == "Player2" ||
            _collider.tag == "Player3"|| _collider.tag == "Player4")
        {
            //Vector3 vector = _collider.gameObject.GetComponent<Rigidbody>(). / 2;
            if (_collider.gameObject.GetComponent<HiderAbilities>() != null)
            {

            }
            else
            {
                float force;
                int random = Random.Range(0, 4);
                Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);

                //THIS SYSTEM IS BAD, IT WILL NEED TO BE REPLACED WITH ONE MUCH SMARTER WHEN WE UPGRADE THE MOVEMENT SYSTEM
                if (random == 1)
                {
                    force = 80.0f;
                    vector = new Vector3(0.0f, 0.0f, force);
                }
                else if (random == 2)
                {
                    force = -80.0f;
                    vector = new Vector3(0.0f, 0.0f, force);
                }
                else if (random == 3)
                {
                    force = 80.0f;
                    vector = new Vector3(force, 0.0f, 0.0f);
                }
                else if (random == 4)
                {
                    force = -80.0f;
                    vector = new Vector3(force, 0.0f, 0.0f);
                }

                _collider.gameObject.GetComponent<Rigidbody>().AddForce(vector);
            }
        }
    }
}
