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
        seek,
        finish
    }
    [SerializeField]
    DriveAndSeekPhases phaseDriveAndSeek;
    [SerializeField]
    Games gameToPlay;
    bool gameSetup;
    bool m_gameDirtyFlag;
    bool m_coRoutineStarted;
    
    [SerializeField]
    GameObject volcanoArenaPrefab;
    [SerializeField]
    GameObject arena;

    List<Transform> seekerPositions = new List<Transform>();

    int m_roundCounter;
    int m_hider;
    List<int> previousHiders = new List<int>();

    [SerializeField]
    Transform hiderPosition; //volcanoArenaPrefab.gameObject.transform.FindChild("HiderSpawn").gameObject.transform.position;

	// Use this for initialization
	void Start () {
	    if(StateManager.m_instance.m_currentState == StateManager.State.GAMESETUPDRIVEANDSEEK)
        {
            gameToPlay = Games.driveAndSeek;
            phaseDriveAndSeek = DriveAndSeekPhases.setup;
        }
        gameSetup = false;
        m_gameDirtyFlag = false;
        m_coRoutineStarted = false;
        m_roundCounter = 0;
        hiderPosition = volcanoArenaPrefab.gameObject.transform.FindChild("HiderSpawn").gameObject.transform;
        seekerPositions.Add(volcanoArenaPrefab.gameObject.transform.FindChild("SeekerSpawn1").gameObject.transform);
        seekerPositions.Add(volcanoArenaPrefab.gameObject.transform.FindChild("SeekerSpawn2").gameObject.transform);
        m_hider = -5;
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
        if (m_coRoutineStarted == false)
        {
            m_coRoutineStarted = true;
            StartCoroutine(HideAndSeekTimer(10));
        }
        //start a co-routine for the timer with a boolean
        //check to see if vision is hiden
        //check to see if controls are taken away
        if(m_gameDirtyFlag == true)
        {
            phaseDriveAndSeek = DriveAndSeekPhases.seek;
            m_gameDirtyFlag = false;
            m_coRoutineStarted = false;
        }
    }

    void DriveAndSeekSeek()
    {
        if (m_coRoutineStarted == false)
        {
            m_coRoutineStarted = true;
            StartCoroutine(HideAndSeekTimer(10));
        }
        if (m_gameDirtyFlag == true)
        {
            phaseDriveAndSeek = DriveAndSeekPhases.setup;
            m_gameDirtyFlag = false;
            m_coRoutineStarted = false;
            gameSetup = false;
            m_hider = -5;
            m_roundCounter++;

        }
        
        //start a co-routine for the timer with a boolean
        //check to see if vision is hiden
        //check to see if controls are taken away
    }

    void PerformSetup()
    {
        if(m_roundCounter == 3)
        {
            EventManager.m_instance.m_currentEvent = EventManager.Events.FREEROAM;
            StateManager.m_instance.m_currentState = StateManager.State.PLAY;
            Destroy(arena);
            Destroy(gameObject);
        }
        if (gameToPlay == Games.driveAndSeek && phaseDriveAndSeek == DriveAndSeekPhases.setup)
        {

            //do some logic for deciding what arena to set up
            if (arena == null)
            {
                arena = Instantiate(volcanoArenaPrefab);
            }

            //select the hider
            while (m_hider == -5)
            {
                m_hider = Random.Range(0, 2);
                for(int iter = 0; iter < previousHiders.Count; iter++)
                {
                    if(m_hider == previousHiders[iter])
                    {
                        m_hider = -5;
                    }
                }
            }
            //cycle through players and set their start points, then put them at the start points
            int iterTwo = 0;
            for(int iter = 0; PlayerManager.m_instance.m_playerCars.Count > iter; iter++)
            {
                if(iter == m_hider)
                {
                    PlayerManager.m_instance.m_playerCars[m_hider].gameObject.transform.position = hiderPosition.position;
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

    IEnumerator HideAndSeekTimer(int time)
    {
        yield return new WaitForSeconds(time);

        m_gameDirtyFlag = true;
    }
}
