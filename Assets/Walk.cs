using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public float speed = 2f;
    public float turnSpeed = 120f;
    public float walkTimeMin = 3f;
    public float walkTimeMax = 7f;

    private float walkTimer;

    void Start()
    {
        SetNewWalkTimer();
    }

    void Update()
    {
        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        walkTimer -= Time.deltaTime;

        // When timer ends new direction boom
        if (walkTimer <= 0f)
        {
            // Random rotation
            float randomAngle = Random.Range(-160f, 160f);
            transform.Rotate(0, randomAngle, 0);

            SetNewWalkTimer();
        }
    }

    void SetNewWalkTimer()
    {
        walkTimer = Random.Range(walkTimeMin, walkTimeMax);
    }
}
