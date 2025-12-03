using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrash : MonoBehaviour
{
    public GameObject trash;
    public float minDropTime = 3f;
    public float maxDropTime = 9f;

    private float dropTimer;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        dropTimer -= Time.deltaTime;

        if (dropTimer <= 0f)
        {
            DropItem();
            ResetTimer();
        }
    }

    void DropItem()
    {
        Instantiate(trash, transform.position, Quaternion.identity);
    }

    void ResetTimer()
    {
        dropTimer = Random.Range(minDropTime, maxDropTime);
    }
}
