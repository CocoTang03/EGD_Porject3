using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource footstep;
    [SerializeField] float speed = 10f;
    [SerializeField] float minMoveDistance = 0.1f;
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 previousPosition;

    Vector3 moveDirection;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;
        previousPosition = transform.position;
    }

    // Update is called once per frame


    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        moveDirection = orientation.forward*verticalInput + orientation.right*horizontalInput;

        //rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime);

        float moveDistance = Vector3.Distance(transform.position, previousPosition);
        if (moveDistance >= minMoveDistance)
        {
            //Debug.Log("GET into");
            playFootsteps(footstep);
            previousPosition = transform.position;
        }
    }

    private IEnumerator playFootsteps(AudioSource footstep)
    {
        footstep.Play();
        yield return new WaitForSeconds(footstep.clip.length);
        
    }
}
