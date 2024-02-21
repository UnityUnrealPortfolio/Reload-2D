using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePicked"))
        {
            Item hitItem = collision.gameObject.GetComponent<Consumable>().item;
            if (hitItem != null)
            {
                Debug.Log($"hit item name:{hitItem.m_objectName} of type {hitItem.m_itemType}");
               
                switch (hitItem.m_itemType)
                {
                    case Item.ItemType.Coin:
                        break;
                    case Item.ItemType.Health:
                        AdjustHitPoints(hitItem.m_quantity);
                        break;
                    default:
                        break;
                }

                collision.gameObject.SetActive(false);
            }
        }
    }

    private void AdjustHitPoints(int _amount)
    {
        m_hitPoints += _amount;
        Debug.Log($"Adjusted hitPoints by: {_amount}. New Value: {m_hitPoints}");
    }
}
