using UnityEngine;
using System.Collections;

namespace HF
{
    public class ProjectBoulders : MonoBehaviour
    {
        public Rigidbody boulder;
        private Rigidbody instantiatedboulder;
        public float speed = 10;

        public bool isErupting = true;
        public float erupttimer = 10.0f;

        public ParticleSystem Volcano;

        void Start()
        {
            Volcano.Play();
        }

        void Update()
        {
            if (TwistManager.m_instance.m_currentTwist == TwistManager.Twists.eruption)
            {
                erupttimer -= Time.deltaTime;
                if (isErupting == true)
                {
                    instantiatedboulder = Instantiate(boulder, transform.position, transform.rotation) as Rigidbody;
                    instantiatedboulder.velocity = transform.TransformDirection(new Vector3(50, 50, speed));
                }
                if (erupttimer < 0)
                {
                    isErupting = false;
                }
            }
        }
    }
}