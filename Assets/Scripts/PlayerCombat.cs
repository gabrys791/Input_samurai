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
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        attackCooldown = 0f;
 
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        attackCooldown += Time.deltaTime;
    }
    public void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space) && attackCooldown >= 1f)
        {
            attackCooldown = 0f;
            if(attack == true)
            {
                animator.SetTrigger("Attack");
                attack = false;
            }
            else if(attack == false)
            {
                animator.SetTrigger("Attack1");
                attack = true;
            }
            Collider2D[] hitEnemeies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach(Collider2D enemy in hitEnemeies)
            {
                enemy.GetComponent<HealthManager>().TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
