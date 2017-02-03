//===================== Kojima Drive - Half-Full Games 2017 ====================//
//
// Author:  Harry Davies
// Purpose: Used to create event call lists, by adding a new entry, adds a new 
//          event call list.
// Namespace: HF
//
//===============================================================================//

using UnityEngine;
using System.Collections;

//================================== Event Key ==================================//
//
//                              ST_ = State Events
//                              UI_ = UI Event
//                              GM_ = Game Mode Events 
//                              DS_ = Drive and Seek
//
//
//===============================================================================//

namespace HF
{
    public class Events : MonoBehaviour
    {
        public enum Event
        {
            ST_STATECHANGED,
            UI_UICHANGED,
            GM_FREEROAM,
            GM_DRIVEANDSEEK,
            GM_TEST,
            DS_SETUP,
            DS_RUNNING,
            DS_CHASING,
            DS_RESET,
            DS_FINISH,
            DS_BUFFER,
            DS_HIDERREADY,
            Count
        }
    }
}
