using UnityEngine;
using System.Collections;

namespace HF
{
    public class HideOnDAS : MonoBehaviour
    {

        void Start()
        {
            EventManager.m_instance.SubscribeToEvent(Events.Event.GM_FREEROAM, EvFunc_RevealEvent);
            EventManager.m_instance.SubscribeToEvent(Events.Event.GM_DRIVEANDSEEK, EvFunc_HideEvent);
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