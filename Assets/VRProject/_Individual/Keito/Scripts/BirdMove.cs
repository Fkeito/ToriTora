using UnityEngine;
using System.Collections;

public class BirdMove : MonoBehaviour
{
    [SerializeField]
    private MovingMode movingMode;

    [SerializeField]
    private StopMode stopMode;
    [SerializeField]
    private float stopTime;

    [SerializeField]
    private Vector3[] movePoint;

    private Vector3 targetPoint;
    private int targetNum = 0;

    private float speed = 3;

    private Animator anim;

    private bool moveFlag = false;
    private bool fly = false;
    private float time = 0;
    private float stoptime = 0;

    private Bird bird;

    void Start()
    {
        //Time.timeScale = 2;

        if (movePoint.Length == 0) Destroy(this);
        if (movingMode == MovingMode.NeverMove) Destroy(this);

        bird = this.gameObject.GetComponent<Bird>();
        anim = bird.anim;

        if (bird.isFly || bird.isFlyBack || anim.GetBool("inCage")) return;

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
        if (bird.isFly || bird.isFlyBack || anim.GetBool("inCage")) return;

        Move();

        if (time > stoptime)
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
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.GetComponentsInChildren<Collider>()[1].isTrigger = true;

            }
        }
        else
        {
            moveFlag = false;
            if (anim.GetBool("Action")) anim.SetBool("Action", false);
            if (anim.GetBool("isPecking")) anim.SetBool("isPecking", false);

            if (time > 5 && time < 6 && stoptime > 10)
            {
                anim.SetBool("Action", true);
            }
            else if (time > 8 && time < 9 && stoptime > 15)
            {
                anim.SetBool("isPecking", true);
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
        int tmpNum = 0;

        if (movingMode == MovingMode.Random) tmpNum = Random.Range(0, movePoint.Length);
        else if (movingMode == MovingMode.RightOrder)
        {
            if (targetNum == movePoint.Length) Destroy(this);
            tmpNum = targetNum++;
        }
        else if(movingMode == MovingMode.Repetition)
        {
            if (targetNum == movePoint.Length) targetNum = 0;
            tmpNum = targetNum++;
        }

        return movePoint[tmpNum];
    }

    void SetForward()
    {
        Vector3 tmpForward = this.transform.forward;
        tmpForward.y *= 0;
        this.transform.forward = Vector3.Slerp(this.transform.forward, tmpForward.normalized, Time.deltaTime * 2);
    }

    float Distance(Vector3 a, Vector3 b)
    {
        float answer = (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
        return Mathf.Sqrt(answer);
    }

    void OnCollisionStay(Collision other)
    {
        if (this.transform.position.y < other.transform.position.y)
        {
            if (other.gameObject.name == "Plane")
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

                if (fly || Distance(tmpPos, targetPoint) > 5f)
                {
                    speed = 3;
                }
                else
                {
                    speed = 1;
                }

                anim.SetFloat("Speed", 0);
                anim.SetBool("isFlying", false);

                if (stopMode == StopMode.Random) stoptime = Random.Range(1, 20);
                else if (stopMode == StopMode.Fix) stoptime = stopTime;
                else stoptime = 0;

                time = 0;
            }
        }
    }
}
enum MovingMode
{
    Random,
    RightOrder,
    Repetition,
    NeverMove
}
enum StopMode
{
    Random,
    Fix,
    Zero
}
