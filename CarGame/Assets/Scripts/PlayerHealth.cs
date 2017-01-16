using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
	public float max_Health = 100f;
	public float cur_Health = 0f;
	public GameObject healthbar;
    public Car m_car;
    public Movement m_movement;

    public string m_tag;

	// Use this for initialization
	void Start () 
	{
		cur_Health = max_Health;
        m_car = gameObject.GetComponent<Car>();
        m_movement = gameObject.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        CheckHealth();
	}

	public void decreasehealth()
	{
		cur_Health -= 20.0f;
		float calc_Health = cur_Health / max_Health;
		SetHealthBar (calc_Health);
	}

	public void SetHealthBar(float myHealth)
	{
		//myHealth value 0-1
		healthbar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0f , 1f) , healthbar.transform.localScale.y , healthbar.transform.localScale.z);
	}

    public void CheckHealth()
    {
        if (cur_Health <= 0.0f)
        {
            m_movement.m_controls = false;
            m_car.m_isDead = true;
        }
        else
        {
            //m_movement.m_controls = true;
            m_car.m_isDead = false;
        }
    }

    public void ResetHealth()
    {
        cur_Health = max_Health;
    }
}
