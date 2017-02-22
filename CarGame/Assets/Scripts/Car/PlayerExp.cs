using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace HF
{
    public class PlayerExp : MonoBehaviour
    {
        //list of the milestones
        public List<float> expMilestones;
        public float currentLevel = 0;

        //variables for the fading and transforms of xp comps
        public float fade = 1.0f;
        public Text text;

        // Use this for initialization
        void Start()
        {
            expMilestones = new List<float>();
            addMilestonesToList(500, 1000, 1500);
            checkMilestoneAchieved();

            text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //add milestones to a milestone list 
        private void addMilestonesToList(params float[] mileStoneList)
        {
            for (int i = 0; i < mileStoneList.Length; i++)
            {
                expMilestones.Add(mileStoneList[i]);
            }
        }

        //used to add exp to a level
        public void addExptoCurrentLevel(float expInc)
        {
            //add XP to a total for the player
            currentLevel += expInc;

            //project it to the screen via car script 
            gameObject.GetComponent<HF.Car>().demoText.text = "+ " + expInc + "xp";
            gameObject.GetComponent<HF.Car>().m_demoText = true;

            //our current level
            Debug.Log(currentLevel);
        }

        //check if a milestone has been achieved
        private void checkMilestoneAchieved()
        {
            for (int i = 0; i < expMilestones.Count; i++)
            {
                float mile = expMilestones[i];
                if (currentLevel == mile)
                {
                    Debug.Log("CurrentXP = " + mile + " Level Up: " + (i + 1));
                }
            }
        }
    }
}

