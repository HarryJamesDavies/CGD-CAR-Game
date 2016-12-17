using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    enum Games
    {
        driveAndSeek
    }

    enum DriveAndSeekPhases
    {
        setup,
        hide,
        seek
    }

    DriveAndSeekPhases phaseDriveAndSeek;
    Games gameToPlay;
    bool gameSetup;
    

    GameObject volcanoArenaPrefab;

    GameObject arena;
    
    List<Transform> seekerPositions;
    
    Transform hiderPosition;

	// Use this for initialization
	void Start () {
	    if(StateManager.m_instance.m_currentState == StateManager.State.GAMESETUPDRIVEANDSEEK)
        {
            gameToPlay = Games.driveAndSeek;
            phaseDriveAndSeek = DriveAndSeekPhases.setup;
        }
        gameSetup = false;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(gameSetup == false)
        {
            PerformSetup();
        }
        if(gameSetup == true)
        {
            if(gameToPlay == Games.driveAndSeek)
            {
                if(phaseDriveAndSeek == DriveAndSeekPhases.hide)
                {
                    DriveAndSeekHide();
                }
                else if (phaseDriveAndSeek == DriveAndSeekPhases.seek)
                {
                    DriveAndSeekSeek();
                }
            }
        }
	}

    void DriveAndSeekHide()
    {
        //start a co-routine for the timer with a boolean
        //check to see if vision is hiden
        //check to see if controls are taken away
    }

    void DriveAndSeekSeek()
    {
        //start a co-routine for the timer with a boolean
        //check to see if vision is hiden
        //check to see if controls are taken away
    }

    void PerformSetup()
    {
        if (gameToPlay == Games.driveAndSeek && phaseDriveAndSeek == DriveAndSeekPhases.setup)
        {
            
            //do some logic for deciding what arena to set up
            arena = Instantiate(volcanoArenaPrefab);

            //select the hider
            int hider = Random.Range(1, 4);
            //cycle through players and set their start points, then put them at the start points
            int iterTwo = 0;
            for(int iter = 0; PlayerManager.m_instance.m_playerCars.Count > iter; iter++)
            {
                if(iter == hider)
                {
                    PlayerManager.m_instance.m_playerCars[hider].gameObject.transform.position = hiderPosition.position;
                }
                else
                {
                    PlayerManager.m_instance.m_playerCars[iter].gameObject.transform.position = seekerPositions[iterTwo].position;
                    iterTwo++;
                }
            }

            phaseDriveAndSeek = DriveAndSeekPhases.hide;
            gameSetup = true;
           
            //countdown to play? intro?

        //disable controls for the seekers, hide their cameras
        }
    }
}
