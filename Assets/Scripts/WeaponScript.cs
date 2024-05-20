using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float knockbackForce = 5f;
    [SerializeField] protected float knockDuration = 1f;
    [HideInInspector] public bool followPlayer = false;
    [HideInInspector] public bool attacking = false;
    protected GameManager gameManager => GameManager.instance;
    protected AudioManager audioManager => AudioManager.instance;
    protected float xPos => transform.localPosition.x;
    public abstract void Attack(float attackDuration);

    protected abstract IEnumerator WeaponCo(float attackDuration);
}
