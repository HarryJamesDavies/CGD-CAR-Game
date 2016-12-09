using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour {

    public static ControllerManager m_instance = null;

    private int m_joystickIterator;

    public bool m_useController;

	// Use this for initialization
	void Start ()
    {
        m_useController = false;

        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log(Input.GetJoystickNames().Length);
	}
	
	void Update ()
    {
        m_joystickIterator = 0;
        Debug.Log("Use Controller: " + m_useController);

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if (Input.GetJoystickNames()[i] == "Wireless Controller")
            {
                m_useController = true;
                m_joystickIterator++;
                Debug.Log("Joystick Iterator: " + m_joystickIterator);
            }
            else
            {
                m_joystickIterator--;

                if (m_joystickIterator >= Input.GetJoystickNames().Length)
                {
                    m_useController = false;
                    Debug.Log("Joystick Iterator: " + m_joystickIterator);
                }
            }
        }
	}
}
