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

            if (other.gameObject.tag == "Player2")
            {
                other.GetComponent<HF.PlayerExp>().addExptoCurrentLevel(50);
            }

            if (other.gameObject.tag == "Player3")
            {
                other.GetComponent<HF.PlayerExp>().addExptoCurrentLevel(70);
            }

            if (other.gameObject.tag == "Player4")
            {
                other.GetComponent<HF.PlayerExp>().addExptoCurrentLevel(65);
            }
        }


    }
}
