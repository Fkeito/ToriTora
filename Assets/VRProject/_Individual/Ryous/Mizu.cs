using UnityEngine;
using System.Collections;

public class Mizu : MonoBehaviour {

    // Use this for initialization
    public GameObject explosionParticle;
    public GameObject HUU;
    [SerializeField] Transform basyosyo;
	void Start () {

      // 爆発パーティクル
}

// Update is called once per frame
   private  void OnTriggerEnter(Collider collision)
    {
       if (collision.gameObject.name=="mizuhu-senn")
     {
      
           var obj=(GameObject)Instantiate(explosionParticle, new Vector3(1, 3, -4), new Quaternion());
           
        

            Destroy(HUU);
            Destroy(gameObject);
            
            }
      }
    }

    

