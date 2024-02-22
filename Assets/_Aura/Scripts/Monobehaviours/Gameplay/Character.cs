using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public abstract void ResetCharacter();
  
    /// <summary>
    /// Called by other characters to damage the current character.
    /// 
    /// </summary>
    /// <param name="damage">amount of damage</param>
    /// <param name="interval">time interval in case of recurring damge application e.g walking on lava</param>
    /// <returns></returns>
    public abstract IEnumerator DamageCharacter(int damage, float interval);

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }
}
