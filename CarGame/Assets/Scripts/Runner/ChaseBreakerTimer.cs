using UnityEngine;
using System.Collections;

//===================== Kojima Drive - Half-Full Games 2017 ====================//
//
// Author(s): MATTHEW WYNTER
// Purpose: Script attached to each chase breaker to destroy after a certain amount of time has passed. 
// Namespace: HF
//
//===============================================================================//

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
