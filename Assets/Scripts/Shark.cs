using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shark : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float followSpeed = 4f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Image healthBar;

    public float health;

    private GameObject player;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Animator anim;
    private Rigidbody2D rb;

    private bool movingToTarget = true;
    private bool isDead = false;

    private void Start()
    {
        // Referanslari al
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Pozisyonlari ve sa�l�k degerlerini baslat
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x + 3, originalPosition.y, originalPosition.z);

        health = maxHealth;
    }

    private void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 1f)
        {
            // Oyuncuya saldir
            anim.SetBool("Attack", true);
        }
        else if (distanceToPlayer < 5f)
        {
            FollowPlayer();
            CheckForObstacle();
            anim.SetBool("Attack", false);
        }
        else
        {
            MoveBetweenPositions();
            CheckForObstacle();
            anim.SetBool("Attack", false);
        }

        UpdateRotation();
        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
    }

    private void MoveBetweenPositions()
    {
        anim.SetBool("Move", true);

        Vector3 target = movingToTarget ? targetPosition : originalPosition;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.2f)
        {
            movingToTarget = !movingToTarget;
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed * Time.deltaTime);
    }

    private void CheckForObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * (movingToTarget ? 1 : -1), 0.5f);

        if (hit.collider != null && !hit.collider.CompareTag("Player"))
        {
            if (Mathf.Abs(rb.velocity.y) < 0.1f)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void UpdateRotation()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;

            if (directionToPlayer.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (directionToPlayer.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // Hareket ve sald�r� durdur
        moveSpeed = 0;
        followSpeed = 0;

        anim.SetBool("Dead", true);

        // Nesneyi yok et
        Destroy(gameObject, 1f);
    }

    public void HitPlayer()
    {
        player.GetComponent<PlayerController>().life -=1;
    }
}
