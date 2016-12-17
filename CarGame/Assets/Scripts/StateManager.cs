using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour
{
    public enum State
    {
        MENU = 0,
        PLAY = 1,
        PAUSE = 2,
        GAMEOVER = 3,
        RESET = 4,
        GAMESETUPDRIVEANDSEEK = 5,
        Count
    };

    public static StateManager m_instance = null;

    public bool m_dirtyFlag;
    public State m_currentState;
    public State m_prevState;
    private State m_floatingState;

    [SerializeField]
    GameObject gameManagerRef;
    [SerializeField]
    GameObject gameManager;

    // Use this for initialization
    void Start()
    {
        if (m_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //   COPY BETWEEN THESE TWO LINES FOR STATE STUFF   \\
        //==================================================\\

        //Check if the state has changed
        if (StateManager.m_instance.m_dirtyFlag)
        {
            //Call state exit behaviour
            OnStateExit();

            //Call state enter behaviour
            OnStateEnter();
        }

        //State specific behaviour
        StateUpdate();
    }

    //Behaviour to perform per frame based on state
    void StateUpdate()
    {
        switch (StateManager.m_instance.m_currentState)
        {
            case StateManager.State.MENU:
                {
                    break;
                }
            case StateManager.State.PLAY:
                {
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
                    break;
                }
            case StateManager.State.RESET:
                {
                    break;
                }
            case StateManager.State.GAMESETUPDRIVEANDSEEK:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    //Behaviour to perform on state change
    void OnStateEnter()
    {
        switch (StateManager.m_instance.m_currentState)
        {
            case StateManager.State.MENU:
                {

                    break;
                }
            case StateManager.State.PLAY:
                {

                    break;
                }
            case StateManager.State.PAUSE:
                {
                    break;
                }
            case StateManager.State.GAMEOVER:
                {

                    break;
                }
            case StateManager.State.RESET:
                {
                    break;
                }
            case StateManager.State.GAMESETUPDRIVEANDSEEK:
                {
                    if(gameManager != null)
                    {

                    }
                    else
                    {
                        gameManager = Instantiate(gameManagerRef);
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void OnStateExit()
    {
        switch (StateManager.m_instance.m_prevState)
        {
            case StateManager.State.MENU:
                {
                    break;
                }
            case StateManager.State.PLAY:
                {
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
                    break;
                }
            case StateManager.State.RESET:
                {
                    break;
                }
            case StateManager.State.GAMESETUPDRIVEANDSEEK:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    //==================================================\\

    void LateUpdate()
    {
        m_dirtyFlag = false;

        if (m_currentState != m_floatingState)
        {
            m_dirtyFlag = true;
            m_prevState = m_floatingState;
            m_floatingState = m_currentState;
        }
    }
}


