using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    Animator anim => GetComponent<Animator>();

    public void SetBool(string boolName, bool newValue)
    {
        if (anim.GetBool(boolName) != newValue)
        {
            anim.SetBool(boolName, newValue);
        }
    }

    public void ChangeDirection(int direction)
    {
        if (direction == 1)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction == -1)
        {
            spriteRenderer.flipX = true;
        }
    }
}
