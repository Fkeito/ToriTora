using UnityEngine;
using System.Collections;

public class BirdMove : MonoBehaviour
{

    [SerializeField]
    private Vector3[] movePoint;

    private Vector3 targetPoint;

    private float speed = 3;

    private Animator anim;

    private bool moveFlag = true;
    private float time = 0;
    private float stopTime = 0;

    void Start()
    {
        if (movePoint.Length == 0) Destroy(this);

        anim = this.gameObject.GetComponent<Bird>().anim;

        Vector3 tmpPos = Vector3.zero;
        targetPoint = SetTargetPosition();

        if (Distance(tmpPos, targetPoint) < 5f)
        {
            speed = 0.1f;
        }
        else
        {
            speed = 3;
        }

        if (tmpPos.y != targetPoint.y)
        {
            anim.SetFloat("Speed", 0);
            anim.SetBool("isFlying", true);
        }
        else
        {
            anim.SetBool("isFlying", false);
            anim.SetFloat("Speed", speed);
        }
        Debug.Log(targetPoint);

        for (int i = 0; i < movePoint.Length; i++)
        {
            GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tmp.GetComponent<Collider>().isTrigger = true;
            tmp.transform.position = movePoint[i];
            tmp.tag = "Finish";
            tmp.GetComponent<Renderer>().enabled = false;
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > stopTime)
        {
            this.transform.Translate(Vector3.forward * 3 * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * 3);
        }
    }

    Vector3 SetTargetPosition()
    {
        int tmpNum = Random.Range(0, movePoint.Length);
        return movePoint[tmpNum];
    }

    float Distance(Vector3 a,Vector3 b)
    {
        float answer = (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
        return Mathf.Sqrt(answer);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Finish")
        {
            if (other.transform.position == targetPoint)
            {
                Vector3 tmpPos = targetPoint;
                targetPoint = SetTargetPosition();

                if(Distance(tmpPos,targetPoint) < 5f)
                {
                    speed = 0.1f;
                }
                else
                {
                    speed = 3;
                }

                if (tmpPos.y != targetPoint.y)
                {
                    anim.SetFloat("Speed", 0);
                    anim.SetBool("isFlying", true);
                }
                else
                {
                    anim.SetBool("isFlying", false);
                    anim.SetFloat("Speed", speed);
                }

                //stopTime = Random.Range(1, 20);
                time = 0;

                Debug.Log(targetPoint);
                Debug.Log(Distance(tmpPos, targetPoint));
            }
        }
    }
}
