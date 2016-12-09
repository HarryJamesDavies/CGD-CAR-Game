using UnityEngine;
using System.Collections;

public class BilboardCam : MonoBehaviour {

    public Camera m_bilboard;

    void Update()
    {
        //always point the canvas to the camera
        transform.LookAt(transform.position + m_bilboard.transform.rotation * Vector3.forward,
                         m_bilboard.transform.rotation * Vector3.up);
    }
}
