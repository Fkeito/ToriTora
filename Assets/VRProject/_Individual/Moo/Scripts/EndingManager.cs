using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class EndingManager :VRObjectBase {
    public static EndingManager endingManager;
    private bool DoorOpened;
    private bool HappyEnd;
    [SerializeField] private string EndSceneName;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Hand hand1, hand2;
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
        SceneManager.LoadScene(EndSceneName, LoadSceneMode.Additive);
        PlayerCamera.cullingMask = 7;
        hand1.hoverLayerMask = 7;
        hand2.hoverLayerMask = 7;
    }
}
