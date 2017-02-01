using UnityEngine;
using System.Collections;

public class TwistManager : MonoBehaviour {

    bool m_timerStart;

    public enum Twists
    {
        NULL,
        speedUp,
        dissapear,
        tsunami,
        teleporters,
        flipControls
    }

    public Twists m_currentTwist;

    public static TwistManager m_instance;

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

        m_currentTwist = Twists.NULL;
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
        int twist = Random.Range(1, 6);

        BroadcastTwist(twist);
    }

    void BroadcastTwist(int twistNumber)
    {
        m_currentTwist = (Twists)twistNumber;
    }

    IEnumerator CountdownToTwist()
    {
        m_timerStart = true;
        yield return new WaitForSeconds(20); //time that the thing will start
        ChooseTwist();
    }
}
