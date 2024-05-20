using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [HideInInspector] public int damage;
    [HideInInspector] public bool playerAttack = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health collisionHealth = collision.GetComponent<Health>();

        if (collisionHealth)
        {
            collisionHealth.TakeDamage(damage);
        }

        if (playerAttack)
        {
            collision.GetComponent<EnemyStateMachine>().CallChaseState();
        }
    }
}
