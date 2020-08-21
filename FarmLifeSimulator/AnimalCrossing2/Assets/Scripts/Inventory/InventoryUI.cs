using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    Inventory inventory; //для оптимизации сразу его кешируем
    public Transform itemsALL;
    public GameObject InventoryWINDOW;

    InventorySlot[] slots;


    void Start()
    {
        inventory = Inventory.instance; //закидуем сюда наш инвентарь
        inventory.ifChangedItemCallback += UpdateUI; //мы юзаем делегат каждый раз добавляя или удаляя итем, добавляем еще метод UI

        slots = itemsALL.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            InventoryWINDOW.SetActive(!InventoryWINDOW.activeSelf);
        }
        
        if(InventoryWINDOW.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.itemsInInventory.Count)
            {
                slots[i].AddItem(inventory.itemsInInventory[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
