using UnityEngine;
using System.Collections;

public class Scenemanager : MonoBehaviour {
    public string buttonTag = "Button";
    public Animator anima;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag(buttonTag))
                {

                    hit.collider.gameObject.GetComponent<ButtonControl>().OnUserAction();

                }
            }
        }
    }
}
