using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public bool m_counting = false;

    public string m_name = "";
    public float m_startTime = 0.0f;
    public float m_timerLength = 0.0f;

    public void SetTimer(string _name, float _timerLength)
    {
        m_name = _name;
        m_timerLength = _timerLength;
    }

    public void StartTimer()
    {
        m_counting = true;
        m_startTime = Time.time;
    }

    public void ResetTimer()
    {
        m_name = "";
        m_startTime = 0.0f;
        m_timerLength = 0.0f;
    }

    public bool CheckFinished()
    {
        if (Time.time - m_startTime >= m_timerLength)
        {
            m_counting = false;
            return true;
        }
        return false;
    }
}
