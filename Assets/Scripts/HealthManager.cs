using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    public Respawn respawn;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
        
        if(currentHealth > 0)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
            anim.SetTrigger("takeDmg");
            
        }
        else
        {
            if(!dead)
            {
                dead = true;
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerCombat>().enabled = false;
                StartCoroutine(HandleDeath());
            }
            
        }
    }
    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(2f);
        transform.position = respawn.respawnPoint.transform.position;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerCombat>().enabled = true;
        anim.ResetTrigger("die");
        currentHealth = startingHealth;
        anim.Play("Idle");
        dead = false;
    }
    void Update()
    {
      
    }
}
