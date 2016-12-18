using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

    public float targetTime = 60.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

        GetComponent<TextMesh>().text = string.Format("{0:N2}", targetTime);
    }
    
    void timerEnded()
    {
        //do your stuff here.
    }
}

