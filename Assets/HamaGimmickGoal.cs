using UnityEngine;
using System.Collections;

public class HamaGimmickGoal : VRObjectBase {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Metal") {
            Debug.Log("はまギミッククリア！！");
        }
    }
}
