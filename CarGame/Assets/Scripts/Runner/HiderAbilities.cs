using UnityEngine;
using System.Collections;

namespace HF
{
    public class HiderAbilities : MonoBehaviour
    {

        GameObject m_oilReference;

        GameObject m_oilHolder;

        GameObject m_oil;

        bool m_timer;

        float m_oilWaitTime = 5.0f;

        // Use this for initialization
        void Start()
        {

            m_oilReference = (GameObject)Resources.Load("OilSlick");
            m_oilHolder = Instantiate((GameObject)Resources.Load("OilHolder"));
            m_timer = false;
        }

        // Update is called once per frame
        void Update()
        {

            if (m_timer == false)
            {
                StartCoroutine(OilTimer());
            }

            if (GameModeManager.m_instance.m_currentEvent == GameModeManager.GameModeState.FREEROAM)
            {
                Destroy(m_oilHolder);
                Destroy(m_oilReference);
                Destroy(gameObject.GetComponent<HiderAbilities>());
            }

        }

        void SpawnOil()
        {
            m_oil = Instantiate(m_oilReference);
            // m_oil.transform.position = m_spawnPos;
            m_oil.transform.position = gameObject.transform.Find("BackSpawn").transform.position;
            m_oil.transform.parent = m_oilHolder.transform;
        }

        IEnumerator OilTimer()
        {
            m_timer = true;
            yield return new WaitForSeconds(m_oilWaitTime);
            SpawnOil();
            m_timer = false;
        }
    }
}