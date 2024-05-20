using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    AudioManager audioManager => AudioManager.instance;
    protected SpriteFlash spriteFlash => GetComponentInChildren<SpriteFlash>();
    protected Spawner spawner => GetComponent<Spawner>();
    protected GameManager gameManager => GameManager.instance;
    [SerializeField] protected int maxHealth = 6;
    protected int currentHealth;
    [SerializeField] protected float invincibilityDuration = .75f;
    protected float nextDamageTime;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        if (Time.time > nextDamageTime)
        {
            audioManager.PlaySound(1);
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }

            spriteFlash.Flash();

            spawner.Spawn();

            nextDamageTime = Time.time + invincibilityDuration;
        }
        
    }

    protected virtual void Die()
    {
        audioManager.PlaySound(3);
        Instantiate(gameManager.deathParticle, transform.position, Quaternion.identity);
        if (tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void Heal()
    {
        currentHealth = maxHealth;
    }
}
