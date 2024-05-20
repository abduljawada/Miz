using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackOnCollision : MonoBehaviour
{
    [HideInInspector] public float knockbackForce;
    [HideInInspector] public float knockTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if (rb)
        {
            Vector2 knockbackDirection = collision.transform.position - transform.position;

            Knockback collisionKnockback = collision.GetComponent<Knockback>();

            if (collisionKnockback)
            {
                collisionKnockback.Knock(knockbackDirection.normalized * knockbackForce, knockTime);
            }
        }
    }
}