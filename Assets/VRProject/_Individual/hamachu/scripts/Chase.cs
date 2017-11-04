using UnityEngine;
using System.Collections;

public class Chase : VRObjectBase {

    //public Transform target;//追いかける対象-オブジェクトをインスペクタから登録できるように
    private float speed;//移動スピード

    void Start()
    {
        //target = GameObject.Find("対象").transform; インスペクタから登録するのでいらない
    }

    /*void Update()
    {
        Vector3 vec = other.transform.position-this.transform.position;
    }*/


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Metal")
        {
            Vector3 vec = this.transform.position - other.transform.position;
            float dis = vec.magnitude;
            //targetの方に少しずつ向きが変わる
            // other.transform.rotation = Quaternion.Slerp(other.transform.rotation, Quaternion.LookRotation(this.position - other.transform.position), 0.3f);
            //other.transform.rotation = Quaternion.LookRotation(this.transform.position - other.transform.position);
            //targetに向かって進む
            speed = 0.2f / dis;
            other.transform.position += vec * speed;
        }
    }



    private void OnTriggerExit(Collider other)
    { other.GetComponent<Rigidbody>().velocity = Vector3.zero; }
}
