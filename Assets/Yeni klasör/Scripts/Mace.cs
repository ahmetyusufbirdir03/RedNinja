using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float maxRotationAngle = 45f;
    private float timeCounter = 0f;

    void Update()
    {
        timeCounter += Time.deltaTime * rotationSpeed;
        float currentAngle = Mathf.Sin(timeCounter) * maxRotationAngle;
        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
