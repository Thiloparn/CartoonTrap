using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalObject : MonoBehaviour
{
    public float lifeTime = 5f;
    private float currentTimeAlive;

    private void Awake()
    {
        currentTimeAlive = 0;
    }

    private void FixedUpdate()
    {
        currentTimeAlive += Time.fixedDeltaTime;

        if (currentTimeAlive >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
