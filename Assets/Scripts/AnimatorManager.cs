using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorManager : MonoBehaviour
{
    //SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    Animator anim => GetComponent<Animator>();

    public void SetBool(string boolName, bool newValue)
    {
        if (anim.GetBool(boolName) != newValue)
        {
            anim.SetBool(boolName, newValue);
        }
    }

    public void SetFloat(string floatName, float newValue)
    {
        newValue = Mathf.Round(newValue);

        if (anim.GetFloat(floatName) != newValue)
        {
            anim.SetFloat(floatName, newValue);
        }
    }

    //public void ChangeDirection(int direction)
    //{
        //if (direction == 1)
        //{
            //spriteRenderer.flipX = false;
        //}
        //else if (direction == -1)
        //{
            //spriteRenderer.flipX = true;
        //}
    //}

    //protected void ChangeDirectionRotation(int direction)
    //{
        //if (direction == 1)
        //{
            //transform.forward = new Vector3(0, 0, 0);
       //}
        //else if (direction == -1)
        //{
            //transform.forward = new Vector3(0, 180, 0);
        //}
    //}
}
