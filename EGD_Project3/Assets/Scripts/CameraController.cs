using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 3.0f;
    [SerializeField] private float smoothTime = 0.1f;

    public Transform orientation;

    private float rotationY;
    private float rotationX;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    private Camera playCam;
    private float range = 5f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playCam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        Vector3 nextRotation = new Vector3(rotationX, rotationY);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);

        transform.localEulerAngles = currentRotation;
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);

    }

    private void Update()
    {
        Vector3 rayOrigin = playCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;


        if (Physics.Raycast(rayOrigin, playCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.collider.name);
            if (hit.collider.tag == "itemPosition" && !hit.collider.GetComponent<Test_placement>().occupied)
            {
                Debug.Log(hit.collider.name);
                GameObject held_obj = GameObject.Find("Background").GetComponent<Inventory>().held;
                if (held_obj != null)
                {
                    held_obj.layer = 0;
                    foreach(Transform child in held_obj.transform)
                    {
                        child.gameObject.layer = 0;
                        foreach(Transform child2 in child.transform)
                        {
                            child2.gameObject.layer = 0;
                        }
                    }
                    hit.collider.GetComponent<Test_placement>().MoveObject(held_obj);
                    held_obj.SetActive(true);

                    GameObject.Find("Background").GetComponent<Inventory>().RemoveFromInventory(held_obj);
                    Destroy(held_obj);
                }

            }
        }
    }
}