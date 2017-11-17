using UnityEngine;
using System.Collections;


public class Dentaku : VRObjectBase {
    public GameObject Time0, Time1, Time2, Time3;
    private bool time0,time1, time2, time3;
    public Material[] materials;
    public static Dentaku dentaku;
    int index1,index2,index3,index4;
    private AudioSource sound01;
    private AudioSource sound02;

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];
    }

    public override void Awake() {
        base.Awake();
        dentaku = this;
    }

    public void Pull(int index) {
        if (!time0)
        {
            hyozi(Time0, index);
            time0 = true;
            index1 = index;
        }
        else if (!time1)
        {
            hyozi(Time1, index);
            time1 = true;
            index2 = index;
        }
        else if (!time2)
        {
            hyozi(Time2, index);
            time2 = true;
            index3 = index;
        }
        else if (!time3)
        {
            hyozi(Time3, index);
            index4 = index;
            if (index1 == 3 && index2 ==4 && index3 == 0 && index4 == 8)
            {
                DoorController.door.Clear();
                sound01.PlayOneShot(sound01.clip);
            }
            else
            {
                StartCoroutine("Destroy");
                sound02.PlayOneShot(sound02.clip);
            }
        }

    }
   

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        hyozi(Time0, 10);
        hyozi(Time1, 10);
        hyozi(Time2, 10);
        hyozi(Time3, 10);
        time0 = false;
        time1 = false;
        time2 = false;
    }

  public void hyozi(GameObject obj,int index) {
        obj.GetComponent<MeshRenderer>().material = materials[index];
    }
 
}
