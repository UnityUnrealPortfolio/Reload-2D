using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public const int numSlots = 5;
    Image[] itemImages = new Image[numSlots];
    Item[] items = new Item[numSlots];
    GameObject[] slots = new GameObject[numSlots];

    private void Start()
    {
        CreateSlots();
    }

    public void CreateSlots()
    {
        if(slotPrefab != null)
        {
            //loop through and create new slots
            for(int i = 0; i < numSlots; i++)
            {
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = $"ItemSlot_ {i}";

                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform,false);//ToDo:refactor this to just attach a reference to the parent in the editor

                slots[i] = newSlot;

                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();//ToDo:can't we have the slot hold references to it's own children

            }
        }
    }

    public bool AddItem(Item itemToAdd)
    {
        bool result = false;//ToDo:quickly added this refactor

        for(int i = 0; i<items.Length; i++)
        {
            //Handle situation where we already have items in the inventory
            if (items[i] != null && items[i].m_itemType == itemToAdd.m_itemType
                && itemToAdd.m_stackable == true)
            {
                //add item
                items[i].m_quantity = items[i].m_quantity + 1;

                //display it on the slot text
                //ToDo:again this could be a method called on the Slot
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();

                TMP_Text quantityText = slotScript.qtyText;

                quantityText.enabled = true;

                quantityText.text = items[i].m_quantity.ToString();

                result = true;
            }
            if (items[i] == null)
            {
                //add to an empty slot
                //first time we've picked up this item
                //copy the item before adding so we don't
                //change original Scriptable Object
                items[i] = Instantiate(itemToAdd);
                items[i].m_quantity = 1;
                itemImages[i].sprite = itemToAdd.m_sprite;
                itemImages[i].enabled = true;
                result = true;
            }
         
      
        }

        return result;  
    }
}
