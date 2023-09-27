using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public GameObject[] items; // const
    public Camera canvasCamera;
    public GameObject itemsParent;

    public bool inventoryOpen = false;
    private int itemsNumMax = 9;
    private float itemWidth;
    private float itemHeight;
    private float itemDepth = 2f;
    public GameObject held;

    private GameObject[] totalItems; // 
    public List<GameObject> itemsShown;

    //private Vector3 itemDefaultPosition = new Vector3(0f, 10f, 10f);
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        itemWidth = canvas.pixelRect.width / itemsNumMax;
        itemHeight = canvas.pixelRect.height / itemsNumMax;

        totalItems = items;
        itemsShown = new List<GameObject>();
        itemsDisplay(items);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab Detect");
            if(inventoryOpen)
            {
                closeInventory();
            }
            else openInventory();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Right button down");

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            // Details
            if(!inventoryOpen && held != null)
            {

            }
        }
        for(int i = 0; i <= 9; i++)
        {
            //if (Input.GetKeyDown(KeyCode.Alpha0)) continue;
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                Debug.Log("Number key" + i + "is pressed");
                if (inventoryOpen)
                {
                    Debug.Log(itemSelected(i - 1).name);

                    holdItem(itemSelected(i - 1));
                }
            }
        }
    }

    public void itemsDisplay(GameObject[] items)
    {
        int index;
        Vector3 nextItemPos = new Vector3(itemWidth / 2, itemHeight, itemDepth);
        for(index = 0; index < itemsNumMax; index++)
        {
            if (items[index] != null)
            {
                Vector3 worldPos = canvasCamera.ScreenToWorldPoint(nextItemPos);
                itemsShown.Add(Instantiate(items[index], worldPos, Quaternion.Euler(0, 180, 0), itemsParent.transform));
                nextItemPos += new Vector3(itemWidth, 0f, 0f);
            }
            else
            {
                if (itemsShown[index] != null)
                {
                    //itemsShown
                    Destroy(itemsShown[index]);
                }
            }
        }
    }

    public void holdItem(GameObject item)
    {
        if(held != null) { Destroy(held); }

        Vector3 newPos = new Vector3(
            GetComponentInParent<Canvas>().pixelRect.width - itemWidth,
            itemHeight * 1.5f,
            itemDepth);
        newPos = canvasCamera.ScreenToWorldPoint(newPos);
        held = Instantiate(item, newPos, Quaternion.Euler(0,180,0), this.transform);
        held.transform.localScale *= 2;
        closeInventory();
        //held.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //held.layer = 0;
        //held.AddComponent<ObjectMovement>();
        held.GetComponent<Collider>().enabled = false;
        held.transform.rotation = Quaternion.Euler(0, 180, 0);
        //RemoveFromInventory(held);
        //held.GetComponent<BoxCollider>().enabled = false;
    }

    public void itemsDisplayUpdate()
    {

    }

    public void AddToInventory(GameObject instance)
    {
        GameObject itemAdd = getItemInTotal(instance);
        if(itemAdd != null)
        {
            itemsShown.Add(itemAdd);
        }
        //itemsDisplay(itemsShown.ToArray());
    }

    public void RemoveFromInventory(GameObject instance)
    {
        if(instance != null)
        {
            //Find the object in the inventory and delete it.
            GameObject itemDelete = getItemInInventory(instance);
            if(itemDelete != null)
            {
                itemsShown.Remove(itemDelete);
                Destroy(itemDelete);
                //Debug.Log(itemsShown.Count);
            }
        }
        //itemsDisplay(itemsShown.ToArray());
    }

    public void openInventory()
    {
        GetComponent<Image>().enabled = true;
        itemsDisplay(totalItems);
        inventoryOpen = true;

        if(held != null)
        {
            MeshRenderer[] meshes = held.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer mesh in meshes)
            {
                mesh.enabled = false;
            }
        }
    }

    public void closeInventory()
    {
        GetComponent<Image>().enabled = false;
        foreach (GameObject item in itemsShown)
        {
            Destroy(item);
        }
        itemsShown.Clear();
        inventoryOpen = false;
        if(held != null)
        {
            MeshRenderer[] meshes = held.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = true;
            }
        }
    }

    public GameObject itemSelected(int index)
    {
        return itemsShown[index];
    }

    GameObject getItemInInventory(GameObject held) // for zoom in
    {
        char separator = '(';
        int indexOfSeparator = held.name.IndexOf(separator);
        string heldName = held.name.Substring(0, indexOfSeparator);
        for (int i = 0; i < itemsShown.Count; i++)
        {
            indexOfSeparator = itemsShown[i].name.IndexOf(separator);
            string itemName = itemsShown[i].name.Substring(0, indexOfSeparator);
            if (itemName == heldName)
            {
                return itemsShown[i];
            }
        }
        return null;
    }

    GameObject getItemInTotal(GameObject held)
    {
        char separator = '(';
        int indexOfSeparator = held.name.IndexOf(separator);
        string heldName = held.name.Substring(0, indexOfSeparator);
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name == heldName)
            {
                return items[i];
            }
        }
        return null;
    }
}
