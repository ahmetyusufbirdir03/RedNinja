using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shark : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float followSpeed = 4f;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Image healthBar;

    public float health;

    private GameObject player;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Vector3 lastPosition;
    private Animator anim;

    private bool movingToTarget = true;
    private bool isDead = false;

    private void Start()
    {
        // Referanslarý al
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();

        // Pozisyonlarý ve saðlýk deðerlerini baþlat
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x + 5, originalPosition.y, originalPosition.z);
        lastPosition = transform.position;

        health = maxHealth;
    }

    private void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 1f)
        {
            // Oyuncuya saldýr
            anim.SetBool("Attack", true);
        }
        else if (distanceToPlayer < 8f)
        {
            // Oyuncuyu takip et
            FollowPlayer();
            anim.SetBool("Attack", false);
        }
        else
        {
            // Hedefler arasýnda hareket et
            MoveBetweenPositions();
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

    private void UpdateRotation()
    {
        Vector3 direction = transform.position - lastPosition;

        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        lastPosition = transform.position;
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(FlashEffect());

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private IEnumerator FlashEffect()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color;

        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.color = originalColor;
    }

    private void Die()
    {
        isDead = true;

        // Hareket ve saldýrý durdur
        moveSpeed = 0;
        followSpeed = 0;

        anim.SetBool("Dead", true);

        // Nesneyi yok et
        Destroy(gameObject, 1f);
    }

    public void HitPlayer()
    {
        if (player.TryGetComponent(out PlayerController playerController))
        {
            playerController.life -= 1;
        }
    }
}
