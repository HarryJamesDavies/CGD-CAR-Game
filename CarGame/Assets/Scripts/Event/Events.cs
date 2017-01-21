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

public class Events : MonoBehaviour
{
	public enum Event
    {
        ST_STATECHANGED,
        UI_UICHANGED,
        GM_FREEROAM,
        GM_DRIVEANDSEEK,
        DS_SETUP,
        DS_HIDING,
        DS_CHASE,
        DS_RESET,
        DS_FINISH,
        DS_BUFFER,
        DS_HIDERREADY,
        Count
    }
}
