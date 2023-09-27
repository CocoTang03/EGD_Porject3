using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_placement : MonoBehaviour
{
    //public GameObject targetObject; // Object to put in case

    public void MoveObject(GameObject targetObject)
    {

        if (targetObject != null)
        {
            // Rotate the targetObject to match the rotation of the invisible cube
            targetObject.transform.rotation = transform.rotation;

            // For combined bounds of all the meshes in the targetObject
            Bounds combinedBounds = new Bounds(targetObject.transform.position, Vector3.zero);

            // Logic for dealing with objects with child meshes
            combinedBounds.Encapsulate(targetObject.GetComponent<MeshFilter>().mesh.bounds);

            // Recalculate object size
            Vector3 scaleFactor = Vector3.one;
            Vector3 cubeSize = transform.localScale;
            float denominator = Mathf.Min(combinedBounds.size.x, combinedBounds.size.y, combinedBounds.size.z);

            if (cubeSize.x > 0 && cubeSize.y > 0 && cubeSize.z > 0)
            {
                scaleFactor.x = cubeSize.x / denominator;
                scaleFactor.y = cubeSize.y / denominator;
                scaleFactor.z = cubeSize.z / denominator;
            }

            // Rescaling and positioning
            targetObject.transform.localScale = scaleFactor;
            targetObject.transform.position = transform.position;
        }
    }
}
