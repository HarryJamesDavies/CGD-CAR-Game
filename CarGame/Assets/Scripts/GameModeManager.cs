using UnityEngine;
using System.Collections;

public class GameModeManager : MonoBehaviour {

    public static GameModeManager m_instance = null;

    public enum GameModeState
    {
        FREEROAM = 0,
        DRIVEANDSEEK = 1,
        Count
    };

    public GameModeState m_currentEvent = GameModeState.FREEROAM;
    public GameModeState m_prevEvent = GameModeState.FREEROAM;
    private GameModeState m_floatingEvent = GameModeState.FREEROAM;

    public GameMode m_currentGameMode;
    public GameObject m_modePrefab;
    private GameObject m_modeHolder;

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
    void LateUpdate()
    {
        if (m_currentEvent != m_floatingEvent)
        {
            m_prevEvent = m_floatingEvent;
            m_floatingEvent = m_currentEvent;
            UpdateEvent();
        }
    }

    void UpdateEvent()
    {
        switch (m_currentEvent)
        {
            case GameModeState.FREEROAM:
                {
                    EventManager.m_instance.AddEvent(Events.Event.GM_FREEROAM);
                    Destroy(m_modeHolder);
                    m_currentGameMode = null;
                    break;
                }
            case GameModeState.DRIVEANDSEEK:
                {
                    EventManager.m_instance.AddEvent(Events.Event.GM_DRIVEANDSEEK);
                    m_modeHolder = (GameObject)Instantiate(m_modePrefab, m_modePrefab.transform.position, m_modePrefab.transform.rotation);
                    m_currentGameMode = m_modeHolder.GetComponent<DriveAndSeekMode>();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}