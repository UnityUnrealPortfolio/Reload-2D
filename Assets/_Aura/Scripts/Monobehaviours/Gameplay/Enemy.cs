using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    //ToDo::consider refactoring Enemy properties into a Scriptable object
    public float startingHitPoints;
    public int damageStrength;
    float hitPoints;
    Coroutine damageCoroutine;//store a ref to a coroutine so we can stop it at later time

    private void OnEnable()
    {
        ResetCharacter();
    }

    private void OnCollisionEnter2D(Collision2D collision)//ToDo:Why not run this in OnCollisionStay rather than OnCollisionEnter?
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Inside OnCollisionExit2D");
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
       while (true)
        {
            hitPoints = hitPoints - damage;

            if(hitPoints <= float.Epsilon)
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

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }
}
