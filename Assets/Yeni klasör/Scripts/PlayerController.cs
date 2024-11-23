using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Ground Checker")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.35f;

    [Header("Booleans")]
    [SerializeField] private bool isGrounded;
    [SerializeField] public bool isAboutToLand;
    [SerializeField] private bool canFire;

    [Header("Fire")]
    [SerializeField] private GameObject shuriken;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float fireCooldown;

    [Header("Health")]
    [SerializeField] public int life;
    [SerializeField] public GameObject healthParent;

    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        canFire = true;
        life = 3;
    }

    private void Update()
    {
        Move();
        Jump();
        Fire();
        HealthManager();
    }

    private void Move()
    {
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? 6 : 3;

        bool isMoving = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        anim.SetBool("Move", isMoving);

        if(isMoving)
        {
            anim.speed = Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1f;
        }

    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isAboutToLand = !isGrounded && rb.velocity.y < 0 && Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius * 5f, groundLayer);

        anim.SetBool("JumpUp", !isGrounded && rb.velocity.y > 0);
        anim.SetBool("JumpDown", isAboutToLand);                 

        if (isGrounded)
        {
            anim.SetBool("JumpDown", false);
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("JumpUp", true);
            anim.SetBool("JumpDown", false);
        }
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire)
        {
            canFire = false;

            GameObject shrukienObj = Instantiate(shuriken, firePoint.transform.position, Quaternion.identity);
            shrukienObj.GetComponent<Shuriken>().moveInput = transform.eulerAngles.y == 180 ? -1 : 1;

            Invoke(nameof(ResetCanFire), fireCooldown);
        }
    }

    private void ResetCanFire()
    {
        canFire = true;
    }

    private void HealthManager()
    {
        Transform[] healthIcons = new Transform[3];
        for (int i = 0; i < 3; i++)
        {
            healthIcons[i] = healthParent.transform.GetChild(i);
        }

        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].gameObject.SetActive(i < life);
        }

    }



}
