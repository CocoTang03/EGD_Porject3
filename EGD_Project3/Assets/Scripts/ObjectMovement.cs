using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDrag()
    {
        //Debug.Log("DETECT");
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set the depth to match your camera's near clip plane.
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

}
