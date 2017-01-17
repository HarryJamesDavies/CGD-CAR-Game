using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetButtonDown("P1-TouchPad(PS4)")))
        {
            Application.Quit();
        }
    }


    //   enum Games
    //   {
    //       driveAndSeek
    //   }

    //   public enum DriveAndSeekPhases
    //   {
    //       setup,
    //       hide,
    //       seek,
    //       finish
    //   }

    //   Coroutine timer = null;

    //   [SerializeField]
    //   public DriveAndSeekPhases m_phaseDriveAndSeek;
    //   [SerializeField]
    //   Games m_gameToPlay;
    //   bool m_gameSetup;
    //   bool m_gameDirtyFlag;
    //   bool m_coRoutineStarted;

    //   [SerializeField]
    //   GameObject m_volcanoArenaPrefab;
    //   [SerializeField]
    //   GameObject m_arena;

    //   List<Transform> m_seekerPositions = new List<Transform>();

    //   int m_roundCounter;
    //   int m_hider;
    //   List<int> m_previousHiders = new List<int>();

    //   [SerializeField]
    //   Transform m_hiderPosition; //volcanoArenaPrefab.gameObject.transform.FindChild("HiderSpawn").gameObject.transform.position;

    //// Use this for initialization
    //void Start () {
    //    if(StateManager.m_instance.m_currentState == StateManager.State.GAMESETUPDRIVEANDSEEK)
    //       {
    //           m_gameToPlay = Games.driveAndSeek;
    //           m_phaseDriveAndSeek = DriveAndSeekPhases.setup;
    //       }
    //       m_gameSetup = false;
    //       m_gameDirtyFlag = false;
    //       m_coRoutineStarted = false;
    //       m_roundCounter = 0;
    //       m_hiderPosition = m_volcanoArenaPrefab.gameObject.transform.FindChild("HiderSpawn").gameObject.transform;
    //       m_seekerPositions.Add(m_volcanoArenaPrefab.gameObject.transform.FindChild("SeekerSpawn1").gameObject.transform);
    //       m_seekerPositions.Add(m_volcanoArenaPrefab.gameObject.transform.FindChild("SeekerSpawn2").gameObject.transform);
    //       m_hider = -5;
    //   }

    //// Update is called once per frame
    //void Update () {

    //       if(m_gameSetup == false)
    //       {
    //           StateManager.m_instance.m_currentState = StateManager.State.GAMESETUPDRIVEANDSEEK;
    //           PerformSetup();
    //           StateManager.m_instance.m_currentState = StateManager.State.GAMEPLAYDRIVEANDSEEK;
    //       }
    //       if(m_gameSetup == true)
    //       {
    //           if(m_gameToPlay == Games.driveAndSeek)
    //           {
    //               if(m_phaseDriveAndSeek == DriveAndSeekPhases.hide)
    //               {
    //                   DriveAndSeekHide();
    //               }
    //               else if (m_phaseDriveAndSeek == DriveAndSeekPhases.seek)
    //               {
    //                   DriveAndSeekSeek();
    //               }
    //           }
    //       }
    //}

    //   void DriveAndSeekHide()
    //   {
    //       if (m_coRoutineStarted == false)
    //       {
    //           m_coRoutineStarted = true;
    //           timer = StartCoroutine(HideAndSeekTimer(10));
    //       }
    //       //start a co-routine for the timer with a boolean
    //       //check to see if vision is hiden
    //       //check to see if controls are taken away
    //       if(m_gameDirtyFlag == true)
    //       {
    //           m_phaseDriveAndSeek = DriveAndSeekPhases.seek;
    //           m_gameDirtyFlag = false;
    //           m_coRoutineStarted = false;
    //       }
    //   }

    //   void DriveAndSeekSeek()
    //   {
    //       if (m_coRoutineStarted == false)
    //       {
    //           m_coRoutineStarted = true;
    //           StartCoroutine(HideAndSeekTimer(10));
    //       }
    //       if (m_gameDirtyFlag == true)
    //       {
    //           m_phaseDriveAndSeek = DriveAndSeekPhases.setup;
    //           m_gameDirtyFlag = false;
    //           m_coRoutineStarted = false;
    //           m_gameSetup = false;
    //           m_hider = -5;
    //           m_roundCounter++;

    //       }

    //       //start a co-routine for the timer with a boolean
    //       //check to see if vision is hiden
    //       //check to see if controls are taken away
    //   }

    //   void PerformSetup()
    //   {
    //       if(m_roundCounter == 3)
    //       {
    //           GameModeManager.m_instance.m_currentEvent = GameModeManager.GameModeState.FREEROAM;
    //           StateManager.m_instance.m_currentState = StateManager.State.PLAY;
    //           Destroy(m_arena);
    //           Destroy(gameObject);
    //       }
    //       if (m_gameToPlay == Games.driveAndSeek && m_phaseDriveAndSeek == DriveAndSeekPhases.setup)
    //       {

    //           //do some logic for deciding what arena to set up
    //           if (m_arena == null)
    //           {
    //               m_arena = Instantiate(m_volcanoArenaPrefab);
    //           }

    //           //select the hider
    //           while (m_hider == -5)
    //           {
    //               m_hider = Random.Range(0, 2);
    //               for(int iter = 0; iter < m_previousHiders.Count; iter++)
    //               {
    //                   if(m_hider == m_previousHiders[iter])
    //                   {
    //                       m_hider = -5;
    //                   }
    //               }
    //           }
    //           //cycle through players and set their start points, then put them at the start points
    //           int iterTwo = 0;
    //           for(int iter = 0; PlayerManager.m_instance.m_playerCars.Count > iter; iter++)
    //           {
    //               if(iter == m_hider)
    //               {
    //                   PlayerManager.m_instance.m_playerCars[m_hider].gameObject.transform.position = m_hiderPosition.position;
    //               }
    //               else
    //               {
    //                   PlayerManager.m_instance.m_playerCars[iter].gameObject.transform.position = m_seekerPositions[iterTwo].position;
    //                   iterTwo++;
    //               }
    //           }

    //           m_phaseDriveAndSeek = DriveAndSeekPhases.hide;
    //           m_gameSetup = true;

    //           //countdown to play? intro?

    //       //disable controls for the seekers, hide their cameras
    //       }
    //   }

    //   IEnumerator HideAndSeekTimer(int time)
    //   {
    //       yield return new WaitForSeconds(time);

    //       m_gameDirtyFlag = true;
    //   }

    //   public void hiderCaught()
    //   {
    //       StopCoroutine(timer);

    //       m_gameDirtyFlag = true;
    //   }
}
