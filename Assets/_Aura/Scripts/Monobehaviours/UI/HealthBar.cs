using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HitPoints m_hitPoints;
    public Image m_meterImage;
    public TMP_Text m_hpText;

    private void Update()
    {
        //ToDo:Refactor this from update callback checks to event driven updates from the Scriptable object
        m_meterImage.fillAmount = m_hitPoints.hitPoints/m_hitPoints.m_maxHitPoints;
        m_hpText.text = $"HP:{m_meterImage.fillAmount * 100}";
    }
}
