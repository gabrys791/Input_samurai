using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDist;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = 0f;
    private HealthManager playerHealth;
    private HealthManager enemyHealth;
    private bool isFacingRight;
    private Animator anim;
    void Start()
    {
        enemyHealth = GetComponent<HealthManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(isPlayer())
        {
            if (cooldownTimer >= attackCooldown)
            {
                anim.SetTrigger("damage");
                cooldownTimer = 0f;
            }
        }
        Die();
    }
    private void Attack()
    {
        if(cooldownTimer >= attackCooldown)
        {
            DamagePlayer();
            anim.SetTrigger("damage");
            cooldownTimer = 0f;
        }
    }
    private bool isPlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDist, new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<HealthManager>();
        }
        return hit;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDist, new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if(isPlayer())
        {
            playerHealth.TakeDamage(damage);
        }
    }
    private void Die()
    {
        if(enemyHealth.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

