using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        Vector3 pos = player.position;
        pos.y += 8f;     
        transform.position = pos;
    }
}
