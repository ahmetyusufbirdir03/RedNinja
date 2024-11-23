using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject expoPoses;
    [SerializeField] private GameObject expo;

    [Header("Booleans")]
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isDead;

    Animator anim;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        expoPoses = transform.GetChild(2).gameObject;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < 8f && !isAttacking)
            {

            }
        }

        if (isDead)
        {
            anim.SetTrigger("Dead");
            Destroy(this.gameObject, 1f);
        }
    }

}
