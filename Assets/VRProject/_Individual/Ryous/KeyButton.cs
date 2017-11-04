using UnityEngine;
using System.Collections;

public class KeyButton : VRObjectBase {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           

            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ray = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
               // Debug.Log("raytobasita");
              //  Debug.Log(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "KeyButton")
                {

                    Unlock();

                }
            }
        }
    }*/
    public  void Unlock()
    {
       Debug.Log("Unlock");
    }
}
