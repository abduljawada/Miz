using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : WeaponScript
{
    Collider2D myCollider => GetComponent<Collider2D>();
    [SerializeField] float rotationDegree = 180f;
    [SerializeField] float rotationDuration = .75f;
    bool isRight = true;


    private void Awake()
    {
        GetComponent<DamageOnCollision>().damage = damage;
        GetComponent<KnockbackOnCollision>().knockbackForce = knockbackForce;
        GetComponent<KnockbackOnCollision>().knockTime = knockDuration;
    }
    public override void Attack(float attackDuration)
    {
        audioManager.PlaySound(4);
        myCollider.enabled = true;

        attacking = true;

        if (isRight)
        {
            transform.DORotate(new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z - rotationDegree), rotationDuration);
        }
        else
        {
            transform.DORotate(new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z + rotationDegree), rotationDuration);
        }
        StartCoroutine(WeaponCo(attackDuration));
    }

    protected override IEnumerator WeaponCo(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);

        myCollider.enabled = false;

        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            Vector2 mousePos = gameManager.cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 rotationDirection = mousePos - (Vector2)transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.right, rotationDirection);
            if (transform.parent.position.x > mousePos.x)
            {
                isRight = false;
                transform.localPosition = new Vector2(-Mathf.Abs(xPos), transform.localPosition.y);
                transform.eulerAngles = new Vector3(180, 180, transform.eulerAngles.z);
            }
            else if (transform.parent.position.x <= mousePos.x)
            {
                isRight = true;
                transform.localPosition = new Vector2(Mathf.Abs(xPos), transform.localPosition.y);
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            }
        }
    }
}
