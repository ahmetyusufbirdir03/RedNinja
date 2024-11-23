using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float maxRotationAngle = 45f;
    public float knockbackForce = 10f;
    private float timeCounter = 0f;

    void Update()
    {
        timeCounter += Time.deltaTime * rotationSpeed;
        float currentAngle = Mathf.Sin(timeCounter) * maxRotationAngle;
        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().life -= 1;

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
