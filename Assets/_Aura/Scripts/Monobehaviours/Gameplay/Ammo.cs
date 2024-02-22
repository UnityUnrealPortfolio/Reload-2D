using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int damageInflicted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)//ToDo:Look into this to see if we can refactor the enemy better
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            StartCoroutine(enemy.DamageCharacter(damageInflicted, 0.0f));

            gameObject.SetActive(false);
        }
    }
}
