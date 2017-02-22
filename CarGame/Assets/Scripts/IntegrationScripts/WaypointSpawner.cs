using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HF
{
    public class WaypointSpawner : MonoBehaviour
    {
        List<GameObject> m_wayPoints = new List<GameObject>();

        GameObject m_wayPointReference;

        GameObject m_wayPointHolder;

        GameObject m_WayPoint;

        bool m_timer;

        float m_oilWaitTime = 5.0f;

        float m_minDistance = 30.0f;

        void Start()
        {

            m_wayPointReference = (GameObject)Resources.Load("OilSlick");
            m_wayPointHolder = Instantiate((GameObject)Resources.Load("OilHolder"));
            m_timer = true;
        }


        void Update() //check to see if the event has started, check to see if the car needs to start a timer to drop oil...
        {
            EventManager.m_instance.SubscribeToEvent(Events.Event.DS_RUNNING, StartWayPointSpawns);
            //EventManager.m_instance.SubscribeToEvent(Events.Event.DS_CHASE, StartOilSpawns);

            if (m_timer == false)
            {
                StartCoroutine(WayPointTimer());
            }

            if (GameModeManager.m_instance.m_currentMode == GameModeManager.GameModeState.FREEROAM) //... check to see if I should be alive
            {
                Destroy(m_wayPointHolder);
                //Destroy(m_oilReference);
                Destroy(gameObject.GetComponent<HiderAbilities>());
            }

        }

        void StartWayPointSpawns() //this is the event trigger which flips a bool to allow the car to start leaking oil
        {
            m_timer = false;
        }

        void SpawnWayPointCheck()
        {
            if (m_wayPoints.Count <= 0)
            {
                SpawnWayPoint();
            }
            else if (m_wayPoints.Count > 0)
            {
                Vector3 previousPosition = m_wayPoints[m_wayPoints.Count - 1].transform.position;

                float currentDistance = Vector3.Distance(previousPosition, gameObject.transform.position);

                if (currentDistance >= m_minDistance)
                {
                    SpawnWayPoint();
                }
            }
        }

        void SpawnWayPoint() //this spawns the oil by the back spawn
        {

            m_WayPoint = Instantiate(m_wayPointReference);
            // m_oil.transform.position = m_spawnPos;
            m_WayPoint.transform.position = gameObject.transform.Find("BackSpawn").transform.position;
            m_WayPoint.transform.parent = m_wayPointHolder.transform;
            m_wayPoints.Add(m_WayPoint);
        }

        IEnumerator WayPointTimer() //this is a timer to seperate out the oil slick drops
        {
            m_timer = true;
            yield return new WaitForSeconds(m_oilWaitTime);
            SpawnWayPointCheck();
            m_timer = false;
        }
    }
}