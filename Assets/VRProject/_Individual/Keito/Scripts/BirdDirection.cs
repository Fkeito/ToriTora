using UnityEngine;
using System.Collections;

public class BirdDirection:MonoBehaviour{

    public static GameObject OnDirection(GameObject direction,RaycastHit hit)
    {
        GameObject dire = Instantiate(direction, hit.point + hit.normal * float.Epsilon, Quaternion.LookRotation(hit.normal)) as GameObject;
        return dire;
    }
}
