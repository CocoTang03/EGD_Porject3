using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameJudgement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objFerriWheels;
    public GameObject[] objHockeys;
    public GameObject[] objFounders;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isCompleted(GameObject[] objs, string tag)
    {
        foreach (GameObject obj in objs)
        {
            if(!obj.GetComponent<Test_placement>().occupied) return false;

        }
        return true;
    }
}
