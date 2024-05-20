using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] bool detonateOnDestroy = false;
    [SerializeField] float timerDuration = 5f;
    float timerEndTime;
    [SerializeField] UnityEvent unityEvent;

    // Start is called before the first frame update
    void Start()
    {
        timerEndTime = Time.time + timerDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timerEndTime)
        {
            Detonate();
        }
    }

    void Detonate()
    {
        unityEvent?.Invoke();
    }

    private void OnDestroy()
    {
        if (detonateOnDestroy)
        {
            Detonate();
        }
    }
}
