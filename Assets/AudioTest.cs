﻿using UnityEngine;
using System.Collections;

public class AudioTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("a"))
        {
            AudioManager.Main.PlayNewSound("clock-chimes-daniel_simon");
        }
	
	}
}
