using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    SpriteFlash spriteFlash => GetComponentInChildren<SpriteFlash>();
    WeaponScript weapon => GetComponent<WeaponScript>();
    StateMachine stateMachine => GetComponentInParent<StateMachine>();
    Rigidbody2D rb => GetComponentInParent<Rigidbody2D>();
    Spawner spawner => GetComponent<Spawner>();
    [SerializeField] float attackingDelay = 2f;
    public float attackDuration = .5f;
    float nextAttackTime = 0f;
    bool flashed = true;
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            if (!flashed)
            {
                spriteFlash.Flash();
                flashed = true;
                spawner.Spawn();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (stateMachine.state == StateMachine.State.Default)
                {
                    rb.velocity = Vector2.zero;
                    weapon.Attack(attackDuration);
                    stateMachine.ChangeState(StateMachine.State.Stun);
                    Invoke("Recover", attackDuration);
                }
            }
        }
    }

    void Recover()
    {
        stateMachine.ChangeState(StateMachine.State.Default);
        nextAttackTime = Time.time + attackingDelay;
        flashed = false;
    }
}
