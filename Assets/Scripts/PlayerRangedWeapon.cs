using System.Collections;
using UnityEngine;

public class PlayerRangedWeapon : WeaponScript
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 15f;
    [SerializeField] Transform firePoint;
    
    public override void Attack(float attackDuration)
    {
        attacking = true;

        StartCoroutine(WeaponCo(attackDuration));

        audioManager.PlaySound(2);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        DamageOnCollision damageOnCollision = bullet.GetComponent<DamageOnCollision>();
        damageOnCollision.damage = damage;
        damageOnCollision.playerAttack = true;

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
            Vector2 mousePos = gameManager.cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 rotationDirection = mousePos - (Vector2)transform.position;

            if (transform.parent.position.x > mousePos.x)
            {
                transform.localPosition = new Vector2(-Mathf.Abs(xPos), transform.localPosition.y);
                transform.rotation = Quaternion.FromToRotation(Vector3.right, -rotationDirection);
                transform.eulerAngles = new Vector3(180, 180, transform.eulerAngles.z);
            }
            else if (transform.parent.position.x <= mousePos.x)
            {
                transform.localPosition = new Vector2(Mathf.Abs(xPos), transform.localPosition.y);
                transform.rotation = Quaternion.FromToRotation(Vector3.right, rotationDirection);
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            }
        }
    }
}
