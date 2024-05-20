using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    HealthUI healthUI => GetComponent<HealthUI>();

    public override void TakeDamage(int damage)
    {
        if (Time.time > nextDamageTime)
        {
            gameManager.cameraShake.ShakeCamera(gameManager.shakeIntensity, gameManager.shakeTime);
        }

        base.TakeDamage(damage);

        healthUI.UpdateHearts(currentHealth);
    }

    protected override void Die()
    {
        transform.position = gameManager.spawnPoint;
        currentHealth = maxHealth;
        healthUI.UpdateHearts(currentHealth);

        foreach (Health enemy in gameManager.enemies)
        {
            if (enemy)
            {
                enemy.Heal();
            }
        }

        base.Die();
    }

    public void Win()
    {
        base.Die();
        Destroy(gameObject);
    }

    public override void Heal()
    {
        base.Heal();
        healthUI.UpdateHearts(currentHealth);
    }
}
