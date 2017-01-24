using UnityEngine;
using System.Collections;

public class HiderAbilities : MonoBehaviour {

    [SerializeField]
    GameObject m_oilReference;

    GameObject m_oil;

	// Use this for initialization
	void Start () {

        m_oilReference = GameObject.Find("OilSlick");
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Z))
        {
            SpawnOil();
        }
	}

    void SpawnOil()
    {
        m_oil = Instantiate(m_oilReference);
        m_oil.transform.position = gameObject.transform.FindChild("BackSpawn").transform.position;
    }
}
