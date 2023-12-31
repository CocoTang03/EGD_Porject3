using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_placement : MonoBehaviour
{
    //public GameObject targetObject; // Object to put in case
    public bool occupied = false;
    public GameObject newObject;
    public void MoveObject(GameObject targetObject)
    {
        if (targetObject != null)
        {
            // Rotate the targetObject to match the rotation of the invisible cube
            targetObject.transform.rotation = transform.rotation;

            // For combined bounds of all the meshes in the targetObject
            Bounds combinedBounds = new Bounds(targetObject.transform.position, Vector3.zero);

            MeshFilter[] meshFilters = targetObject.GetComponentsInChildren<MeshFilter>();
            foreach (var meshFilter in meshFilters)
            {
                combinedBounds.Encapsulate(meshFilter.mesh.bounds);
            }
            //combinedBounds.Encapsulate(targetObject.GetComponent<MeshFilter>().mesh.bounds);

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

            newObject = Instantiate(targetObject, transform.position, transform.rotation) as GameObject;  // instatiate the object
            newObject.transform.localScale = scaleFactor; // change its local scale in x y z forma
            //SoundPutItems(targetObject, this.gameObject);

            // Rescaling and positioning
            //targetObject.transform.localScale = scaleFactor;
            //targetObject.transform.position = transform.position;
            //this.gameObject.GetComponentInParent<AudioSource>().Play();
            StartCoroutine(SoundPutItems(newObject, this.gameObject));
            occupied = true;
            targetObject.transform.localScale = scaleFactor;
            targetObject.transform.position = transform.position;
        }
    }

    IEnumerator SoundPutItems(GameObject held, GameObject collider)
    {
        Debug.Log(collider.GetComponentInParent<AudioSource>());
        collider.GetComponentInParent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        Debug.Log(held);
        held.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(5);
        held.GetComponent<AudioSource>().Stop();
    }
}