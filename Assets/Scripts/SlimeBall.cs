using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    public float moveSpeed;
    public float moveInput;

    private void Update()
    {
        transform.position += new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0);
        Destroy(this.gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().life--;
            Destroy(this.gameObject);
        }
    }
}
