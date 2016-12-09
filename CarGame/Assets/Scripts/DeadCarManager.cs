﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeadCarManager : MonoBehaviour {

    public List<GameObject> m_sections;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i <= transform.childCount; i++)
        {
            m_sections.Add(transform.GetChild(i).gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
