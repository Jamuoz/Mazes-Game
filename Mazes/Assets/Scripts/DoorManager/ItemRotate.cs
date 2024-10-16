using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    public float rotationSpeed = 30f;

    void Update()
    {
        // Rotar el objeto en el eje Y de manera continua
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
