using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool m_counting = false;

    public string m_name = "";
    public float m_startTime = 100000.0f;
    public float m_timerLength = 0.0f;

    public GameObject m_box;
    public GameObject m_text;

    void Update()
    {
        if (m_counting)
        {
            m_text.GetComponent<Text>().text = "" + (int)(m_timerLength - (Time.time - m_startTime));
        }
    }

    public void SetTimer(string _name, float _timerLength)
    {
        m_name = _name;
        m_timerLength = _timerLength;
    }

    public void StartTimer()
    {
        m_counting = true;
        m_startTime = Time.time;
        m_box.SetActive(true);
        m_text.SetActive(true);
        m_text.GetComponent<Text>().text = "";
    }

    public void ResetTimer()
    {
        m_counting = false;
        m_startTime = 10000.0f;
        m_box.SetActive(false);
        m_text.SetActive(false);
        m_text.GetComponent<Text>().text = "";
    }

    public bool CheckFinished()
    {
        if (Time.time - m_startTime >= m_timerLength)
        {
            m_counting = false;
            m_box.SetActive(false);
            m_text.SetActive(false);
            m_text.GetComponent<Text>().text = "";
            return true;
        }
        return false;
    }
}
