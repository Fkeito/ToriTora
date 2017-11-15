using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class EndingManager :VRObjectBase {
    public static EndingManager endingManager;
    private bool DoorOpened;
    private bool HappyEnd;
    private bool EndingStart;
    [SerializeField] private string EndSceneName;
    [SerializeField] private Camera PlayerCamera,DebugCamera;
    [SerializeField] private Hand hand1, hand2;
    private void Start()
    {
        endingManager = this;
        DoorOpened = false;
        HappyEnd = false;
        EndingStart = false;
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
        if (EndingStart) return;
        SceneManager.LoadScene(EndSceneName, LoadSceneMode.Additive);
        PlayerCamera.cullingMask = EndLayer;
        DebugCamera.cullingMask = EndLayer;
        hand1.hoverLayerMask = EndLayer;
        hand2.hoverLayerMask = EndLayer;
        EndingStart = true;
    }
    private const int EndLayer = 1048576;
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            End();
        }
    }
}
