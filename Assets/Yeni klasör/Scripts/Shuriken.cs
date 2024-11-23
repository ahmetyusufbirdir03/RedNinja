using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float moveInput;



    private void Update()
    {
        transform.position += new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0);
        transform.Rotate(new Vector3(0, 0, 1 * rotationSpeed));
    }



}
