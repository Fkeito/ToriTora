using UnityEngine;
using System.Collections;

public class balloon : MonoBehaviour {
    
    [SerializeField]
    private float force;

    private Rigidbody rb;
    private Vector3 right;
    private Vector3 left;

    private float startPos;

	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
        startPos = this.gameObject.transform.position.y;
    }
	
	void Update ()
    {
        if(this.transform.position.y < startPos) rb.AddForce(Vector3.up * force);
    }
}