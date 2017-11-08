using UnityEngine;
using System.Collections;

public class CageController : VRObjectBase {

    public Animator anim;
    private float moveTime = 5f;
    private float time = 0;

    public GameObject key;

    void Start () {
        anim.SetBool("haveBird", true);
	}
	
	void Update () {
        time += Time.deltaTime;

        if(time > moveTime)
        {
            anim.SetBool("moveFlag", true);
            time = 0;
        }
	}

    public void SetGlipped(bool isGlipped)
    {
        anim.SetBool("isGlipped", isGlipped);
    }

    void OnCollisionEnter(Collision other)
    {
        if(key.GetInstanceID() == other.gameObject.GetInstanceID())
        {
            Destroy(key);
            anim.SetBool("haveBird", false);
            GameObject bird = this.gameObject.GetComponentInChildren<Bird>().gameObject;
            bird.transform.parent = null;
            bird.GetComponent<Bird>().inCage = false;
            bird.GetComponent<Bird>().anim.SetBool("inCage", false);
            bird.GetComponentsInChildren<Collider>()[1].isTrigger = false;
            bird.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
