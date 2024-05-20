using System.Collections;
using UnityEngine;
using Pathfinding;

public class EnemyRangedWeapon : WeaponScript
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 15f;
    [SerializeField] Transform firePoint;
    AIPath aIPath => GetComponentInParent<AIPath>();

    public override void Attack(float attackDuration)
    {
        audioManager.PlaySound(2);
        attacking = true;

        StartCoroutine(WeaponCo(attackDuration));

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        DamageOnCollision damageOnCollision = bullet.GetComponent<DamageOnCollision>();
        damageOnCollision.damage = damage;

        KnockbackOnCollision knockbackOnCollision = bullet.GetComponent<KnockbackOnCollision>();
        knockbackOnCollision.knockbackForce = knockbackForce;
        knockbackOnCollision.knockTime = knockDuration;
    }

    protected override IEnumerator WeaponCo(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);

        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            Vector3 rotationDirection = gameManager.player.transform.position - transform.position;

            if (followPlayer)
            {
                if (transform.parent.position.x < gameManager.player.transform.position.x)
                {
                    transform.localPosition = new Vector3(Mathf.Abs(xPos), transform.localPosition.y, 0);
                    transform.rotation = Quaternion.FromToRotation(Vector3.right, -rotationDirection);
                    transform.eulerAngles = new Vector3(180, 180, transform.eulerAngles.z);
                }
                else
                {
                    transform.localPosition = new Vector3(-Mathf.Abs(xPos), transform.localPosition.y, 0);
                    transform.rotation = Quaternion.FromToRotation(Vector3.right, rotationDirection);
                    transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
                }
            }
            else if (aIPath.desiredVelocity != Vector3.zero)
            {
                if (aIPath.desiredVelocity.x < -0.1f)
                {
                    transform.localPosition = new Vector3(-Mathf.Abs(xPos), transform.localPosition.y, 0);
                    transform.eulerAngles = new Vector2(180, 180);
                }
                else
                {
                    transform.localPosition = new Vector3(Mathf.Abs(xPos), transform.localPosition.y, 0);
                    transform.eulerAngles = new Vector2(0, 0);
                }
            }
        }    
    }
}
