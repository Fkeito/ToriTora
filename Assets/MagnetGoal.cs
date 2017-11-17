using UnityEngine;
using System.Collections;

public class MagnetGoal : VRObjectBase {
    private bool clear;
    private void Start() {
        clear = false;
    }
    void OnCollisionEnter(Collision other) {
        if (clear) return;
        if (other.gameObject.tag == "Metal") {
            DoorController.door.Clear();
            clear = true;
        }
    }

}
