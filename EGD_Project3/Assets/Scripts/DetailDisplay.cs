using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailDisplay : MonoBehaviour
{
    [SerializeField] private float sensitivity = 3.0f;
    [SerializeField] private float distanceFromTarget = 3.0f;
    public Transform target;
    [SerializeField] private float smoothTime = 0.1f;

    private float rotationY;
    private float rotationX;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationY += mouseX;
            rotationX += mouseY;

            rotationX = Mathf.Clamp(rotationX, 15, 45);
            Vector3 nextRotation = new Vector3(rotationX, rotationY);
            currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);

            transform.localEulerAngles = currentRotation;

            transform.position = target.position - transform.forward * distanceFromTarget;
        }

    }


}
