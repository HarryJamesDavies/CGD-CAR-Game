using UnityEngine;
using System.Collections;

/* ####################################### */
//                                         //
//         ST_ = State Events              //
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
        GM_FREEROAM,
        GM_DRIVEANDSEEK,
        DS_SETUP,
        DS_HIDING,
        DS_SEEKING,
        DS_CHASE,
        DS_RESET,
        DS_FINISH,
        Count
    }
}
