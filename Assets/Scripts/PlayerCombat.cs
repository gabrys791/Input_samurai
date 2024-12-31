using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    private bool attack = true;
    public float damage;
    private float attackCooldown;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        attackCooldown = 0f;

    }
    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        audioSource1.volume = volume;
        audioSource2.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        attackCooldown += Time.deltaTime;
    }
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackCooldown >= 1f)
        {
            attackCooldown = 0f;
            if (attack == true)
            {
                animator.SetTrigger("Attack");
                attack = false;
                PlaySound1();
            }
            else if (attack == false)
            {
                animator.SetTrigger("Attack1");
                attack = true;
                PlaySound2();
            }
            Collider2D[] hitEnemeies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemeies)
            {
                enemy.GetComponent<HealthManager>().TakeDamage(damage);
            }
        }
    }
    public void PlaySound1()
    {
        audioSource1.Play();
    }
    public void PlaySound2()
    {
        audioSource2.Play();
    }
    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
