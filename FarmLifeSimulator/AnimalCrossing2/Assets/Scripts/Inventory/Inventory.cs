using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton inventory instance
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More then one instance of inventory found!");
            return;
        }

        instance = this;
    }
    #endregion 

    public delegate void onItemChanged();
    public onItemChanged ifChangedItemCallback;

    public Transform FacingPoint;
    public GameObject playerModel;
    public int inventorySlots = 10;

    public List<Items> itemsInInventory = new List<Items>();

    public bool Add(Items itemToAdd)
    {
        if(itemsInInventory.Count >= inventorySlots)
        {
            Debug.Log("No space left in the inventory! Eat or drop something!");
            return false;
        }

        itemsInInventory.Add(itemToAdd);

        if(ifChangedItemCallback != null)
        {
            ifChangedItemCallback.Invoke();
        }

        return true;
    }

    public void Remove(Items itemToRemove)
    {
        itemsInInventory.Remove(itemToRemove);

        if(ifChangedItemCallback != null)
        {
            ifChangedItemCallback.Invoke();
        }
    }
}
