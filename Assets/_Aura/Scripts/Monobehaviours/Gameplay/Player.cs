using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HitPoints _hitPointsSO;
    private Inventory inventory;

    //ToDo:refactor to use a scriptable object architecture
    private void OnEnable()//when player enters the world it gets reference to this guy
    {
        ResetCharacter();
    }
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
                        shouldDisappear = inventory.AddItem(hitItem);
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
        if(_hitPointsSO.hitPoints < _hitPointsSO.m_maxHitPoints)
        {
            _hitPointsSO.hitPoints += _amount;
            Debug.Log($"Adjusted hitPoints by: {_amount}. New Value: {_hitPointsSO.hitPoints}");
            return true;
        }
        return false;
       
    }

    public override void ResetCharacter()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
       while(true)
        {
           _hitPointsSO.hitPoints -= damage;

            if(_hitPointsSO.hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if(interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    public override void KillCharacter()
    {
        base.KillCharacter();

        Destroy(inventory.gameObject);
    }
}
