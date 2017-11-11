using UnityEngine;
using System.Collections;

public class SecurityCameraController : MonoBehaviour {

    public GameObject target1;
    public GameObject target2;

    private GameObject target;
    
    void Start()
    {
        if (target1.activeSelf)
        {
            target = target1;
        }
        else
        {
            target = target2;
        }
    }

	void Update () {
        this.transform.LookAt(target.transform.position);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (target1.activeSelf)
            {
                target = target1;
            }
            else
            {
                target = target2;
            }
        }
	}
}
