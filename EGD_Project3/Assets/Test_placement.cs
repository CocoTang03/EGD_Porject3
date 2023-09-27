using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_placement : MonoBehaviour
{
    public GameObject targetObject; // Assign the target GameObject in the Inspector

    private void Update()
    {
        if (targetObject != null)
        {
            // Rotate the targetObject to match the rotation of the invisible cube
            targetObject.transform.rotation = transform.rotation;

            // Calculate the scale factor to fit the original bounds within the invisible cube
            Vector3 scaleFactor = Vector3.one;
            Vector3 cubeSize = transform.localScale;

            if (cubeSize.x > 0 && cubeSize.y > 0 && cubeSize.z > 0)
            {
                // Calculate a uniform scale factor based on the maximum scale component
                float maxScaleComponent = Mathf.Max(targetObject.transform.localScale.x, targetObject.transform.localScale.y, targetObject.transform.localScale.z);
                scaleFactor = Vector3.one * (maxScaleComponent / Mathf.Max(cubeSize.x, cubeSize.y, cubeSize.z));
            }

            // Calculate the position offset based on the original object's position
            Vector3 offset = (targetObject.transform.localScale - Vector3.one) * 0.5f;

            // Position the targetObject relative to the center of the invisible cube
            targetObject.transform.position = transform.position + offset;
        }
    }

}
