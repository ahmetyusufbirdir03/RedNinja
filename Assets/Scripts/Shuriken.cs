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

        Destroy(this.gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slime"))
        {
            collision.gameObject.GetComponent<Slime>().health -= 40;
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Shark"))
        {
            collision.gameObject.GetComponent<Shark>().health -= 20;
            Destroy(this.gameObject);
        }
    }



}
