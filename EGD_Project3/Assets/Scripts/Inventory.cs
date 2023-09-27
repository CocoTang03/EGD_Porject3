using System.Collections;
using System.Collections.Generic;
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
    private GameObject[] itemsShown;

    //private Vector3 itemDefaultPosition = new Vector3(0f, 10f, 10f);
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        itemWidth = canvas.pixelRect.width / itemsNumMax;
        itemHeight = canvas.pixelRect.height / itemsNumMax;

        totalItems = items;
        itemsShown = new GameObject[itemsNumMax];
        //Debug.Log(items[0].name);
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
        for(int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i ))
            {
                Debug.Log("Number key" + i + "is pressed");
                if (inventoryOpen)
                {
                    Debug.Log(itemSelected(i-1).name);

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
                itemsShown[index] = Instantiate(items[index], worldPos, Quaternion.Euler(0, 180, 0), itemsParent.transform);
                nextItemPos += new Vector3(itemWidth, 0f, 0f);
            }
            else
            {
                if (itemsShown[index] != null)
                {
                    Destroy(itemsShown[index]);
                }
            }
        }
    }

    public void holdItem(GameObject item)
    {
        if(held != null) { Destroy(held); }

        held = Instantiate(item, GameObject.Find("Player").transform);
        held.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        held.layer = 0;
        held.AddComponent<ObjectMovement>();
        held.GetComponent<BoxCollider>().enabled = false;
    }

    public void itemsDisplayUpdate()
    {

    }

    public void DropItem()
    {

    }

    public void openInventory()
    {
        GetComponent<Image>().enabled = true;
        itemsDisplay(totalItems);
        inventoryOpen = true;
    }

    public void closeInventory()
    {
        GetComponent<Image>().enabled = false;
        foreach (GameObject item in itemsShown)
        {
            Destroy(item);
        }
        inventoryOpen = false;
    }

    public GameObject itemSelected(int index)
    {
        return itemsShown[index];
    }
}
