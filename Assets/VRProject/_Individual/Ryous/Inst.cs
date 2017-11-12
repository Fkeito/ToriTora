using UnityEngine;
using System.Collections;

public class Inst : MonoBehaviour {
    [SerializeField] private GameObject prefub;
    [SerializeField] private float interval;
    [SerializeField] private Transform basyo;
    private float time;
    Rigidbody huu;
      private void Start()
    {
        time = interval;
        
    }
    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
   
       
            if (interval*0.5 <time&&huu!=null)
            {
                huu.useGravity = true;
            }
            if(interval<time) { 
                GameObject obj = (GameObject)Instantiate(prefub, basyo.position, basyo.rotation);
                 huu = obj.transform.GetComponent<Rigidbody>();
             
                time = 0;
            }
        }
	}

