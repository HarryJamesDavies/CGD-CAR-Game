using UnityEngine;
using System.Collections;

namespace HF
{
    public class ControllerManager : MonoBehaviour
    {

        public static ControllerManager m_instance = null;

        public int m_joystickNumber;
        public bool m_useController;

        // Use this for initialization
        void Start()
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
        }

        void Update()
        {
            m_joystickNumber = 0;
            // Debug.Log("Use Controller: " + m_useController);

            for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            {
                if (Input.GetJoystickNames()[i] == "Wireless Controller")
                {
                    m_useController = true;
                    m_joystickNumber++;
                    Debug.Log("Joystick Iterator: " + m_joystickNumber);
                }
                else
                {
                    m_joystickNumber--;

                    if (m_joystickNumber >= Input.GetJoystickNames().Length)
                    {
                        m_useController = false;
                        Debug.Log("Joystick Iterator: " + m_joystickNumber);
                    }
                }
            }
        }
    }
}
