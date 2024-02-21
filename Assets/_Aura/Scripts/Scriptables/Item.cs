using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string m_objectName;
    public Sprite m_sprite;
    public int m_quantity;
    public bool m_stackable;
    public ItemType m_itemType;
    public enum ItemType
    {
        Coin,
        Health
    }
}
