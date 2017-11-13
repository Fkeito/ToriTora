using UnityEngine;
using System.Collections;

public class BirdMove : MonoBehaviour
{

    [SerializeField]
    private Vector3[] movePoint;

    private Vector3 targetPoint;

    private float speed = 3;

    private Animator anim;

    private bool moveFlag = false;
    private bool fly = false;
    private float time = 0;
    private float stopTime = 0;

    void Start()
    {
        //Time.timeScale = 2;

        if (movePoint.Length == 0) Destroy(this);

        anim = this.gameObject.GetComponent<Bird>().anim;

        Vector3 tmpPos = Vector3.zero;
        targetPoint = SetTargetPosition();

        if (Distance(tmpPos, targetPoint) < 5f)
        {
            speed = 3f;
        }
        else
        {
            speed = 3;
        }

        if (tmpPos.y != targetPoint.y) fly = true;

        for (int i = 0; i < movePoint.Length; i++)
        {
            GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tmp.GetComponent<Collider>().isTrigger = true;
            tmp.transform.position = movePoint[i];// + new Vector3(0, 0.5f, 0);
            tmp.tag = "Finish";
            tmp.GetComponent<Renderer>().enabled = false;
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        Move();

        if (time > stopTime)
        {
            if (!moveFlag)
            {
                moveFlag = true;
                if (fly)
                {
                    anim.SetBool("isFlying", true);
                    fly = false;
                }
                else
                {
                    anim.SetFloat("Speed", speed);
                }

                
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponentsInChildren<Collider>()[1].isTrigger = true;

            }
        }
        else
        {
            Debug.Log((int)(Time.deltaTime*1000));

            moveFlag = false;
            if((int)(time* 1000) % 10 == 1)
            {
                anim.SetBool("Action", true);
            }
        }
    }

    void Move()
    {
        if (moveFlag)
        {

            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * 3);
        }
        else
        {
            SetForward();
        }
    }

    Vector3 SetTargetPosition()
    {
        int tmpNum = Random.Range(0, movePoint.Length);
        return movePoint[tmpNum];
    }

    void SetForward()
    {
        Vector3 tmpForward = this.transform.forward;
        tmpForward.y *= 0;
        this.transform.forward = Vector3.Slerp(this.transform.forward, tmpForward.normalized, Time.deltaTime * 2);
    }

    float Distance(Vector3 a,Vector3 b)
    {
        float answer = (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
        return Mathf.Sqrt(answer);
    }

    void OnCollisionStay(Collision other)
    {
        if(this.transform.position.y < other.transform.position.y)
        {
            if(other.gameObject.name == "Plane")
            {
                this.transform.position = new Vector3(this.transform.position.x, 0.01f, this.transform.position.z);
            }
            else
            {
                float tmpPos = other.transform.position.y + other.transform.localScale.y / 2;
                this.transform.position = new Vector3(this.transform.position.x, tmpPos, this.transform.position.z);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Finish")
        {
            if (other.transform.position == targetPoint)
            {
                this.GetComponent<Rigidbody>().useGravity = true;
                this.GetComponentsInChildren<Collider>()[1].isTrigger = false;

                Vector3 tmpPos = targetPoint;
                targetPoint = SetTargetPosition();

                if (tmpPos.y != targetPoint.y) fly = true;

                if(fly || Distance(tmpPos, targetPoint) > 5f)
                {
                    speed = 3;
                }
                else
                {
                    speed = 1;
                }

                anim.SetFloat("Speed", 0);
                anim.SetBool("isFlying", false);

                stopTime = Random.Range(1, 20);
                time = 0;
            }
        }
    }
}
