﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class schenechanger : MonoBehaviour {
    public bool change;
    void Update(){
        if (change) {
            SceneManager.LoadScene("Main");
        }
    }
}
