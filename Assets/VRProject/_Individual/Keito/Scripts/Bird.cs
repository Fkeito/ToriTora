﻿using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
public class Bird : VRObjectBase {

    [SerializeField]
    private Camera VRCamera, NotVRCamera;
    private Camera mainCamera;
    [SerializeField]
    private GameObject hand1,hand2;
    private Hand con1,con2;

    private bool VR = false;

    public bool inCage = true;//鳥が籠の中にいるか
    private bool flyFlag = true;//鳥が飛べるか
    public bool isFly = false;//鳥が飛んでいるか
    public bool isFlyBack = false;//鳥が戻ってきてるか

    private GameObject obj;//持ってきたいオブジェクト
    private VRObjectMode objMode;//持ってきたいオブジェクトのモード
    private Vector3 direction;//飛行の中心軸

    [SerializeField]
    private GameObject circle;
    private GameObject tmpDirection;

    public Animator anim;

    void Start()
    {
        if (hand1.transform.parent.gameObject.activeSelf)
        {
            VR = true;
            con1 = hand1.GetComponent<Hand>();
            con2 = hand2.GetComponent<Hand>();
            mainCamera = VRCamera;
        }
        else
        {
            mainCamera = NotVRCamera;
        }

    }

    void Update()
    {
        if (VR)
        {

            if (con1.controller != null) {
                
                if (con1.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)
                    && con1.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    SetFly(hand1.transform.position, hand1.transform.forward);
                }
                else if (con1.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    SetDirection(hand1.transform.position, hand1.transform.forward);
                }
                if (con1.controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    Destroy(tmpDirection);
                    tmpDirection = null;
                }
            }
            if(con2.controller != null) {
                
                if (con2.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)
                    && con2.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    SetFly(hand2.transform.position, hand2.transform.forward);
                }
                else if (con2.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    SetDirection(hand2.transform.position, hand2.transform.forward);
                }
                if (con2.controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    Destroy(tmpDirection);
                    tmpDirection = null;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift)
                && transform.parent == null) SetFly();
            else if (Input.GetKey(KeyCode.LeftShift)) SetDirection();
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Destroy(tmpDirection);
                tmpDirection = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            VR = hand1.transform.parent.gameObject.activeSelf;
            if (VR)
            {
                mainCamera = VRCamera;
            }
            else
            {
                mainCamera = NotVRCamera;
            }
        }
    }

    void Fly(Vector3 startPos)
    {
        transform.position = startPos + direction;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = direction * 5;

        isFly = true;
        flyFlag = false;

        anim.SetBool("isFlying", true);
    }
    void FlyBack()
    {
        transform.LookAt(mainCamera.transform.position);

        direction = (mainCamera.transform.position - transform.position).normalized;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = direction * 5;
        isFlyBack = true;

        anim.SetBool("isFlyingBack", true);

        Invoke("FinishFly", 3f);
    }

    void SetFly(Ray ray)
    {
        Debug.Log("Set");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Hit");

            transform.LookAt(hit.point);
            rigidBody.useGravity = false;

            direction = (hit.point - ray.origin).normalized;
            Debug.Log(GetVRObjectMode());
            SetVRObjectMode(VRObjectMode.None);
            Debug.Log(GetVRObjectMode());

            Fly(ray.origin);
        }
    }
    void SetFly()
    {
        if (!flyFlag || inCage) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        SetFly(ray);
    }
    void SetFly(Vector3 startPos, Vector3 direction)
    {
        if (!flyFlag || inCage) return;

        Ray ray = new Ray(startPos, direction);
        SetFly(ray);
    }

    void FinishFly()
    {
        if (!isFlyBack) return;

        if (obj)
        {
            obj.gameObject.GetComponent<ObjectForBird>().caught = false;
            obj.transform.parent = null;
        }
        rigidBody.velocity *= 0.5f;
        rigidBody.useGravity = true;

        anim.SetBool("isFlying", false);
        anim.SetBool("isFlyingBack", false);

        obj = null;
        objMode = VRObjectMode.None;
        direction = Vector3.zero;
    }

    void SetDirection()
    {
        if (!flyFlag || inCage) return;
        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 5,Color.red);
            //tmpDirection = a.OnDirection(Direction, ray.origin, hit.point);  

            if (tmpDirection)
            {
                tmpDirection.transform.position = hit.point + hit.normal * 0.1f;
                tmpDirection.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
            else
            {
                tmpDirection = BirdDirection.OnDirection(circle, hit);
            }
        }
    }
    void SetDirection(Vector3 startPos, Vector3 direction)
    {
        if (!flyFlag || inCage) return;

        Ray ray = new Ray(startPos, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 5,Color.red);
            //tmpDirection = a.OnDirection(Direction, ray.origin, hit.point);  

            if (tmpDirection)
            {
                tmpDirection.transform.position = hit.point + hit.normal;
                tmpDirection.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
            else
            {
                tmpDirection = BirdDirection.OnDirection(circle, hit);
            }
        }
    }

    void SetForward()
    {
        Vector3 tmpForward = this.transform.forward;
        tmpForward.y *= 0;
        this.transform.forward = tmpForward.normalized;
    }

    void OnCollisionEnter(Collision other)
    {
        if (isFly)
        {
            var tmpObj = other.gameObject.GetComponent<ObjectForBird>();
            if (tmpObj)
            {
                if (tmpObj.GetComponent<VRObjectBase>().GetVRObjectMode() != VRObjectMode.NeverMove)
                {
                    Debug.Log("Catch");
                    objMode = tmpObj.GetVRObjectMode();
                    //tmpObj.SetVRObjectMode(VRObjectMode.None);
                    obj = other.transform.gameObject;
                    tmpObj.caught = true;
                    obj.transform.parent = this.transform;
                }
            }
            //SetVRObjectMode(VRObjectMode.Grabable);

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            Invoke("FlyBack", 0.5f);
        }

        isFly = false;
        //isFlyBack = false;
        flyFlag = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (isFlyBack)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SetForward();
                FinishFly();
                isFlyBack = false;
            }
        }
    }
}
