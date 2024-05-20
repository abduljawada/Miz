using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    AnimatorManager animatorManager => GetComponentInChildren<AnimatorManager>();
    Vector2 moveInput;

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveInput != Vector2.zero)
        {
            animatorManager.SetBool("IsWalking", true);

            if (moveInput.x == 1)
            {
                animatorManager.ChangeDirection(1);
            }
            else if (moveInput.x == -1)
            {
                animatorManager.ChangeDirection(-1);
            }
        }
        else
        {
            animatorManager.SetBool("IsWalking", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * movementSpeed;
    }
}
