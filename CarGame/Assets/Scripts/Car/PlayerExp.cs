using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerExp : MonoBehaviour {

    public List<float> expMilestones;
    public float currentLevel = 0;
    public float fade = 1.0f;
    

    public Text text;

	// Use this for initialization
	void Start ()
    {
        expMilestones = new List<float>();
        addMilestonesToList(500, 1000, 1500);
        checkMilestoneAchieved();

        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<HF.Car>().demoText.color -= new Color(0, 0, 0, fade * Time.deltaTime);
    }

    //add milestones to a milestone list 
    private void addMilestonesToList(params float[] mileStoneList)
    {
        for(int i = 0; i < mileStoneList.Length; i++)
        {
            expMilestones.Add(mileStoneList[i]);
        }
    }

    //used to add exp to a level
    public void addExptoCurrentLevel(float expInc)
    {
        currentLevel += expInc;
        gameObject.GetComponent<HF.Car>().demoText.text = "+ " + expInc;
        gameObject.GetComponent<HF.Car>().m_demoText = true;

        // text.text = "+ " + expInc;

        Debug.Log(currentLevel);
    }

    //check if a milestone has been achieved
    private void checkMilestoneAchieved()
    {
        for(int i = 0; i < expMilestones.Count; i++)
        {
            float mile = expMilestones[i];
            if(currentLevel == mile)
            {
                Debug.Log("CurrentXP = " + mile + " Level Up: " + (i + 1));
            }
        }
    }



}
