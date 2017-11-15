using UnityEngine;
using System;
using System.Collections;
using UnityStandardAssets;

public class Mizuhuuuusen : VRObjectBase {

    [SerializeField]
    private GameObject haretu;

    public void Caught()
    {
        GetComponent<Collider>().isTrigger=true;
        Destroy(this.gameObject.GetComponent<Animator>());
        this.name = "mizuhu-senn";
    }
    public void Thrown()
    {
        GetComponent<Collider>().isTrigger = false;
    }
    void OnCollisionEnter(Collision col)
    {

        Instantiate(haretu, col.contacts[0].point, Quaternion.Euler(-90,0,0));
        Destroy(this.gameObject);
    }
}
