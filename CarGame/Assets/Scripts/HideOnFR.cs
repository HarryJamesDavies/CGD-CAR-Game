using UnityEngine;
using System.Collections;

namespace HF
{
    public class HideOnFR : MonoBehaviour
    {

        void Start()
        {
            EventManager.m_instance.SubscribeToEvent(Events.Event.GM_FREEROAM, EvFunc_HideEvent);
            EventManager.m_instance.SubscribeToEvent(Events.Event.GM_DRIVEANDSEEK, EvFunc_RevealEvent);
            gameObject.SetActive(false);
        }

        void EvFunc_RevealEvent()
        {
            gameObject.SetActive(true);
        }

        void EvFunc_HideEvent()
        {
            gameObject.SetActive(false);
        }
    }
}