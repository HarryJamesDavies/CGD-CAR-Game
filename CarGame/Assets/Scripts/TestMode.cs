using UnityEngine;
using System.Collections;

namespace HF
{
    public class TestMode : GameMode
    {

        new
        void Start()
        {
            base.Start();
            Debug.Log("Start");
        }

        new
       void Update()
        {
            base.Update();
            if (m_active)
            {
                Debug.Log("Active");
            }
        }
    }
}
