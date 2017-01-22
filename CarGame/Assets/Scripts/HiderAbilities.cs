using UnityEngine;
using System.Collections;

public class HiderAbilities : MonoBehaviour {

    [SerializeField]
    GameObject m_decoyReference;

    GameObject m_decoy;

	// Use this for initialization
	void Start () {
        m_decoyReference = gameObject;
        
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Z))
        {
            SpawnDecoy();
        }
	}

    void SpawnDecoy()
    {
        m_decoy = Instantiate(m_decoyReference);
        
        Destroy(m_decoy.GetComponentInChildren<Camera>().gameObject);
        Destroy(m_decoy.GetComponent<Car>());
        Destroy(m_decoy.GetComponent<Rigidbody>());
        Destroy(m_decoy.GetComponent<Collider>());
        Destroy(m_decoy.GetComponent<Movement>());
        Destroy(m_decoy.GetComponent<Hider>());
        Destroy(m_decoy.GetComponent<HiderAbilities>());
    }
}
