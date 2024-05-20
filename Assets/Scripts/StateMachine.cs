using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Default,
        Chase,
        LosingTarget,
        Stun
    }

    [HideInInspector] public State state = State.Default;

    public void ChangeState(State newState)
    {
        if (newState != state)
        {
            state = newState;
        }
    }
}
