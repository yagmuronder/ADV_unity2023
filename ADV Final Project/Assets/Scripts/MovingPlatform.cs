using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + new Vector3(0, y, 0);
    }
}

