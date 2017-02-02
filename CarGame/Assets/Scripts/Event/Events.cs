using UnityEngine;
using System.Collections;

/* ####################################### */
//                                         //
//         ST_ = State Events              //
//         UI_ = UI Event                  //
//         GM_ = Game Mode Events          //
//         DS_ = Drive and Seek            //
//                                         //
//                                         //
/* ####################################### */

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
