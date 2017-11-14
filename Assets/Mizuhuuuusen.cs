using UnityEngine;
using System.Collections;

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

        Instantiate(haretu, col.contacts[0].point, new Quaternion());
        Destroy(this.gameObject);
    }
}
