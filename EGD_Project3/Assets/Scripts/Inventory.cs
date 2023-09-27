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

    public List<string> totalItems; // 
    public List<GameObject> itemsShown;

    //private Vector3 itemDefaultPosition = new Vector3(0f, 10f, 10f);
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        itemWidth = canvas.pixelRect.width / itemsNumMax;
        itemHeight = canvas.pixelRect.height / itemsNumMax;

        totalItems = new List<string>();
        foreach (GameObject item in items)
        {
            totalItems.Add(item.name);
        }
        itemsShown = new List<GameObject>();
        itemsDisplay(totalItems);
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

    public void itemsDisplay(List<string> totalItems)
    {
        int index;
        Vector3 nextItemPos = new Vector3(itemWidth / 2, itemHeight, itemDepth);
        for(index = 0; index < totalItems.Count; index++)
        {
            if (totalItems[index] != null)
            {
                Vector3 worldPos = canvasCamera.ScreenToWorldPoint(nextItemPos);
                Debug.Log(getItemByName(totalItems[index]).name);
                itemsShown.Add(Instantiate(getItemByName(totalItems[index]), worldPos, Quaternion.Euler(0, 180, 0), itemsParent.transform));
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
        if(held != null) { AddToInventory(held);  Destroy(held); }

        Vector3 newPos = new Vector3(
            GetComponentInParent<Canvas>().pixelRect.width - itemWidth,
            itemHeight * 1.5f,
            itemDepth);
        newPos = canvasCamera.ScreenToWorldPoint(newPos);
        held = Instantiate(item, newPos, Quaternion.Euler(0,180,0), this.transform);
        held.transform.localScale *= 2;
        closeInventory();

        held.GetComponent<Collider>().enabled = false;
        held.transform.rotation = Quaternion.Euler(0, 180, 0);
        RemoveFromInventory(held);
    }

    public void itemsDisplayUpdate()
    {

    }

    public void AddToInventory(GameObject instance)
    {
        GameObject itemAdd = getItemInTotal(instance);
        if(itemAdd != null)
        {
            totalItems.Add(itemAdd.name);
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
                totalItems.Remove(itemDelete.name);
                //Destroy(itemDelete);
                //Debug.Log(itemsShown.Count);
            }
        }
    }

    public void openInventory()
    {
        GetComponent<Image>().enabled = true;
        itemsDisplay(totalItems);
        //itemsDisplay(itemsShown.ToArray());
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
        bool have = false;
        for (int i = 0; i < totalItems.Count; i++)
        {
           
            if (totalItems[i] == heldName)
            {
                have = true;
                break;
            }
        }
        if(!have) return null;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name == heldName) { return items[i]; }
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

    GameObject getItemByName(string name)
    {
        char separator = '(';
        int indexOfSeparator = name.IndexOf(separator);
        string heldName;
        if (indexOfSeparator != -1)
        {
            heldName = name.Substring(0, indexOfSeparator);
        }
        else heldName = name;
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
