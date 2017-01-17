using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarrierWallFadeScript : MonoBehaviour
{
 //   GameObject wall;

 //   public int fadeSpeed = 3;
 //   private bool isDone = false;

 //   private bool canFade;
 //   private Color alphaColor;
 //   private float timeToFade = 1.0f;

 //   // Use this for initialization
 //   void Start () {
 //       wall = transform.GetChild(0).gameObject;
 //       isDone = false;
 //       alphaColor = wall.GetComponent<MeshRenderer>().material.color;
 //       alphaColor.a = 0;
 //   }
	
	//// Update is called once per frame
	//void Update () {
	
	//}

 //   void OnTriggerStay(Collider _collider)
 //   {
 //       if(_collider.gameObject.tag == "Player1" ||
 //           _collider.gameObject.tag == "Player2" ||
 //           _collider.gameObject.tag == "Player3" ||
 //           _collider.gameObject.tag == "Player4")
 //       {
 //           if (!isDone)
 //           {
                
 //               if(wall.GetComponent<MeshRenderer>().material.color.a <= 0)
 //               {
 //                   isDone = true;
 //               }
 //               else
 //               {
 //                   while (wall.GetComponent<MeshRenderer>().material.color.a > 0)
 //                   {
 //                       Color newColor = wall.GetComponent<MeshRenderer>().material.color;
 //                       newColor.a -= Time.deltaTime / fadeSpeed;
 //                       wall.GetComponent<MeshRenderer>().material.color = newColor;
 //                   }
                        
 //               }
 //           }
 //       }
 //   }
}
