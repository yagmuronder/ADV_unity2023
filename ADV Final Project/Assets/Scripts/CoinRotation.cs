using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the coin around its local Y-axis (upwards) continuously
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
