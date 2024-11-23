using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject slimeBomb;
    [SerializeField] private Transform spawnPoint; 

    [Header("Booleans")]
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isDead;


    Animator anim;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < 8f && !isAttacking)
            {
                StartCoroutine(SpawnSlimeBombs());
            }
        }

        if (isDead)
        {
            anim.SetTrigger("Dead");
            Destroy(this.gameObject, 1f);
        }
    }

    private IEnumerator SpawnSlimeBombs()
    {
        isAttacking = true;

        while (Vector3.Distance(transform.position, player.transform.position) < 8f)
        {
            anim.SetTrigger("Attack");
            GameObject slimeObj = Instantiate(slimeBomb, spawnPoint.position, Quaternion.Euler(0,180,0));
            slimeObj.GetComponent<SlimeBomb>().moveInput = transform.eulerAngles.y == 180 ? -1 : 1;
            yield return new WaitForSeconds(3f);
        }

        isAttacking = false;
    }
}
