using UnityEngine;

using Valve.VR.InteractionSystem;
public class Chase : VRObjectBase {

    //public Transform target;//追いかける対象-オブジェクトをインスペクタから登録できるように
    private float speed;//移動スピード
    [SerializeField]
    private int MagnetPower;

    public override void Awake()
    {
        base.Awake();
        GetComponent<Throwable>().attachEaseIn = true;
    }

    /*void Update()
    {
        Vector3 vec = other.transform.position-this.transform.position;
    }*/


    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;
        VRObjectMode mode = new VRObjectMode();
        if (obj.GetComponent<VRObjectBase>() != null) {
            mode=obj.GetComponent<VRObjectBase>().GetVRObjectMode();
        }
        if (mode == VRObjectMode.NeverMove) {
            if (transform.parent != null) {
                Hand.DetachObject(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        //Debug.Log("はあ");
        if (other.transform.tag == "Metal")
        {
            //Debug.Log("ひい");
            Vector3 vec = transform.position - other.transform.position;
            float dis = vec.magnitude;
            //targetの方に少しずつ向きが変わる
             //other.transform.rotation = Quaternion.Slerp(other.transform.rotation, Quaternion.LookRotation(this.position - other.transform.position), 0.3f);
             //other.transform.rotation = Quaternion.LookRotation(this.transform.position - other.transform.position);
            //targetに向かって進む
            speed = 100*MagnetPower / dis;
            //other.transform.position += vec * speed;
            other.transform.GetComponent<Rigidbody>().AddForce(speed * vec.normalized);
        }
    }

    public void MagnetOn() {
        GetComponent<SphereCollider>().enabled = true;
    }
    public void MagnetOff() {
        GetComponent<SphereCollider>().enabled = false;
    }

    /*private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }*/
}
