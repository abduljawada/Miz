using UnityEngine;
using Pathfinding;

public class EnemyStateMachine : StateMachine
{

    ExpressionManager expressionManager => GetComponent<ExpressionManager>();
    AIPath aIPath => GetComponent<AIPath>();
    AIDestinationSetter destinationSetter => GetComponent<AIDestinationSetter>();
    Collider2D myCollider => GetComponent<Collider2D>();
    Patrol patrol => GetComponent<Patrol>();
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    EnemyAnimatorManager anim => GetComponentInChildren<EnemyAnimatorManager>();
    WeaponScript weaponScript => GetComponentInChildren<WeaponScript>();
    AudioManager audioManager => AudioManager.instance;

    [SerializeField] float spottingDistance = 5f;
    [SerializeField] float viewingDistance = 10f;
    [SerializeField] float evadeDistance = 2f;
    [SerializeField] float waitingTime = 2f;
    [SerializeField] float expressionTime = 2f;
    [SerializeField] float evadeForce = 2f;
    [SerializeField] float attackingDistance = 5f;
    [SerializeField] float attackingDelay = 1f;
    [SerializeField] float attackDuration = .5f;
    float nextAttackTime;
    float waitingEndTime;
    bool triggeredEndOfPath = false;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Default:

                aIPath.enabled = true;

                FindPlayer();

                break;

            case State.Chase:

                aIPath.enabled = true;

                float distanceToPlayer = Vector2.Distance(transform.position, destinationSetter.target.position);

                if (transform.position.x > destinationSetter.target.transform.position.x)
                {
                    anim.SetFloat("Direction", -1);
                }
                else
                {
                    anim.SetFloat("Direction", 1);
                }

                if (distanceToPlayer > viewingDistance)
                {
                    weaponScript.followPlayer = false;
                    destinationSetter.enabled = false;
                    aIPath.whenCloseToDestination = CloseToDestinationMode.ContinueToExactDestination;

                    ChangeState(State.LosingTarget);
                }
                else if (distanceToPlayer < evadeDistance)
                {
                    Vector2 evadeDirection = transform.position - destinationSetter.target.position;
                    rb.AddForce(evadeDirection.normalized * evadeForce, ForceMode2D.Force);
                }
                else if (distanceToPlayer < attackingDistance)
                {
                    if (Time.time > nextAttackTime)
                    {
                        weaponScript.Attack(attackDuration);
                        nextAttackTime = Time.time + attackingDelay;
                        //ChangeState(State.Stun);
                        //Invoke("Recover", attackDuration);
                    }
                }

                break;

            case State.LosingTarget:

                if (aIPath.reachedEndOfPath)
                {
                    if (!triggeredEndOfPath)
                    {
                        triggeredEndOfPath = true;
                        waitingEndTime = Time.time + waitingTime;
                        expressionManager.ShowExpression(ExpressionManager.Expressions.Question, waitingTime);
                    }
                    else if (Time.time > waitingEndTime)
                    {
                        CallDefaultState();
                    }
                }

                FindPlayer();

                break;

            case State.Stun:

                aIPath.enabled = false;

                break;
        }
        
    }

    void FindPlayer()
    {
        RaycastHit2D[] results = new RaycastHit2D[1];
        myCollider.Raycast((destinationSetter.target.transform.position - transform.position), results, spottingDistance);
        if (results[0].collider != null)
        {
            if (results[0].collider.gameObject == destinationSetter.target.gameObject)
            {
                CallChaseState();
            }
        }
    }

    void CallDefaultState()
    {
        aIPath.whenCloseToDestination = CloseToDestinationMode.Stop;
        triggeredEndOfPath = false;
        destinationSetter.enabled = false;
        patrol.enabled = true;

        ChangeState(State.Default);
    }

    public void CallChaseState()
    {
        expressionManager.RemoveExpression();

        if (state == State.Default || triggeredEndOfPath)
        {
            expressionManager.ShowExpression(ExpressionManager.Expressions.Exclamation, expressionTime);
            audioManager.PlaySound(5);
        }

        nextAttackTime = Time.time + attackingDelay;
        triggeredEndOfPath = false;
        aIPath.whenCloseToDestination = CloseToDestinationMode.Stop;
        destinationSetter.enabled = true;
        patrol.enabled = false;
        if (weaponScript)
        {
            weaponScript.followPlayer = true;
        }
        

        ChangeState(State.Chase);
    }
    void Recover()
    {
        ChangeState(State.Default);
        nextAttackTime = Time.time + attackingDelay;
    }
}
