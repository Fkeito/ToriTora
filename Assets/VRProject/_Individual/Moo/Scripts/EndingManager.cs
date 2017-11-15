using UnityEngine;
using System.Collections;

public class EndingManager :VRObjectBase {
    public static EndingManager endingManager;
    private bool DoorOpened;
    private bool HappyEnd;
    private void Start()
    {
        endingManager = this;
        DoorOpened = false;
        HappyEnd = false;
    }

    public void DoorOpen() {
        DoorOpened = true;
    }

    private void HappyEndGet() {
        HappyEnd = true;
    }

    private void OnCollisionEnter(Collision other) {
        if (DoorOpened)
        {
            if (!HappyEnd)
            {
                if (other.gameObject.tag == "Bird")
                {
                    HappyEndGet();
                }
            }
        }
    }
    public void End() {
        if (!DoorOpened) return;
        if (HappyEnd)
        {
            Debug.Log("HappyEnd!!");
        }
        else {
            Debug.Log("BadEnd!!");
        }
    }
}
