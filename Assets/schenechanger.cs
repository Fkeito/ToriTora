using UnityEngine;
using System.Collections;

public class schenechanger : MonoBehaviour {
    public bool change;
    void Update(){
        if (change) {
            SceneManager.LoadScene("Main");
        }
    }
}
