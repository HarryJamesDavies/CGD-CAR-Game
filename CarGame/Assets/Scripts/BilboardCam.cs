using UnityEngine;
using System.Collections;

//===================== Kojima Drive - Half-Full Games 2017 ====================//
//
// Author(s): MATTHEW WYNTER
// Purpose: Bilboard cam for an object
// Namespace: HF
//
//===============================================================================//

namespace HF
{
    public class BilboardCam : MonoBehaviour
    {

        public Camera m_bilboard;

        void Update()
        {
            //always point the canvas to the camera
            transform.LookAt(transform.position + m_bilboard.transform.rotation * Vector3.forward,
                             m_bilboard.transform.rotation * Vector3.up);
        }
    }
}
