using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

public class KeyButton : VRObjectBase {
   
    private Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}

    public override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        if (hand.controller != null)
        {
            SteamVR_Controller.Device device1 = hand.controller;

            if (device1.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                animator.SetBool("pushed", true);
                Debug.Log("トリガーを深く引いた");
                animator.SetBool("pushed", false);
                Unlock();
            }
            if (device1.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                animator.SetBool("pulled", true);
                Debug.Log("トリガーを離した");
                animator.SetBool("pulled", false);
            }
        }

    }
            // Update is called once per frame
            /*void Update()
            {
                if (Input.GetMouseButtonDown(0))
                {


                    Ray ray = new Ray();
                    RaycastHit hit = new RaycastHit();
                    //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    ray = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                    //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                    {
                       // Debug.Log("raytobasita");
                      //  Debug.Log(hit.collider.gameObject.tag);
                        if (hit.collider.gameObject.tag == "KeyButton")
                        {

                            Unlock();

                        }
                    }
                }
            }*/
     public  void Unlock()
    {
       Debug.Log("Unlock");

    }
}

