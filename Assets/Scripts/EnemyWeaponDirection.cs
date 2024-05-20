using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyWeaponDirection : WeaponDirection
{
    AIPath aIPath => GetComponentInParent<AIPath>();
    public bool followPlayer = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationDirection = gameManager.player.transform.position - transform.position;

        if (followPlayer)
        {
            if (transform.parent.position.x < gameManager.player.transform.position.x)
            {
                transform.localPosition = new Vector3(Mathf.Abs(xPos), transform.localPosition.y, 0);
                transform.rotation = Quaternion.FromToRotation(Vector3.right, -rotationDirection);
                transform.eulerAngles = new Vector3(180, 180, transform.eulerAngles.z);
            }
            else
            {
                transform.localPosition = new Vector3(-Mathf.Abs(xPos), transform.localPosition.y, 0);
                transform.rotation = Quaternion.FromToRotation(Vector3.right, rotationDirection);
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            }
        }
        else if (aIPath.desiredVelocity != Vector3.zero)
        {
            if (aIPath.desiredVelocity.x < -0.1f)
            {
                transform.localPosition = new Vector3(-Mathf.Abs(xPos), transform.localPosition.y, 0);
                transform.eulerAngles = new Vector2(180, 180);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.Abs(xPos), transform.localPosition.y, 0);
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            }
        }
    }
}
