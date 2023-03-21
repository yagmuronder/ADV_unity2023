using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    private void OnCollisionEnter(Collision collision)
    {
        Color randomColor = GetRandomColor();
        GetComponent<Renderer>().material.color = randomColor;
    }

    private Color GetRandomColor()
    {
        return new Color(
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f));
    }
}
