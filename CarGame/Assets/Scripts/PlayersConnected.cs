using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayersConnected : MonoBehaviour {

    public static PlayersConnected pc_instance;

    public int m_playersconnected;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();

        m_playersconnected = 0;
    }
    // Use this for initialization

	void Start ()
    {
        if (pc_instance == null)
        {
            pc_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        text.text = "Players Connected: " + m_playersconnected;
    }

    //used to increment the number of active player visually 
    public void IncrementPC()
    {
        m_playersconnected++;
    }

}
