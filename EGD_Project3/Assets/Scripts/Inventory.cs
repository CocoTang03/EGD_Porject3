using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] items;
    public Camera canvasCamera;
    public GameObject itemsParent;
    private int itemsNumMax = 8;
    private float itemWidth;

    private float itemHeight = 20f;
    private float itemDepth = 0f;


    //private Vector3 itemDefaultPosition = new Vector3(0f, 10f, 10f);
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Screen.width);
        //Instantiate items and put them into the array
        //itemsParent = new GameObject("itemsParent");
        itemWidth = Screen.width / itemsNumMax;
        //Debug.Log(itemWidth);
        //itemDefaultPosition = new Vector3(itemWidth, Screen.height /2, 10f);
        //itemDefaultPosition = canvasCamera.ScreenToWorldPoint(itemDefaultPosition);
        //Instantiate(items[0], itemDefaultPosition, Quaternion.identity, itemsParent.transform);
        itemsList(items);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void itemsList(GameObject[] items)
    {
        Vector3 nextItemPos = new Vector3(itemWidth / 2, itemHeight, itemDepth);
        foreach (GameObject item in items)
        {
            nextItemPos += new Vector3(itemWidth, 0f, 0f);
            Vector3 worldPos = canvasCamera.ScreenToWorldPoint(nextItemPos);
            //worldPos.z = 10f;
            Instantiate(item, worldPos, Quaternion.identity, itemsParent.transform);
            Debug.Log(canvasCamera.WorldToScreenPoint(worldPos));
        }
    }

    public void DropItem()
    {

    }
}
