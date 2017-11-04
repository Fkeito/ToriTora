using UnityEngine;
using System.Collections;

public class Mizu : MonoBehaviour {

    // Use this for initialization
    public GameObject explosionParticle;
    public GameObject HUU;
	void Start () {

      // 爆発パーティクル
}

// Update is called once per frame
   private  void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name=="mizuhu-senn")
        {
          
            Destroy(gameObject);
          //  Destroy(HUU);
            Instantiate(explosionParticle);        
            }
        }
    }

    

