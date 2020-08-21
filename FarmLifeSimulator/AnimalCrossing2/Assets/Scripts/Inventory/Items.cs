using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="New Item", menuName ="Inventory/Item")]
public class Items : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public bool isUsable = false;
    public bool isDropable = true;
    public bool isPlacing = false;
    public bool isHanding = false;
    public int staminaAdd;
    public int healthAdd;
    public int creativityAdd;
    public Sprite itemIcon;
    public GameObject itemToSpawnPlacedOrHanded;
    public GameObject itemToDrop;


    public Items(string name, string description, bool dropable, bool usable, bool placing, bool handing, int stamina, int health, int creativity)
    {
        this.itemName = name;
        this.isDropable = dropable;
        this.itemDescription = description;
        this.isUsable = usable;
        this.isPlacing = placing;
        this.isHanding = handing;
        this.staminaAdd = stamina;
        this.healthAdd = health;
        this.creativityAdd = creativity;
    }

    void displayInfo()
    {
        Debug.Log(itemName+", | Description: "+itemDescription);
    }

    public virtual void Use()
    {
        Debug.Log("Using an item... "+itemName);

        FindObjectOfType<Player>().playerCreativity += creativityAdd;
        FindObjectOfType<Player>().playerStamina += staminaAdd;
        FindObjectOfType<Player>().playerHP += healthAdd;
        
    }
}
