using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    [SerializeField] float spawnDelay = .3f;
    AIPath aIPath => GetComponentInParent<AIPath>();
    [SerializeField] Spawner spawner;
    //GameManager gameManager => GameManager.instance;
    float nextSpawnTime;
    // Update is called once per frame
    void Update()
    {
        if (aIPath.desiredVelocity != Vector3.zero)
        {
            SetBool("IsWalking", true);

            if (aIPath.desiredVelocity.x > 0.1)
            {
                SetFloat("Direction", 1);
                //SetBool("IsRight", true);
                //ChangeDirectionRotation(1);
            }
            else if (aIPath.desiredVelocity.x < -0.1)
            {
                SetFloat("Direction", -1);
                //SetBool("IsRight", false);
                //ChangeDirectionRotation(-1);
            }

            if (Time.time > nextSpawnTime)
            {
                spawner.Spawn();
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            SetBool("IsWalking", false);
        }
    }
}