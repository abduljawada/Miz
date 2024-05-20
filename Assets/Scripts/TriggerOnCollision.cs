﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnCollision : MonoBehaviour
{
    [SerializeField] UnityEvent unityEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        unityEvent?.Invoke();
    }
}
