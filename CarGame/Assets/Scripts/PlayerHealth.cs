using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
	public float max_Health = 100f;
	public float cur_Health = 0f;
	public GameObject healthbar;

	// Use this for initialization
	void Start () 
	{
		cur_Health = max_Health;
		//InvokeRepeating ("decreasehealth", 1f , 1f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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
}
