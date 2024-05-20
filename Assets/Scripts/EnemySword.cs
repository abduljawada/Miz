using DG.Tweening;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : WeaponScript
{
    Collider2D myCollider => GetComponent<Collider2D>();
    [SerializeField] float rotationDegree = 180f;
    [SerializeField] float rotationDuration = .75f;
    AIPath aIPath => GetComponentInParent<AIPath>();
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

        StartCoroutine(WeaponCo(attackDuration));

        if (isRight)
        {
            transform.DORotate(new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z - rotationDegree), rotationDuration);
        }
        else
        {
            transform.DORotate(new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z + rotationDegree), rotationDuration);
        }
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
            

            if (followPlayer)
            {
                Vector3 rotationDirection = gameManager.player.transform.position - transform.position;
                

                if (transform.parent.position.x > gameManager.player.transform.position.x)
                {
                    isRight = false;
                    transform.localPosition = new Vector3(-Mathf.Abs(xPos), transform.localPosition.y, 0);
                    transform.rotation = Quaternion.FromToRotation(Vector3.right, rotationDirection);
                    transform.eulerAngles = new Vector3(180, 180, transform.eulerAngles.z);
                }
                else
                {
                    isRight = true;
                    transform.localPosition = new Vector3(Mathf.Abs(xPos), transform.localPosition.y, 0);
                    transform.rotation = Quaternion.FromToRotation(Vector3.right, rotationDirection);
                    transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
                }
            }
            else if (aIPath.desiredVelocity != Vector3.zero)
            {
                if (aIPath.desiredVelocity.x < -0.1f)
                {
                    isRight = false;
                    transform.localPosition = new Vector3(-Mathf.Abs(xPos), transform.localPosition.y, 0);
                    transform.eulerAngles = new Vector3(0, 180, transform.eulerAngles.z);
                }
                else
                {
                    isRight = true;
                    transform.localPosition = new Vector3(Mathf.Abs(xPos), transform.localPosition.y, 0);
                    transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
                }
            }
        }
    }
}
