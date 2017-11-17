using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

public class ButtonControl : VRObjectBase
{
    public int index;
    public Animator anima;

    /* public void OnUserAction()
     {
         Dentaku.dentaku.Pull(index);
         anima.SetBool("Push", true);
     }*/

    public override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        if (hand.controller != null)
        {
            SteamVR_Controller.Device device1 = hand.controller;

            if (device1.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("トリガーを深く引いた");
                Dentaku.dentaku.Pull(index);
                anima.SetBool("Push", true);
            }
        }
    }
}