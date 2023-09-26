using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] items;
    public Camera canvasCamera;
    public GameObject itemsParent;
    private int itemsNumMax = 9;
    private float itemWidth;

    public bool inventoryOpen = true;
    private float itemHeight;
    private float itemDepth = 2f;

    private GameObject[] itemsShown;

    //private Vector3 itemDefaultPosition = new Vector3(0f, 10f, 10f);
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        itemWidth = canvas.pixelRect.width / itemsNumMax;
        itemHeight = canvas.pixelRect.height / itemsNumMax;

        itemsShown = new GameObject[itemsNumMax];
        //Debug.Log(items[0].name);
        itemsDisplay();
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
    }

    private void FixedUpdate()
    {


    }

    public void itemsDisplay()
    {
        int index = 0;
        Vector3 nextItemPos = new Vector3(itemWidth / 2, itemHeight, itemDepth);
        foreach (GameObject item in items)
        {
            Vector3 worldPos = canvasCamera.ScreenToWorldPoint(nextItemPos);
            itemsShown[index] = Instantiate(item, worldPos, Quaternion.Euler(0, 180, 0), itemsParent.transform);
            nextItemPos += new Vector3(itemWidth, 0f, 0f);
            index++;
            if (index >= itemsNumMax) break;
        }
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
        itemsDisplay();
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
}
