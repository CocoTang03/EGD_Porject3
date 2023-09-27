using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameJudgement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objFerriWheels;
    public GameObject[] objHockeys;
    public GameObject[] objFounders;

    public GameObject frameFerrisWheel;
    public GameObject frameHockey;
    public GameObject frameFounders;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCompleted(objFerriWheels, "ferris"))
        {
            frameFerrisWheel.GetComponent<MeshRenderer>().enabled = true;
        }
        if(isCompleted(objHockeys, "hockey"))
        {
            frameHockey.GetComponent<MeshRenderer>().enabled = true;
        }
        if(isCompleted(objFounders, "founder")){
            frameFounders.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    bool isCompleted(GameObject[] objs, string tag)
    {
        foreach (GameObject obj in objs)
        {
            if(!obj.GetComponent<Test_placement>().occupied) return false;
            string objTag = obj.GetComponent<Test_placement>().newObject.tag;
            if(objTag != tag) return false;
        }
        return true;
    }
}
