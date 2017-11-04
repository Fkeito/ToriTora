using UnityEngine;
using System.Collections;

public class Dentaku : MonoBehaviour {
    public GameObject Time0, Time1, Time2, Time3;
    private bool time0,time1, time2, time3;
    public Material[] materials;
    public static Dentaku dentaku;
    int index1,index2,index3,index4;
    
    void Awake() {
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
            if (index1 == 1 && index2 == 0 && index3 == 2 && index4 == 5)
            {
                Debug.Log("正解！");
            }
            else
            {
                StartCoroutine("Destroy");
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
