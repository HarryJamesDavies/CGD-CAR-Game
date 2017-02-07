using UnityEngine; 
using System.Collections;

namespace HF
{
    public class Boulder : MonoBehaviour
    {
        public float minTime = 0.25f;
        public float maxTime = 0.5f;
        public float minX = -150.0f;
        public float maxX = 150.0f;
        public float topY = 20.0f;
        public float minZ = -150.0f;
        public float maxZ = 150.0f;

        public int count = 10000;
        public GameObject boulder;

        public bool StartEruption = false;
        public float EruptionTimer = 30.0f;

        public ParticleSystem Eruption;

        public bool doSpawn = true;

        void Start()
        {
            //StartCoroutine(Spawner());
        }

        void Update()
        {
            if (TwistManager.m_instance.m_currentTwist == TwistManager.Twists.eruption)
            {
                if (!StartEruption)
                {
                    StartEruption = true;
                    StartCoroutine(Spawner());
                }

                EruptionTimer -= Time.deltaTime;

                if (EruptionTimer < 0)
                {
                    count = 0;
                }
            }
            else
            {
                StartEruption = false;
            }
    
            //if (StartEruption == true)
            //{
            //    StartCoroutine(Spawner());
            //    EruptionTimer -= Time.deltaTime;
            //}
            //if (EruptionTimer < 0)
            //{
            //    count = 0;
            //}
        }

        IEnumerator Spawner()
        {
            while (doSpawn && count > 0)
            {
                //Eruption.Play();
                Vector3 v = new Vector3(Random.Range(minX, maxX), topY, Random.Range(minZ, maxZ));
                Instantiate(boulder, v, Random.rotation);
                count--;
                yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            }
        }
    }
}