using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetecting : MonoBehaviour
{
    List<Collider> framedObjects = new List<Collider>();
    int numFramed;

    public GameObject emptyFrame;
    private AudioSource frameAudio;

    // Start is called before the first frame update
    void Start()
    {
        frameAudio = emptyFrame.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(framedObjects.Count);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("object"))
        {
            if(!framedObjects.Contains(other))
            {
                framedObjects.Add(other);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("object"))
        {
            if(framedObjects.Contains(other))
            {
                framedObjects.Remove(other);
            }
            
        }
    }

    public void EmptyFrameAudio()
    {
        if (framedObjects.Count != 0)
        {
            frameAudio.Stop();
        }

        else
        {
            frameAudio.Play();
        }
    }
}
