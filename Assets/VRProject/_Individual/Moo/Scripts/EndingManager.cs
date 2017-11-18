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
    [SerializeField]private GameObject endingEnvironment;
    [SerializeField]private GameObject alphaobj;
    [SerializeField] private Material alphamaterial;
    [SerializeField] private float changeTime;
    [SerializeField] private bool changing;
    [SerializeField] private GameObject PlayRoom;
    private float changeSpeed;
    private void Start()
    {
        endingManager = this;
        DoorOpened = false;
        HappyEnd = false;
        EndingStart = false;
        endingEnvironment.SetActive(false);
        alphaobj.SetActive(false);
        changing = false;
        time = 0;
        changeSpeed = 1 / changeTime;
    }

    public void DoorOpen() {
        DoorOpened = true;

        endingEnvironment.SetActive(true);
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
        
        PlayerCamera.cullingMask = EndLayer;
        PlayerCamera.clearFlags = CameraClearFlags.Skybox;
        DebugCamera.cullingMask = EndLayer;
        DebugCamera.clearFlags = CameraClearFlags.Skybox;
        hand1.hoverLayerMask = EndLayer;
        hand2.hoverLayerMask = EndLayer;
        alphaobj.SetActive(true);
        PlayRoom.SetActive(false);
        EndingStart = true;
        changing = true;
    }
    private const int EndLayer = 1048576;
    private float time;
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            End();
        }

        if (changing) {
            time += Time.deltaTime;
            alphamaterial.color = new Color(0,0,0,1-time*changeSpeed);
            Debug.Log("time");
            if (time > changeTime) {
                changing = true;
                alphamaterial.color = new Color();
                Debug.Log("owari");
            }
        }
    }
}
