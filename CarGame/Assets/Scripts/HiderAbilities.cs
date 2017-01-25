using UnityEngine;
using System.Collections;

public class HiderAbilities : MonoBehaviour {

    [SerializeField]
    GameObject m_oilReference;

    GameObject m_oil;

    bool m_timer;

    //Coroutine oilCoroutine;

    float m_oilWaitTime =  5.0f;

	// Use this for initialization
	void Start () {

        m_oilReference = GameObject.Find("OilSlick");
        m_timer = false;
    }
	
	// Update is called once per frame
	void Update () {

	    if(m_timer == false)
        {
            StartCoroutine(OilTimer());
        }

	}

    void SpawnOil()
    {
        RaycastHit[] hits = Physics.RaycastAll(gameObject.transform.position, new Vector3(0.0f, -10.0f, -10.0f));
        Debug.DrawRay(gameObject.transform.position, Vector3.back);

        for(int iter = 0; hits.Length > iter; iter++)
        {
            if(hits[iter].transform.gameObject.tag == "Level")
            {
                m_oil = Instantiate(m_oilReference);
                m_oil.transform.position = hits[iter].transform.position;
            }
            else
            {
                Debug.Log("Blergygsgews");
            }
        }

      
        //m_oil.transform.position = gameObject.transform.FindChild("BackSpawn").transform.position;
    }

    IEnumerator OilTimer()
    {
        m_timer = true;
        yield return new WaitForSeconds(m_oilWaitTime);
        SpawnOil();
        m_timer = false;
    }
}
