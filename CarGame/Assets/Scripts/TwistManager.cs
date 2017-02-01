﻿using UnityEngine;
using System.Collections;

public class TwistManager : MonoBehaviour {

    bool m_timerStart;

    public enum Twists
    {
        speedUp,
        dissapear,
        tsunami,
        teleporters,
        flipControls
    }

    public Twists m_currentTwist;

    TwistManager m_instance;

	// Use this for initialization
	void Start () {

        if (m_instance)
        {
            Destroy(this);
        }
        else
        {
            m_instance = this;
        }

        m_timerStart = false;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(m_timerStart == false)
        {
            StartCoroutine(CountdownToTwist());
        }
	}

    void ChooseTwist()
    {
        int twist = Random.Range(0, 5);

        BroadcastTwist(twist);
    }

    void BroadcastTwist(int twistNumber)
    {
        m_currentTwist = (Twists)twistNumber;
    }

    IEnumerator CountdownToTwist()
    {
        m_timerStart = true;
        yield return new WaitForSeconds(100); //time that the thing will start
        ChooseTwist();
    }
}
