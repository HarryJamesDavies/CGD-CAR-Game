using UnityEngine;
using System.Collections;

public class Tsunami : MonoBehaviour {

    public GameObject sea;

    public Transform highSeaLevel;
    public Transform dropSeaLevel;

    private float m_timer = 0.0f;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_timer += Time.deltaTime;
        DropSeaLevel();
        RaiseSeaLevel();

        if(m_timer >= 15.0f)
        {
            sea.transform.position = Vector3.Lerp(sea.transform.position, dropSeaLevel.position + new Vector3(0.0f,4.0f,0.0f), Time.deltaTime);
        }

    }

    void DropSeaLevel()
    {
        if (m_timer <= 3.0f)
        {
            sea.transform.position = Vector3.Lerp(sea.transform.position, dropSeaLevel.position, Time.deltaTime);
        }
    }

    void RaiseSeaLevel()
    {
        if (m_timer >= 5.0f)
        {
            sea.transform.position = Vector3.Lerp(sea.transform.position, highSeaLevel.position, Time.deltaTime * 0.1f);
        }
    }

}
