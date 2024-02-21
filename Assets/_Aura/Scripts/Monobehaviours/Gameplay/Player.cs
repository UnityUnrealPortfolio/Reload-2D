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
                bool shouldDisappear = false;
                switch (hitItem.m_itemType)
                {
                    case Item.ItemType.Coin:
                        shouldDisappear = true;
                        break;
                    case Item.ItemType.Health:
                      shouldDisappear =  AdjustHitPoints(hitItem.m_quantity);
                        break;
                    default:
                        break;
                }

                if(shouldDisappear)
                {
                   collision.gameObject.SetActive(false);
                }
            }
        }
    }

    public bool AdjustHitPoints(float _amount)
    {
        //logic to prevent health pick up if hit points are at max
        if(m_hitPoints.hitPoints < m_hitPoints.m_maxHitPoints)
        {
            m_hitPoints.hitPoints += _amount;
            Debug.Log($"Adjusted hitPoints by: {_amount}. New Value: {m_hitPoints.hitPoints}");
            return true;
        }
        return false;
       
    }
}
