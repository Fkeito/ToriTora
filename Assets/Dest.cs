using UnityEngine;
using System.Collections;

public class Dest : MonoBehaviour {

	// Use this for initialization

	
  void OnCollisionEnter(Collision col) {
        Destroy(this.gameObject);
            }
	
	
}
