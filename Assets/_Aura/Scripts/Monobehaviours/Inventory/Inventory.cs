using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public RectTransform slotHolder;
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

                newSlot.transform.SetParent(slotHolder,false);

                slots[i] = newSlot;

                itemImages[i] = newSlot.GetComponent<Slot>().ItemImage;

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

                return true;
            }
            else if (items[i] == null)
            {
                Debug.Log("inside Add Items, Empty slots");
                //add to an empty slot
                //first time we've picked up this item
                //copy the item before adding so we don't
                //change original Scriptable Object
                items[i] = Instantiate(itemToAdd);
                items[i].m_quantity = 1;
                itemImages[i].sprite = itemToAdd.m_sprite;
                itemImages[i].enabled = true;
                return true;
            }
         
      
        }

        return false;  
    }
}
