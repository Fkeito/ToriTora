using UnityEngine;
using System.Collections;

public class ButtonControl : VRObjectBase{
    public int index;
    public Animator anima;

    public void OnUserAction()
    {
        Dentaku.dentaku.Pull(index);
        anima.SetBool("Push", true);
    }
}
