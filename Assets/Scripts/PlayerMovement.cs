using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float spawnDelay = .2f;
    //GameManager gameManager => GameManager.instance;
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    AnimatorManager animatorManager => GetComponentInChildren<AnimatorManager>();
    StateMachine stateMachine => GetComponent<StateMachine>();
    Spawner spawner => GetComponent<Spawner>();
    Vector2 moveInput;
    float nextSpawnTime;

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveInput != Vector2.zero && stateMachine.state == StateMachine.State.Default)
        {
            animatorManager.SetBool("IsWalking", true);

            if (Time.time > nextSpawnTime)
            {
                spawner.Spawn();
                nextSpawnTime = Time.time + spawnDelay;
            }            
        }
        else
        { 
            animatorManager.SetBool("IsWalking", false);
        }
    }

    private void FixedUpdate()
    {
        if (moveInput != Vector2.zero && stateMachine.state == StateMachine.State.Default)
        {
            rb.velocity = moveInput.normalized * movementSpeed;
        }
        else if (moveInput == Vector2.zero && stateMachine.state == StateMachine.State.Default)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
