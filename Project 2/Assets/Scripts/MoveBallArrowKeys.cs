using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBallArrowKeys : MonoBehaviour
{
    public float mSpeed = 7f;

    void Start()
    {
        mSpeed = 1f; //f means float
    }

    void Update()
    {
        transform.Translate(mSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, mSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
    }
}
