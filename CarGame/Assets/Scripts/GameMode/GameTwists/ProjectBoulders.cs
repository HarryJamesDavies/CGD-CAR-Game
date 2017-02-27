using UnityEngine;
using System.Collections;

namespace HF
{
    public class ProjectBoulders : MonoBehaviour
    {
        public Rigidbody boulder;
        private Rigidbody instantiatedboulder;
        public float speed = 1;

        public bool isErupting = true;

        public float erupttimer = 10.0f;

		public float cleanclonetimer = 30.0f;

		public Animator boulderDecay;

		public ParticleSystem explosion;

        void Update()
        {
            if (TwistManager.m_instance.m_currentTwist == TwistManager.Twists.eruption)
            {
                erupttimer -= Time.deltaTime;
                if (isErupting == true)
                {
					explosion.Play();
                    instantiatedboulder = Instantiate(boulder, transform.position, transform.rotation) as Rigidbody;
                    instantiatedboulder.velocity = transform.TransformDirection(new Vector3(5, 5, speed));
                }
				if (erupttimer < 9) 
				{
					explosion.Stop();
				}
                if (erupttimer < 0)
                {
                    isErupting = false;
					cleanclonetimer -= Time.deltaTime;
                }
				if (cleanclonetimer < 0) 
				{
					foreach (GameObject boulderclone in FindObjectsOfType(typeof(GameObject)))
					{
						if (boulderclone.name == "Boulder(Clone)")
						{
							Destroy(boulderclone);
							//erupttimer = 5.0f;
							//isErupting = true;
						}
					}
				}
            }
        }
    }
}