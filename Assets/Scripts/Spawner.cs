using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    public void Spawn()
    {
        Instantiate(objectPrefab, transform.position, Quaternion.identity);
    }
}
