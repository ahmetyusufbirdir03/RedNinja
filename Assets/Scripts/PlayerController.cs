using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float backSpeed;

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

    [Header("Coin")]
    [SerializeField] public int coinCount;
    [SerializeField] public TMP_Text coinText;
    [SerializeField] private float coinIncreaseSpeed = 0.1f;

    [SerializeField] private bool canDoubleJump = false; // Çift zıplama kullanılabilir mi?
    [SerializeField] public bool isJumpBonusActive = false; // Bonus aktif mi?



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
        AdjustRotationToSlope();

        coinText.text = coinCount.ToString("00");

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

        if (isMoving)
        {
            anim.speed = Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1f;
        }

    }

    private void PerformJump(float slopeAngle)
    {
    if (slopeAngle == 0)
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    else
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.2f);

    anim.SetBool("JumpUp", true);
    anim.SetBool("JumpDown", false);
    }


    private void Jump()
    {
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    isAboutToLand = !isGrounded && rb.velocity.y < 0 && Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius * 5f, groundLayer);

    anim.SetBool("JumpUp", !isGrounded && rb.velocity.y > 0);
    anim.SetBool("JumpDown", isAboutToLand);

    Vector2 rayDirection = Vector2.down + (Vector2.right * rb.velocity.x * 0.1f);
    float rayDistance = groundCheckRadius * 5f;

    RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, rayDirection, rayDistance, groundLayer);
    float slopeAngle = Vector2.Angle(Vector2.up, hit.normal); // Zemin eğimi

    if (isGrounded)
    {
        anim.SetBool("JumpDown", false);
        canDoubleJump = true; // Yere inince çift zıplama sıfırlanır
    }

    if (Input.GetButtonDown("Jump"))
    {
        if (isGrounded) // İlk zıplama
        {
            PerformJump(slopeAngle);
        }
        else if (isJumpBonusActive && canDoubleJump) // Çift zıplama
        {
            PerformJump(0); // Eğimi dikkate almadan çift zıplama yapılır
            canDoubleJump = false; // Çift zıplama hakkını tüket
        }
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

        if (life <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("ChestBlue"))
        {
            Animator chestAnim = collision.gameObject.GetComponent<Animator>();
            chestAnim.SetTrigger("Open");

            StartCoroutine(IncrementCoins(20));
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            life -= 1;
            StartCoroutine(MoveBackSmoothly());
        }
    }

    private IEnumerator MoveBackSmoothly()
    {
        float targetX = transform.position.x - 1f;
        float startX = transform.position.x;
        float elapsedTime = 0f;

        while (elapsedTime < backSpeed)
        {
            float newX = Mathf.Lerp(startX, targetX, elapsedTime / backSpeed);
            rb.MovePosition(new Vector2(newX, transform.position.y));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(new Vector2(targetX, transform.position.y));
    }


    private void AdjustRotationToSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius * 3f, groundLayer);

        if (hit.collider != null)
        {
            Vector2 groundNormal = hit.normal;
            float slopeAngle = Vector2.SignedAngle(Vector2.up, groundNormal);

            if (rb.velocity.x > 0)
            {
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, slopeAngle);
            }
            else if (rb.velocity.x < 0)
            {
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, -slopeAngle);
            }
        }

    }

    private IEnumerator IncrementCoins(int amount)
    {
        int coinsAdded = 0;
        while (coinsAdded < amount)
        {
            coinCount++;
            coinsAdded++;
            yield return new WaitForSeconds(coinIncreaseSpeed);
        }
    }

    public void ActivateJumpBonus()
    {
        isJumpBonusActive = true; // Bonus etkinleştirildi
        StartCoroutine(JumpBonusDisableRoutine());
    }
        
    IEnumerator JumpBonusDisableRoutine(){
        yield return new WaitForSeconds(6.0f);
        isJumpBonusActive = false;
    }

}
