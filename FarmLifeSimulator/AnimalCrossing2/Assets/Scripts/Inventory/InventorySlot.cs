using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Items currentItem; //item in slot
    public Button removeButton;
    public Button slotButton;
    Vector3 playerPos;
    RaycastHit hit;
    Ray ray;

    public void AddItem(Items newItem)
    {
        currentItem = newItem;
        icon.sprite = currentItem.itemIcon;
        icon.enabled = true;
        slotButton.interactable = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        currentItem = null;

        icon.sprite = null;
        icon.enabled = false;
        slotButton.interactable = false;
        removeButton.interactable = false;
    }


    public void OnRemoveButton()
    {
        if(currentItem != null && currentItem.isDropable == true)
        {
            DropItemOnGround();
        }
        Inventory.instance.Remove(currentItem);
    }

    public void DropItemOnGround()
    {
            playerPos = Inventory.instance.FacingPoint.transform.position;
            GameObject dropedItem = Instantiate(currentItem.itemToDrop, playerPos, Quaternion.identity);


    }

    public void PlaceItem()
    {
        playerPos = Inventory.instance.FacingPoint.transform.position;
        GameObject spawnedItem = Instantiate(currentItem.itemToSpawnPlacedOrHanded, playerPos, Quaternion.RotateTowards(Inventory.instance.playerModel.transform.rotation, Inventory.instance.FacingPoint.rotation, -180f));
        spawnedItem.GetComponentInChildren<Rigidbody>().freezeRotation = true;
        Inventory.instance.Remove(currentItem);
    }

    public void UseItem()
    {
        if (currentItem != null && currentItem.isUsable == true)
        {
            currentItem.Use();
            Inventory.instance.Remove(currentItem);
        }
        else if (currentItem != null && currentItem.isPlacing == true)
        {
            PlaceItem();

        }
    }

    
}
