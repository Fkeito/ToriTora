using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    [Range(0, 3)]
    public int gimmick;

    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    private Renderer rend1;
    private Renderer rend2;
    private Renderer rend3;
    private bool l1 = false;
    private bool l2 = false;
    private bool l3 = false;

    public Material on;
    public Material off;

    public Animator anim;

	void Start () {
        rend1 = light1.GetComponent<Renderer>();
        rend2 = light2.GetComponent<Renderer>();
        rend3 = light3.GetComponent<Renderer>();
        rend1.material = off;
        rend2.material = off;
        rend3.material = off;
    }
	
	void Update () {
        ChangeColor(gimmick);

        if(gimmick == 3)
        {
            if (!anim.GetBool("clear")) anim.SetBool("clear", true);
        }
        else
        {
            if (anim.GetBool("clear")) anim.SetBool("clear", false);
        }
	}

    void ChangeColor(int n)
    {
        switch (n)
        {
            case 0:
                if (l1)
                {
                    rend1.material = off;
                    l1 = false;
                }
                break;
            case 1:
                if (!l1)
                {
                    rend1.material = on;
                    l1 = true;
                }
                if (l2)
                {
                    rend2.material = off;
                    l2 = false;
                }
                break;
            case 2:
                if (!l2)
                {
                    rend2.material = on;
                    l2 = true;
                }
                if (l3)
                {
                    rend3.material = off;
                    l3 = false;
                }
                break;
            case 3:
                if (!l3)
                {
                    rend3.material = on;
                    l3 = true;
                }
                break;
        }
    }

    void Pull()
    {
        if (anim.GetBool("pulling")) return;
        anim.SetBool("pulling", true);
    }
}
