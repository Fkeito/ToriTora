using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour{
    public int index;

    public void OnUserAction()
    {
        Dentaku.dentaku.Pull(index);
    }
}
