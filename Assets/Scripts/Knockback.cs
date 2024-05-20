using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    StateMachine stateMachine => GetComponent<StateMachine>();
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    StateMachine.State oldState;
    public void Knock(Vector2 knockForce, float knockTime)
    {
        if (stateMachine.state != StateMachine.State.Stun)
        {
            oldState = stateMachine.state;
        } 
        stateMachine.ChangeState(StateMachine.State.Stun);
        rb.AddForce(knockForce, ForceMode2D.Impulse);
        StartCoroutine(Recover(knockTime));
    }

    IEnumerator Recover(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        stateMachine.ChangeState(oldState);
        rb.velocity = Vector2.zero;
    }
}
