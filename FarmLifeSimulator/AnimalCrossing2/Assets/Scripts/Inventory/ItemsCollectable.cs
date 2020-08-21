using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCollectable : MonoBehaviour
{
    public Items item;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        Debug.Log("Collecting an item " + item.itemName);
        Debug.Log("Description: " + item.itemDescription);
        bool ifPickedUp = Inventory.instance.Add(item); //использую Синглтон для того чтобы не писать FindGameObjectWithTag<Inventory>().Add
        if (ifPickedUp == true)
        {
            Destroy(gameObject);
        }  
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);
    }
}
