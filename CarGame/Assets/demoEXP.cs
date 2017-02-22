using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace HF
{
    public class demoEXP : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player1")
            {
                other.GetComponent<HF.PlayerExp>().addExptoCurrentLevel(25);
            }
        }


    }
}
