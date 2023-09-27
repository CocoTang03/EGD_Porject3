using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50f;

    public float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Mouse X");
        horizontalInput *= -1f;
        transform.Rotate(0f, rotateSpeed * Time.deltaTime* horizontalInput, 0f, Space.Self);
    }


}
