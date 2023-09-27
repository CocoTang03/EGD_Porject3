using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class ObjectMovement : MonoBehaviour
{
    public GameObject player;

    public bool inPos = false;
    private Inventory inventory;
    private GameObject[] prefabs;
    private GameObject prefab;
    private bool detailedItemOnScreen = false;
    Canvas canvas;
    Vector3 detailedPos;

    public Camera playerCamera;
    public Transform orientation;
    Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerCamera = GameObject.Find("Camera").GetComponent<Camera>();
        orientation = GameObject.Find("Orientation").transform;
        //canvas = GetComponentInParent<Canvas>();
        //detailedPos = new Vector3(canvas.pixelRect.width / 2, canvas.pixelRect.height / 2, 2f);

        inventory = GameObject.Find("Background").GetComponent<Inventory>();
        prefabs = inventory.items;
        getPrefab(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = orientation.forward*1.452f + orientation.right*0.229f;
        moveDirection.y = 2.5f;
        transform.position = moveDirection;
        if (Input.GetMouseButtonDown(1))
        {
            if(prefab == null) zoomIn();
        }
        else if (Input.GetMouseButtonDown(0)) 
        {
            // hold object
            //Vector3 spawnPos = player.transform.position + new Vector3(10, 0, 10);
            //GameObject held = Instantiate(prefab, Vector3.zero,Quaternion.Euler(0,180,0),player.transform);
            //held.transform.localScale = Vector3.one;
            //held.layer = 0;
            //Destroy(this);
        }
    }

    void getPrefab(GameObject instance) // for zoom in
    {
        char separator = '(';
        int indexOfSeparator = instance.name.IndexOf(separator);
        string prefabName = instance.name.Substring(0, indexOfSeparator);
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i].name == prefabName)
            {
                prefab = prefabs[i];
                break;
            }
        }
    }

    public void zoomIn()
    {
        Debug.Log(gameObject.name);
        getPrefab(gameObject);
        Camera canvasCamera = GameObject.Find("CanvasCamera").GetComponent<Camera>();
        Vector3 pos = canvasCamera.ScreenToWorldPoint(detailedPos);
        //prefab = Instantiate(prefabs[i], pos, Quaternion.identity, canvas.transform);
        //prefab.transform.localScale *= 5;
        detailedItemOnScreen = true;
    }

    public void zoomOut()
    {

    }

    public void Drag()
    {
        
    }
}
