using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="HitPoints")]
public class HitPoints : ScriptableObject
{
    public float hitPoints;
    public float m_maxHitPoints;
    public float m_startingHitPoints;

    private void OnValidate()
    {
        hitPoints = m_startingHitPoints;
    }
}
