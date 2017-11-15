using UnityEngine;
using System.Collections;

public class Dest : MonoBehaviour {

    // Use this for initialization
    [SerializeField] GameObject haretu;

  void OnCollisionEnter(Collision col) {
        
        Instantiate(haretu,new Vector3(-1,0.1f,-4.5f),new Quaternion());
       Destroy(this.gameObject);
    }
	
	
}
