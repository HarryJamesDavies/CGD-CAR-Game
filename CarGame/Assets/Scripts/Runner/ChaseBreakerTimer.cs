using UnityEngine;
using System.Collections;

namespace HF
{
    public class ChaseBreakerTimer : MonoBehaviour
    {

        float timer = 0.0f;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (this.gameObject.activeInHierarchy == true)
            {
                timer += Time.deltaTime;
                destroyThisBreaker();
            }
        }

        public void destroyThisBreaker()
        {
            if (timer >= 5.0f)
            {
                Destroy(gameObject);
                timer = 0.0f;
            }
        }
    }
}
