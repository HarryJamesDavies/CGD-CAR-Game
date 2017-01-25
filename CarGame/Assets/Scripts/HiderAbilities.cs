using UnityEngine;
using System.Collections;

public class HiderAbilities : MonoBehaviour {

    [SerializeField]
    GameObject m_oilReference;

    GameObject m_oil;

    bool m_timer;

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
        m_oil = Instantiate(m_oilReference);
        // m_oil.transform.position = m_spawnPos;
        m_oil.transform.position = gameObject.transform.Find("BackSpawn").transform.position;
      }

    IEnumerator OilTimer()
    {
        m_timer = true;
        yield return new WaitForSeconds(m_oilWaitTime);
        SpawnOil();
        m_timer = false;
    }
}
