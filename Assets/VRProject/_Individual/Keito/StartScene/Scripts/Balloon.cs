using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

    [SerializeField]
    private float force;

    private Rigidbody rb;
    private float startPos;

	void Start () {
        rb = this.GetComponent<Rigidbody>();
        startPos = this.transform.position.y;
	}
	
	void Update () {
        if(this.transform.position.y < startPos) rb.AddForce(Vector3.up * force);
	}
}
