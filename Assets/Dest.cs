using UnityEngine;
using System.Collections;

public class Dest : MonoBehaviour {

    // Use this for initialization
    [SerializeField] GameObject haretu;

  void OnCollisionEnter(Collision col) {
        
        Instantiate(haretu,new Vector3(-3,1,14),new Quaternion());
       Destroy(this.gameObject);
    }
	
	
}
