using UnityEngine;
using System.Collections;

public class EventCollider : MonoBehaviour {

    //check if player 1 has already pressed to play
    public bool m_firstPlayerPlaying = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player1")
        {
            if (m_firstPlayerPlaying == false)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Debug.Log(other.tag + " wants to connect!");

                    //activates the laser and active player number
                    PlayersConnected.pc_instance.IncrementPC();
                    ShootBeam.sb_instance.DisplayLine();
                    m_firstPlayerPlaying = true;
                }
            }
        }

        //not currently set up for the 2nd player
        if (other.tag == "Player2")
        {
            Debug.Log("Player2");
        }
       
    }

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
