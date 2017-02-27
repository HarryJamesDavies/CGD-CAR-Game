using UnityEngine;
using System.Collections;

public class BoulderGravity : MonoBehaviour 
{	
	// Update is called once per frame
	void Update ()
	{
		Physics.gravity = new Vector3(0, -10.0F, 0);
	}
}
